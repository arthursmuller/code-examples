import { Box, Flex, Switch } from '@chakra-ui/react';
import { FormattedMessage } from 'react-intl';

import Heading from '../../../components/Heading';
import Text from '../../../components/Text';

function Header() {
  return (
    <>
      <Flex justify="space-between" alignItems="center">
        <Heading>Boa tarde, Karolina</Heading>
        <Box>
          <Switch id="email-alerts" mr="10px" />
          <Box as="span" fontSize="14px" letterSpacing="0.56px" color="#6e6e78">
            Roaming internacional
          </Box>
        </Box>
      </Flex>
      <Text w="90%">
        <FormattedMessage id="dashboard.description" />
      </Text>
    </>
  );
}

export default Header;
