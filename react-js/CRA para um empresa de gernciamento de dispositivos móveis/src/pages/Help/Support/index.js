import { Box } from '@chakra-ui/react';
import React from 'react';

import Card from '../../../components/Card';
import Heading from '../../../components/Heading';
import Download from '../../../components/Icons/Download';
import Text from '../../../components/Text';

const Support = () => {
  return (
    <>
      <Box>
        <Heading>Documentos y materiales de apoyo</Heading>
        <Text width="90%">
          Use the /disputes resource to list disputes, create disputes, show
          dispute details, and partially a dispute. Normally, an agent at PayPal
          creates disputes but now you can run test cases in the sandbox that
          create disputes.
        </Text>
      </Box>
      <Card mt="1%">
        <Text
          m="0"
          fontSize="sm"
          color="blue.500"
          fontWeight="400"
          cursor="pointer"
        >
          <Download boxSize={6} mr="0.3%" />
          Manual del Administrador - Control Movil Telcel.docx
        </Text>
      </Card>
      <Card mt="1%">
        <Text
          m="0"
          fontSize="sm"
          color="blue.500"
          fontWeight="400"
          cursor="pointer"
        >
          <Download boxSize={6} mr="0.3%" />
          Manual de Instalación - Control Móvil Telcel.pdf
        </Text>
      </Card>
      <Card mt="1%">
        <Text
          m="0"
          fontSize="sm"
          color="blue.500"
          fontWeight="400"
          cursor="pointer"
        >
          <Download boxSize={6} mr="0.3%" />
          Manual del Administrador - Control Móvil Telcel.pdf
        </Text>
      </Card>
    </>
  );
};

export default Support;
