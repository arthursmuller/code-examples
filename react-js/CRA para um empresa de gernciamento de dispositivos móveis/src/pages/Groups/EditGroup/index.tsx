import { Box, Checkbox } from '@chakra-ui/react';
import React, { useEffect, useState, useRef } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useParams, useHistory } from 'react-router-dom';
import SimpleReactValidator from 'simple-react-validator';

import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import FormSubtitle from '../../../components/FormSubtitle';
import Input from '../../../components/Input';
import PageActions from '../../../components/PageActions';
import PagePagination from '../../../components/PagePagination';
import PageTitle from '../../../components/PageTitle';
import Select from '../../../components/Select';
import TableComponent from '../../../components/Table/Table';
import { ModeObject, getMode } from '../../../helper/mode';
import { useSorting } from '../../../helper/sort';
import { validatorDefaultMessages } from '../../../helper/validador';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';
import {
  createGroup,
  getGroup,
  groupSelected,
  groupSelectedClear,
  updateGroup,
  listLinkedDevice,
  groupDevicesClean,
} from '../../../store/group';
import { DeviceUserType } from '../../../types/deviceUser';
import { ListMetadata } from '../../../types/generic_list';

interface UpdateAttachDeviceType {
  serverAttach: boolean;
  chanceAttach: boolean;
  device: DeviceUserType;
}

const EditGroup = () => {
  const { id } = useParams<{ id: string }>();
  const history = useHistory();
  const dispatch = useAppDispatch();

  const { toaster, group, linkedDevices, metadataLinkedDevices } =
    useAppSelector((state) => state.group);
  const {
    user: { company },
  } = useAppSelector((state) => state.auth);
  const [search, setSearch] = useState('');
  const [updateAttachedDevices, setUpdateAttachedDevices] = useState<{
    [K in number]?: UpdateAttachDeviceType;
  }>({});

  const CRUDMode = getMode(id);

  const intl = useIntl();

  const isAttachServer = (linkedDevice: DeviceUserType) => !!linkedDevice.group;

  const simpleValidator = useRef(
    new SimpleReactValidator({
      messages: {
        ...validatorDefaultMessages(intl),
      },
    })
  );

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    dispatch(groupSelected({ [e.target.name]: e.target.value }));
    simpleValidator.current.showMessages();
  };

  useEffect(() => {
    dispatch(
      listLinkedDevice({ ...metadataLinkedDevices, page: 1 }, parseInt(id), {
        search,
      })
    );
    return () => {
      dispatch(groupDevicesClean());
    };
  }, [id, search]);

  useEffect(() => {
    if (id) {
      dispatch(getGroup(parseInt(id)));
    }
    return () => {
      dispatch(groupSelectedClear());
    };
  }, [id]);

  useEffect(() => {
    if (toaster) {
      history.push(routes.groups.manage);
    }
  }, [toaster]);

  useEffect(() => {
    dispatch(groupSelected({ companyId: parseInt(company?.id.toString()) }));
  }, [company?.id]);

  const handlePrimary = () => {
    const groupToDispatch = {
      ...group,
      addDeviceUserIds: Object.keys(updateAttachedDevices)
        .map((keyUpdate) => updateAttachedDevices[+keyUpdate])
        .filter(
          (deviceInfo) => !deviceInfo.serverAttach && deviceInfo.chanceAttach
        )
        .map((deviceInfo) => deviceInfo.device.id),
      deleteDeviceUserIds:
        (CRUDMode === ModeObject.UPDATE &&
          Object.keys(updateAttachedDevices)
            .map((keyUpdate) => updateAttachedDevices[+keyUpdate])
            .filter(
              (deviceInfo) => deviceInfo.serverAttach && deviceInfo.chanceAttach
            )
            .map((deviceInfo) => deviceInfo.device.id)) ||
        [],
    };

    if (CRUDMode === ModeObject.CREATE) {
      dispatch(createGroup(groupToDispatch));
    } else {
      dispatch(updateGroup(groupToDispatch));
    }
  };

  const handleSecundary = () => {
    history.push(routes.groups.manage);
  };

  const handleLinkedMetadata = (newMetadata: ListMetadata) => {
    dispatch(
      listLinkedDevice(
        {
          ...metadataLinkedDevices,
          ...newMetadata,
        },
        parseInt(id)
      )
    );
  };

  const handleCheboxChange = (linkedDevice: DeviceUserType) => {
    const updateDevice = {
      serverAttach: isAttachServer(linkedDevice),
      chanceAttach: !updateAttachedDevices?.[linkedDevice.id]?.chanceAttach,
      device: linkedDevice,
    };
    setUpdateAttachedDevices({
      ...updateAttachedDevices,
      [linkedDevice.id]: updateDevice.chanceAttach ? updateDevice : null,
    });
  };

  const isAtiveCheckboxDevice = (
    linked: DeviceUserType,
    update: UpdateAttachDeviceType
  ) =>
    !update
      ? isAttachServer(linked)
      : update.serverAttach !== update.chanceAttach; // operator !== is equal to xor (exclusive or)

  const rowsDevices = linkedDevices
    // TODO filtro de somente marcados ou somente desmaracado
    .map((linkedDevice) => ({
      cells: [
        {
          field: 'actions',
          value: '',
          transform: () => {
            return (
              <Checkbox
                fontSize="sm"
                color="gray.500"
                isChecked={isAtiveCheckboxDevice(
                  linkedDevice,
                  updateAttachedDevices[linkedDevice.id]
                )}
                onChange={() => handleCheboxChange(linkedDevice)}
              />
            );
          },
        },
        {
          field: 'name',
          value: linkedDevice.name,
        },
        {
          field: 'phoneNumber',
          value: linkedDevice.phoneNumber,
        },
      ],
    }));

  const columns = useSorting(
    [
      {
        header: '',
        accessor: 'attach',
        canSort: false,
      },
      {
        header: intl.formatMessage({
          id: 'edit_group.column.user',
          defaultMessage: 'Usuário',
        }),
        accessor: 'name',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'edit_group.column.phone',
          defaultMessage: 'Telefone',
        }),
        accessor: 'phone',
        canSort: true,
      },
    ],
    metadataLinkedDevices
  );

  return (
    <>
      <PageTitle
        title={
          CRUDMode === ModeObject.CREATE ? (
            <FormattedMessage id="edit_group.title_new" />
          ) : (
            <FormattedMessage id="edit_group.title_edit" />
          )
        }
        description={
          CRUDMode === ModeObject.CREATE ? (
            <FormattedMessage id="edit_group.description_new" />
          ) : (
            <FormattedMessage id="edit_group.description_edit" />
          )
        }
      />

      <FormContainer
        labelPrimary={
          CRUDMode === ModeObject.CREATE ? (
            <FormattedMessage id="global.register" />
          ) : (
            <FormattedMessage id="global.update" />
          )
        }
        handlePrimary={handlePrimary}
        disabledPrimary={!simpleValidator.current.allValid()}
        labelSecundary={<FormattedMessage id="global.cancel" />}
        handleSecundary={handleSecundary}
      >
        <Box d="flex" flexDirection="row">
          <FormControl
            w="376px"
            mr="24px"
            textLabel={<FormattedMessage id="global.company_name" />}
          >
            <Select disabled value={company?.id}>
              <option value={company?.id}>{company?.name}</option>
            </Select>
          </FormControl>
          <FormControl
            w="376px"
            textLabel={<FormattedMessage id="edit_group.new_group" />}
          >
            <Input
              inputProps={{
                name: 'name',
                value: group.name || '',
                onChange: handleInputChange,
              }}
            />
            {simpleValidator.current.message(
              'name',
              group.name,
              'required|alpha_num_dash_space'
            )}
          </FormControl>
        </Box>

        <FormSubtitle
          subtitle={<FormattedMessage id="edit_group.attach_user" />}
        >
          <PageActions initialSearch={search} onSearch={setSearch} />

          <Box w="90%">
            <TableComponent
              headerColumns={columns}
              rows={rowsDevices}
              handleSort={handleLinkedMetadata}
            />
            <PagePagination
              pagination={metadataLinkedDevices}
              onPageChange={handleLinkedMetadata}
            />
          </Box>
        </FormSubtitle>
      </FormContainer>
    </>
  );
};

export default EditGroup;
