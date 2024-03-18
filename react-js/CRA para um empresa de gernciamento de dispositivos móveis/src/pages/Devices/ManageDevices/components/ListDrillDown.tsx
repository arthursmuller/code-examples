import { Flex } from '@chakra-ui/react';
import { useIntl } from 'react-intl';

import { formatBytesTo } from '../../../../helper/bytes';
import { DeviceType } from '../../../../types/device';
import ItemList from './ItemListDrill';

interface ListDrillDownProps {
  device: DeviceType;
}

function ListDrillDown({ device }: ListDrillDownProps) {
  const intl = useIntl();

  const customColor = (value: boolean) => (value ? 'green.500' : 'red.500');

  const customValue = {
    locked: intl.formatMessage({
      id:
        device.locked === true
          ? 'devices.values.locked.block'
          : 'devices.values.locked.unblock',
    }),
    compliance: intl.formatMessage({
      id:
        device.compliance === true
          ? 'devices.values.compliance.compliance'
          : 'devices.values.compliance.notCompliance',
    }),
    powerStatus: intl.formatMessage({
      id:
        device.powerStatus === true
          ? 'devices.values.powerStatus.connected'
          : 'devices.values.powerStatus.notConnected',
    }),
    gpsStatus: intl.formatMessage({
      id:
        device.gpsStatus === true
          ? 'devices.values.gpsStatus.connected'
          : 'devices.values.gpsStatus.notConnected',
    }),
    modeOwner: intl.formatMessage({
      id:
        device.modeOwner === true
          ? 'devices.values.modeOwner.owner'
          : 'devices.values.modeOwner.notOwner',
    }),
    updatedAt: `${intl.formatDate(
      new Date(device.updatedAt)
    )} ${intl.formatTime(new Date(device.updatedAt))}`,
    activatedAt: `${intl.formatDate(
      new Date(device.activatedAt)
    )} ${intl.formatTime(new Date(device.activatedAt))}`,
    freeMemory: formatBytesTo({ bytes: device.freeMemory, to: 'GB' }),
    battery: `${device.battery}%`,
  };

  function transformValue(object: DeviceType, key: keyof DeviceType) {
    return !customValue[key] ? object[key] : customValue[key];
  }

  return (
    <Flex p="30px 0 30px 70px">
      <Flex flexDirection="column">
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.compliance' })}
          value={transformValue(device, 'compliance')}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.oSVersion' })}
          value={transformValue(device, 'oSVersion')}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.datamobVersion' })}
          value={transformValue(device, 'datamobVersion')}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.battery' })}
          value={transformValue(device, 'battery')}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.freeMemory' })}
          value={transformValue(device, 'freeMemory')}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.powerStatus' })}
          value={transformValue(device, 'powerStatus')}
          color={customColor(device.powerStatus)}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.group' })}
          value={device.group?.name}
        />
      </Flex>
      <Flex flexDirection="column" ml="100px">
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.locked' })}
          value={transformValue(device, 'locked')}
          color={customColor(device.locked)}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.gpsStatus' })}
          value={transformValue(device, 'gpsStatus')}
          color={customColor(device.gpsStatus)}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.unlockPassword' })}
          value={transformValue(device, 'unlockPassword')}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.updatedAt' })}
          value={transformValue(device, 'updatedAt')}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.activatedAt' })}
          value={transformValue(device, 'activatedAt')}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.modeOwner' })}
          value={transformValue(device, 'modeOwner')}
        />
        <ItemList
          label={intl.formatMessage({ id: 'devices.columns.subGroup' })}
          value={device.subGroup?.name}
        />
      </Flex>
    </Flex>
  );
}

export default ListDrillDown;
