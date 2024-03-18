import { Box, FormControl, FormLabel, Divider, Button } from '@chakra-ui/react';
import React from 'react';

import Card from '../../../../components/Card';
import Heading from '../../../../components/Heading';
import Input from '../../../../components/Input';
import Text from '../../../../components/Text';

const RegisterAccessProfile = () => {
  return (
    <>
      <Box>
        <Heading>Cadastrar novo perfil de acesso</Heading>
        <Text width="90%">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
          minim veniam, quis nostrud exercitation ullamco laboris nisi.
        </Text>
      </Box>
      <Card>
        <Box d="flex" flexDirection="row">
          <FormControl w="376px" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              Nombre del perfil de acesso
            </FormLabel>
            <Input />
          </FormControl>
          <FormControl w="376px">
            <FormLabel fontSize="sm" color="gray.500">
              Descrição
            </FormLabel>
            <Input />
          </FormControl>
        </Box>
        <Box mt="3%">
          <Divider orientation="horizontal" mb="1.5%" />
        </Box>
        <Box d="flex" flexDirection="row" alignItems="center">
          <Button bg="blue.500" color="white" h="45px" w="176px">
            Cadastar
          </Button>
          <Divider
            orientation="vertical"
            h="22px"
            ml="1%"
            borderColor="gray.600"
          />
          <Button variant="ghost" color="blue.500">
            Cancelar
          </Button>
        </Box>
      </Card>
    </>
  );
};

export default RegisterAccessProfile;
