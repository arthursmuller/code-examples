import {
  Box,
} from '@chakra-ui/react';
import React, { useEffect } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';

import LockIcon from '../../../../components/Icons/Lock';
import { ModalBody, ModalFooter, ModalHeader } from '../../../../components/Modal';
import Table from '../../../../components/Table/Table';
import { Body, Header } from '../../../../components/Table/TableInterfaces';
import Text from '../../../../components/Text';
import { useAppDispatch, useAppSelector } from '../../../../hooks/useRedux';
import { listPasswordHistoric } from '../../../../store/device';
import { DeviceType } from '../../../../types/device';

interface LastPasswordsModalProps {
  device: DeviceType;
  openNewPassword: () => void;
  onCancel: () => void;
}

const LastPasswordsModal = ({ device, openNewPassword, onCancel }: LastPasswordsModalProps) => {
  const dispatch = useAppDispatch();
  const { passwordsHistoric } = useAppSelector(state => state.device);

  const intl = useIntl();

  useEffect(() => {
    if(device.id) {
      dispatch(listPasswordHistoric(device.id))
    }
  }, [device.id])

  const columns: Header[] = [
    {
      header: intl.formatMessage({ id: 'devices.passwordHistoric.column.password' }),
      accessor: 'password',
    },
    {
      header: intl.formatMessage({ id: 'devices.passwordHistoric.column.date' }),
      accessor: 'date',
    },
  ];
  const data: Body[] = passwordsHistoric.map((historic) => ({
    cells: [
      {
        field: 'password',
        value: historic.password,
      },
      {
        field: 'date',
        value: new Date(historic.date).toLocaleString(),
      },
    ],
  }));

  return (
    <>
      <ModalHeader>
        <Text fontWeight="600" fontSize="2xl" color="gray.500">
          <FormattedMessage id="devices.passwordHistoric.title" />
        </Text>
      </ModalHeader>
      <ModalBody>
        <Box h="330px" overflowY="auto">
          <Table rows={data} headerColumns={columns} />
        </Box>
        <Box d="flex" alignSelf="flex-start">
          <Text
            fontSize="sm"
            color="blue.500"
            cursor="pointer"
            onClick={() => openNewPassword()}
          >
            <LockIcon color="blue" boxSize={6} mr="5px" />
            <FormattedMessage id="devices.passwordHistoric.newpassword" />
          </Text>
        </Box>
      </ModalBody>
      <ModalFooter onConfirm={onCancel} labelConfirm="Fechar" />
    </>
  );
};

export default LastPasswordsModal;
