import { ChevronDownIcon, ChevronRightIcon } from '@chakra-ui/icons';
import { Box } from '@chakra-ui/react';
import React, { useEffect, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import Modal from '../../../components/Modal';
import PageActions from '../../../components/PageActions';
import PageFilter from '../../../components/PageFilter';
import PagePagination from '../../../components/PagePagination';
import PageTitle from '../../../components/PageTitle';
import SelectAutocomplete from '../../../components/SelectAutocomplete';
import Table from '../../../components/Table/Table';
import TableActions from '../../../components/TableActions';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import {
  listDeviceManufacturer,
  listDeviceModel,
  listDevices,
} from '../../../store/device';
import { listDeviceUserToFilter } from '../../../store/deviceUser';
import { DeviceType, ManufacturerType, ModelType } from '../../../types/device';
import { DeviceUserType } from '../../../types/deviceUser';
import ActionItems from './components/ActionItems';
import alertNewPassword from './components/alertNewPassword';
import Inventory from './components/Inventory';
import LastPasswordsModal from './components/LastPasswords';
import ListDrillDown from './components/ListDrillDown';

const ManageDevices = () => {
  const dispatch = useAppDispatch();
  const { devices, metadata, manufacturers, models } = useAppSelector(
    (state) => state.device
  );
  const { devicesUsersToFilter } = useAppSelector(
    (state) => state.deviceUser
  );
  const [modalType, setModalType] = useState('');
  const [selectedDevice, setSelectedDevice] = useState<DeviceType>();

  const intl = useIntl();

  const openModal = (modalType: string, device: DeviceType) => {
    setModalType(modalType);
    setSelectedDevice(device);
  };

  const [filterManufacturer, setFilterManufacturer] =
    useState<ManufacturerType>();
  const [filterModel, setFilterModel] = useState<ModelType>();
  const [filterDeviceUser, setFilterDeviceUser] = useState<DeviceUserType>();
  const [filterSearch, setFilterSearch] = useState('');

  const allFilters = {
    manufacturer: filterManufacturer?.manufacturer,
    model: filterModel?.model,
    userId: filterDeviceUser?.id,
    search: filterSearch,
  };

  useEffect(() => {
    dispatch(listDeviceManufacturer());
    dispatch(listDeviceModel());
    dispatch(listDeviceUserToFilter());
  }, []);

  useEffect(() => {
    dispatch(listDevices(metadata, allFilters));
  }, [filterManufacturer, filterModel, filterDeviceUser, filterSearch]);

  const handleFilterManufacturer = (newFilter: ManufacturerType) => {
    setFilterManufacturer(newFilter);
    dispatch(listDeviceManufacturer());
  };
  const handleFilterModel = (newFilter: ModelType) => {
    setFilterModel(newFilter);
    dispatch(listDeviceModel());
  };
  const handleFilterPhone = (newFilter: DeviceUserType) => {
    setFilterDeviceUser(newFilter);
    dispatch(listDeviceUserToFilter());
  };

  const handleFilterPhoneChange = (value) => {
    dispatch(listDeviceUserToFilter({ search: value }));
  };
  const handleFilterModelChange = (value) => {
    dispatch(listDeviceModel({ search: value }));
  };
  const handleFilterManufacturerChange = (value) => {
    dispatch(listDeviceManufacturer({ search: value }));
  };

  const handleMetadata = (value) => {
    dispatch(listDevices({ ...metadata, ...value }, allFilters));
  };

  const headerColumns = useSorting(
    [
      {
        header: '',
        accessor: 'handle_expander',
      },
      {
        header: intl.formatMessage({ id: 'devices.columns.user' }),
        accessor: 'user',
        canSort: true,
      },
      {
        header: intl.formatMessage({ id: 'devices.columns.federalCode' }),
        accessor: 'federalCode',
        canSort: true,
        paddingLeft: 0,
        paddingRight: 0,
      },
      {
        header: intl.formatMessage({ id: 'devices.columns.phone_number' }),
        accessor: 'phone_number',
        canSort: true,
      },
      {
        header: intl.formatMessage({ id: 'devices.columns.imei' }),
        accessor: 'imei',
        canSort: true,
      },
      {
        header: intl.formatMessage({ id: 'devices.columns.iccid' }),
        accessor: 'iccid',
        canSort: true,
      },
      {
        header: intl.formatMessage({ id: 'devices.columns.manufacturer' }),
        accessor: 'manufacturer',
        canSort: true,
      },
      {
        header: intl.formatMessage({ id: 'devices.columns.model' }),
        accessor: 'model',
        canSort: true,
        paddingRight: 0,
      },
      {
        header: '',
        accessor: 'actions',
      },
    ],
    metadata
  );

  const rows = devices.map((device) => ({
    cells: [
      {
        field: 'handle_expander',
        value: null,
        transform: ({ isExpanded, toggleExpanded }) => (
          <span>
            {isExpanded ? (
              <ChevronDownIcon
                boxSize="5"
                color="#d7d7dc"
                onClick={toggleExpanded}
              />
            ) : (
              <ChevronRightIcon
                boxSize="5"
                color="#d7d7dc"
                onClick={toggleExpanded}
              />
            )}
          </span>
        ),
        chackraProps: { width: 0 },
      },
      {
        value: device.name,
        field: 'user',
      },
      {
        value: device.federalCode,
        field: 'federalCode',
      },
      {
        value: device.phoneNumber,
        field: 'phone_number',
      },
      {
        value: device.imei,
        field: 'imei',
      },
      {
        value: device.iccid,
        field: 'iccid',
      },
      {
        value: device.manufacturer,
        field: 'manufacturer',
      },
      {
        value: device.model,
        field: 'model',
      },
      {
        value: '',
        field: 'actions',
        transform: () => (
          <TableActions
            moreItems={<ActionItems device={device} modalOpen={openModal} />}
          />
        ),
      },
      {
        field: 'expander',
        value: null,
        transform: () => <ListDrillDown device={device} />,
      },
    ],
  }));

  return (
    <>
      {modalType === 'passwordHistoric' && (
        <Modal
          isOpen={modalType === 'passwordHistoric'}
          onClose={() => setModalType('')}
        >
          <LastPasswordsModal
            device={selectedDevice}
            onCancel={() => setModalType('')}
            openNewPassword={() => {
              setModalType('');
              alertNewPassword(intl, dispatch, selectedDevice.id);
            }}
          />
        </Modal>
      )}
      <PageTitle
        title={<FormattedMessage id="devices.title" />}
        description={<FormattedMessage id="devices.sub_title" />}
      />
      <Inventory />
      <FormContainer>
        <PageFilter>
          <FormControl
            textLabel={<FormattedMessage id="devices.manufacturer" />}
          >
            <SelectAutocomplete
              options={manufacturers}
              value={filterManufacturer}
              getOptionLabel={(option) => option.manufacturer}
              onChange={handleFilterManufacturer}
              onInputChange={handleFilterManufacturerChange}
            />
          </FormControl>
          <FormControl textLabel={<FormattedMessage id="devices.model" />}>
            <SelectAutocomplete
              options={models}
              value={filterModel}
              getOptionLabel={(option) => option.model}
              onChange={handleFilterModel}
              onInputChange={handleFilterModelChange}
            />
          </FormControl>
          <FormControl textLabel={<FormattedMessage id="devices.phone" />}>
            <SelectAutocomplete
              options={devicesUsersToFilter}
              value={filterDeviceUser}
              onChange={handleFilterPhone}
              onInputChange={handleFilterPhoneChange}
              getOptionLabel={(option) =>
                `${option.name || ''} ${option.phoneNumber}`
              }
              placeholder={<FormattedMessage id="global.phone" />}
            />
          </FormControl>
        </PageFilter>
      </FormContainer>
      <PageActions
        initialSearch={filterSearch}
        onSearch={(e) => setFilterSearch(e)}
      />
      <Box w="90%">
        <Table
          headerColumns={headerColumns}
          rows={rows}
          handleSort={handleMetadata}
        />
      </Box>
      <PagePagination pagination={metadata} onPageChange={handleMetadata} />
    </>
  );
};

export default ManageDevices;
