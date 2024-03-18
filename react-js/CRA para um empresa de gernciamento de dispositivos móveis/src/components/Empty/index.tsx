import { Box } from '@chakra-ui/react';

import Card from '../Card';
import AlertModal from '../Icons/AlertModal';
import Text from '../Text';

const Empty = () => (
  <Card mt="40px">
    <Box d="flex" flexDirection="row" alignItems="center">
      <AlertModal boxSize={6} />
      <Text m="0" fontSize="md" ml="10px" as="i">
        Nenhum resultado encontrado.
      </Text>
    </Box>
  </Card>
);

export default Empty;
