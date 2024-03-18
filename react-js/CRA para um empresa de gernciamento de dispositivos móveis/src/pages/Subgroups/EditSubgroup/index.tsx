import { Box, Divider, Checkbox } from '@chakra-ui/react';
import React, { useState, useRef, useEffect } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useHistory, useParams } from 'react-router';
import SimpleReactValidator from 'simple-react-validator';

import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import Input from '../../../components/Input';
import PageActions from '../../../components/PageActions';
import PagePagination from '../../../components/PagePagination';
import PageTitle from '../../../components/PageTitle';
import SelectAutocomplete from '../../../components/SelectAutocomplete';
import TableComponent from '../../../components/Table/Table';
import Text from '../../../components/Text';
import { SUBGROUP_PAGE_SIZE_FIXED } from '../../../helper';
import { sanitizeFilter } from '../../../helper/filter';
import { getMode, ModeObject } from '../../../helper/mode';
import { useSorting } from '../../../helper/sort';
import { validatorDefaultMessages } from '../../../helper/validador';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';
import { listGroupsToFilter } from '../../../store/group';
import {
  createSubgroup,
  updateSubgroup,
  getSubgroup,
  listLinkedDevice,
  subgroupSelectedClear,
  subgroupSelected,
  subgroupDevicesClean,
} from '../../../store/subgroup';
import { DeviceUserType } from '../../../types/deviceUser';
import { ListMetadata } from '../../../types/generic_list';
import { GroupType } from '../../../types/group';

interface UpdateAttachDeviceType {
  serverAttach: boolean;
  chanceAttach: boolean;
  device: DeviceUserType;
}

const convertObjToArr: <T>(obj: Record<string | number, T>) => T[] = (obj) =>
  Object.keys(obj).map((key) => obj[key]);

const EditSubGroup = () => {
  const { id } = useParams<{ id: string }>();
  const history = useHistory();
  const dispatch = useAppDispatch();

  const { subgroup, toaster, linkedDevices, metadataLinkedDevices } =
    useAppSelector((state) => state.subgroup);
  const {
    user: { company },
  } = useAppSelector((state) => state.auth);
  const { groupsToFilter } = useAppSelector((state) => state.group);

  const [search, setSearch] = useState('');
  const [updateAttachedDevices, setUpdateAttachedDevices] = useState<{
    [K in number]?: UpdateAttachDeviceType;
  }>({});

  const CRUDMode = getMode(id);
  const intl = useIntl();
  const disableGruopSelect = CRUDMode === ModeObject.UPDATE;
  const simpleValidator = useRef(
    new SimpleReactValidator({
      messages: {
        ...validatorDefaultMessages(intl),
      },
    })
  );

  const isAttachServer = (linkedDevice: DeviceUserType) =>
    !!linkedDevice.subGroup;

  const handleFilterGroup = (newFilter: GroupType) => {
    dispatch(subgroupSelected({ group: newFilter }));
    simpleValidator.current.showMessageFor('group');
  };

  const handleFilterGroupChange = (value) => {
    dispatch(listGroupsToFilter({ search: value }));
  };

  useEffect(() => {
    dispatch(subgroupSelected({ companyId: parseInt(company?.id.toString()) }));
  }, [company?.id]);

  useEffect(() => {
    if (subgroup.group?.id) {
      dispatch(
        listLinkedDevice(
          subgroup.group?.id,
          parseInt(id),
          { ...metadataLinkedDevices, page: 1 },
          { search }
        )
      );
    }
    return () => {
      dispatch(subgroupDevicesClean());
    };
  }, [subgroup.group?.id, id, search]);

  useEffect(() => {
    if (!disableGruopSelect) {
      dispatch(listGroupsToFilter());
    }
  }, []);

  useEffect(() => {
    if (id) {
      dispatch(getSubgroup(parseInt(id)));
    }
    return () => {
      dispatch(subgroupSelectedClear());
    };
  }, [id]);

  useEffect(() => {
    if (toaster) {
      history.push(routes.subgroups.manage);
    }
  }, [toaster]);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    dispatch(
      subgroupSelected({ ...subgroup, [e.target.name]: e.target.value })
    );
    simpleValidator.current.showMessages();
  };

  const handlePrimary = () => {
    const arrUpdateAttachedDevices = convertObjToArr(updateAttachedDevices);

    const deviceUsersToAdd = arrUpdateAttachedDevices
      .filter(
        (deviceInfo) => deviceInfo?.chanceAttach && !deviceInfo?.serverAttach
      )
      .map((deviceInfo) => deviceInfo?.device.id);

    const deviceUsersToRemove =
      (CRUDMode === ModeObject.UPDATE &&
        arrUpdateAttachedDevices
          .filter(
            (deviceInfo) => deviceInfo?.chanceAttach && deviceInfo?.serverAttach
          )
          .map((deviceInfo) => deviceInfo?.device.id)) ||
      [];

    const subgroupToDispatch = sanitizeFilter({
      ...subgroup,
      groupId: subgroup.group?.id,
      addDeviceUserIds: deviceUsersToAdd.length > 0 && deviceUsersToAdd,
      deleteDeviceUserIds:
        deviceUsersToRemove.length > 0 && deviceUsersToRemove,
    });

    if (CRUDMode === ModeObject.CREATE) {
      dispatch(createSubgroup(subgroupToDispatch));
    } else {
      dispatch(updateSubgroup(subgroupToDispatch));
    }
  };

  const handleSecundary = () => {
    history.push(routes.subgroups.manage);
  };

  const handleLinkedMetadata = (newMetadata: ListMetadata) => {
    dispatch(
      listLinkedDevice(subgroup.group?.id, parseInt(id), {
        ...metadataLinkedDevices,
        ...newMetadata,
      })
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
                isChecked={
                  (!!linkedDevice.subGroup &&
                    !!id &&
                    linkedDevice.subGroup.id == id) ||
                  isAtiveCheckboxDevice(
                    linkedDevice,
                    updateAttachedDevices[linkedDevice.id]
                  )
                }
                onChange={() => handleCheboxChange(linkedDevice)}
              />
            );
          },
          width: '0',
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
        width: '0',
      },
      {
        header: intl.formatMessage({
          id: 'edit_subgroup.column.user',
          defaultMessage: 'Usuário',
        }),
        accessor: 'user',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: 'edit_subgroup.column.phone',
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
        title={<FormattedMessage id="edit_subgroup.title" />}
        description={<FormattedMessage id="edit_subgroup.description" />}
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
            textLabel={<FormattedMessage id="global.company_name" />}
          >
            <SelectAutocomplete options={[company]} value={company} disabled />
          </FormControl>

          <FormControl
            w="376px"
            textLabel={<FormattedMessage id="global.group" />}
          >
            <SelectAutocomplete
              options={groupsToFilter}
              value={subgroup.group}
              onChange={handleFilterGroup}
              onInputChange={handleFilterGroupChange}
              placeholder={<FormattedMessage id="global.group" />}
              disabled={disableGruopSelect}
            />
            {simpleValidator.current.message(
              'group',
              subgroup.group,
              'required'
            )}
          </FormControl>

          <FormControl
            w="376px"
            textLabel={<FormattedMessage id="global.subgroup" />}
          >
            <Input
              inputProps={{
                name: 'name',
                placeholder: intl.formatMessage({ id: 'global.subgroup' }),
                value: subgroup.name || '',
                onChange: handleInputChange,
              }}
            />
            {simpleValidator.current.message(
              'name',
              subgroup.name,
              'required|alpha_num_dash_space'
            )}
          </FormControl>
        </Box>
        <Box mt="3%">
          <Box>
            <Text m="0" fontSize="24px" fontWeight="600">
              <FormattedMessage id="edit_subgroup.attached_users" />
            </Text>
          </Box>
          <Box mt="1%">
            <Divider
              orientation="horizontal"
              mb="1.5%"
              borderColor="gray.600"
            />
          </Box>
          <PageActions initialSearch={search} onSearch={setSearch} />
          <Box w="90%">
            <TableComponent
              headerColumns={columns}
              rows={rowsDevices}
              handleSort={handleLinkedMetadata}
            />
          </Box>
          <PagePagination
            pagination={metadataLinkedDevices}
            onPageChange={handleLinkedMetadata}
            pageSizeFixed={SUBGROUP_PAGE_SIZE_FIXED}
          />
        </Box>
      </FormContainer>
    </>
  );
};

export default EditSubGroup;
