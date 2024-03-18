import { ListItem, Box } from '@chakra-ui/react';
import { any } from 'prop-types';

const propTypes = {
  children: any,
};

function TimelineItemTitle({ children }) {
  return (
    <ListItem
      listStyleType="none"
      display="flex"
      position="relative"
      minHeight="73px"
    >
      <Box color="#6e6e78" fontSize="14px" mr="auto" textAlign="right" flex="1">
        <Box as="p">&nbsp;</Box>
      </Box>
      <Box
        flex="0"
        display="flex"
        alignItems="center"
        flexDirection="column"
        m="0 20px"
      >
        <Box
          as="span"
          display="flex"
          p="8px"
          alignSelf="baseline"
          borderStyle="solid"
          borderWidth="1px"
          borderRadius="5px"
          color="#6e6e78"
          borderColor="#ffffff"
          backgroundColor="#ffffff"
          w="140px"
          h="50px"
          alignItems="center"
          justifyContent="center"
        >
          {children}
        </Box>
        <Box as="span" width="2px" backgroundColor="#d7d7dc" flexGrow={1} />
      </Box>
      <Box color="#6e6e78" fontSize="14px" flex="1">
        <Box as="p">&nbsp;</Box>
      </Box>
    </ListItem>
  );
}

TimelineItemTitle.propTypes = propTypes;
export default TimelineItemTitle;
