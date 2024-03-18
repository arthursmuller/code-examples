import { FC } from 'react';

import { Center, Flex, Text, Icon, Button } from '@chakra-ui/react';

import {
  StatusCheckFilledIcon,
  StatusCloseErrorIcon,
} from '@pcf/design-system-icons';
import { TelefoneClienteExibicaoModel } from '@pcf/core';

interface PhoneVerificationBagdeProps {
  phone: TelefoneClienteExibicaoModel;
  onVerify?: () => void;
}

export const PhoneVerificationBagde: FC<PhoneVerificationBagdeProps> = ({
  phone,
  onVerify,
}) => {
  return (
    <Flex>
      {phone.confirmado ? (
        <Text color="white" textStyle="regular12" mr={2}>
          Verificado
        </Text>
      ) : (
        <Button
          size="xs"
          color="error.washed"
          onClick={onVerify}
          variant="link"
          mr="2"
        >
          Não verificado
        </Button>
      )}

      {!phone.confirmado ? (
        <Center
          bg={phone.confirmado ? 'success.regular' : 'error.regular'}
          borderRadius="full"
          w="14.33px"
          h="14.33px"
        >
          <Icon as={StatusCloseErrorIcon} w="7px" h="5.6px" />
        </Center>
      ) : (
        <Icon
          as={StatusCheckFilledIcon}
          color="success.regular"
          w="14.33px"
          h="14.33px"
        />
      )}
    </Flex>
  );
};
