import { Flex, Box } from '@chakra-ui/react';
import { Table, Tr, Td } from '@chakra-ui/react';
import { orderBy } from 'lodash';
import { array } from 'prop-types';

const propTypes = {
  data: array,
};

function BlockedWebsites({ data }) {
  const zeroPad = (num, places) => String(num).padStart(places, '0');
  const renderItems = () => {
    return orderBy(data, ['attempts'], ['desc']).map((item, index) => {
      return (
        <Tr key={index}>
          <Td p="0 0 4px 0" w="100%">
            <Flex
              height="55px"
              opacity="0.6"
              borderRadius="10px"
              backgroundColor="#f2f4f8"
              alignItems="center"
              p="18px 24px"
              mr="12px"
              fontSize="14px"
            >
              {item.website}
            </Flex>
          </Td>
          <Td p="0">
            <Box
              fontSize="12px"
              lineHeight="5"
              letterSpacing="0.48px"
              color="#6e6e78"
            >
              <Box
                as="span"
                flexWrap="nowrap"
                mr="5px"
                fontSize="16px"
                fontWeight="bold"
                letterSpacing="0.64px"
                color={item.color ? item.color : '#282832'}
              >
                {zeroPad(item.attempts, 2)}
              </Box>
              tentativa{parseInt(item.attempts) > 1 ? 's' : ''}
            </Box>
          </Td>
        </Tr>
      );
    });
  };

  return (
    <Flex flexDirection="column">
      {data && <Table variant="unstyled">{renderItems()}</Table>}
    </Flex>
  );
}

BlockedWebsites.propTypes = propTypes;
export default BlockedWebsites;
