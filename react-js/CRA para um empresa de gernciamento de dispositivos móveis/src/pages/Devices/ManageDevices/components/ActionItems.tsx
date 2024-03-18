import { DeleteIcon, NotAllowedIcon } from '@chakra-ui/icons';
import { Box, Divider } from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import Alert, { AlertHtml } from '../../../../components/Alert';
import Card from '../../../../components/Card';
import GraphIcon from '../../../../components/Icons/Graph';
import LockIcon from '../../../../components/Icons/Lock';
import RefreshIcon from '../../../../components/Icons/Refresh';
import MenuItem from '../../../../components/TableActions/MenuItem';
import Text from '../../../../components/Text';
import { useAppDispatch } from '../../../../hooks/useRedux';
import {
  blockDevice,
  removeDevice,
  unblockDevice,
} from '../../../../store/device';
import { DeviceType } from '../../../../types/device';
import alertNewPassword from './alertNewPassword';

interface ActionItemsProps {
  device?: DeviceType;
  modalOpen?: (modalName: string, device: DeviceType) => void;
}

const ActionItems = ({ device, modalOpen }: ActionItemsProps) => {
  const intl = useIntl();
  const dispatch = useAppDispatch();

  const idDevice = device?.id;
  const deviceDisconnected = false;
  const deviceBlock = false;

  const itemBattery = {
    icon: <GraphIcon boxSize={6} color="white" mr="5px" />,
    text: intl.formatMessage({ id: 'devices.action.battery' }),
    onClick: () => false,
  };
  const itemStorage = {
    icon: <GraphIcon boxSize={6} color="white" mr="5px" />,
    text: intl.formatMessage({ id: 'devices.action.storage' }),
    onClick: () => false,
  };
  const itemRemove = {
    icon: <DeleteIcon boxSize={6} mr="5px" />,
    text: intl.formatMessage({
      id: deviceDisconnected
        ? 'devices.action.remove'
        : 'devices.action.change',
    }),
    onClick: () =>
      Alert({
        onConfirm: () => dispatch(removeDevice(idDevice)),
        html: (
          <AlertHtml
            irreversible={intl.formatMessage({ id: 'devices.alert.irreversible' })}
            text={intl.formatMessage({ id: 'devices.alert.remove.text' })}
          />
        ),
        confirmButtonText: intl.formatMessage({
          id: 'devices.alert.remove.button',
        }),
        cancelButtonText: intl.formatMessage({ id: 'devices.alert.cancel' }),
      }),
    color: 'red.500',
  };
  const itemBlock = {
    icon: <NotAllowedIcon boxSize={6} mr="5px" />,
    text: intl.formatMessage({ id: 'devices.action.block' }),
    onClick: () =>
      Alert({
        onConfirm: () => dispatch(blockDevice(idDevice)),
        html: (
          <AlertHtml
            text={intl.formatMessage({ id: 'devices.alert.block.text' })}
          />
        ),
        confirmButtonText: intl.formatMessage({
          id: 'devices.alert.block.button',
        }),
        cancelButtonText: intl.formatMessage({ id: 'devices.alert.cancel' }),
      }),
  };
  const itemUnblock = {
    icon: <NotAllowedIcon boxSize={6} mr="5px" />,
    text: intl.formatMessage({ id: 'devices.action.unblock' }),
    onClick: () =>
      Alert({
        onConfirm: () => dispatch(unblockDevice(idDevice)),
        html: (
          <AlertHtml
            text={intl.formatMessage({ id: 'devices.alert.unblock.text' })}
          />
        ),
        confirmButtonText: intl.formatMessage({
          id: 'devices.alert.unblock.button',
        }),
        cancelButtonText: intl.formatMessage({ id: 'devices.alert.cancel' }),
      }),
  };
  const itemNewPassword = {
    icon: <LockIcon boxSize={6} mr="5px" />,
    text: intl.formatMessage({ id: 'devices.action.newpassword' }),
    onClick: () => alertNewPassword(intl, dispatch, idDevice),
  };
  const itemPasswordHistoric = {
    icon: <RefreshIcon boxSize={6} mr="5px" />,
    text: intl.formatMessage({ id: 'devices.action.passwordHistoric' }),
    onClick: () => modalOpen('passwordHistoric', device),
  };

  return (
    <>
      <MenuItem {...itemBattery} />
      <MenuItem {...itemStorage} />
      <MenuItem {...itemRemove} />

      <Box m="5px">
        <Divider borderColor="gray.600" orientation="horizontal" />
      </Box>
      {deviceDisconnected && (
        <Card
          bg="gray.400"
          w="300px"
          mt="15px"
          mb="10px"
          ml="4%"
          textAlign="center"
          borderRadius="5px"
        >
          <Text m="0" as="em" fontWeight="600">
            <FormattedMessage id="devices.action.disconnected" />
          </Text>
        </Card>
      )}
      <MenuItem
        {...(deviceBlock ? itemBlock : itemUnblock)}
        isDisabled={deviceDisconnected}
      />
      <MenuItem {...itemNewPassword} isDisabled={deviceDisconnected} />
      <MenuItem {...itemPasswordHistoric} isDisabled={deviceDisconnected} />
    </>
  );
};

export default ActionItems;
