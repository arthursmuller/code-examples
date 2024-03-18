import { ListItem, Box } from '@chakra-ui/react';
import { any } from 'prop-types';

const propTypes = {
  leftContent: any,
  rightContent: any,
};

function TimelineItem({ leftContent, rightContent, markColor }) {
  return (
    <ListItem
      listStyleType="none"
      display="flex"
      position="relative"
      minHeight="43px"
    >
      <Box color="#6e6e78" fontSize="14px" mr="auto" textAlign="right" flex="1">
        <Box as="p">{leftContent}</Box>
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
          borderRadius="50%"
          color={markColor ? markColor : '#d7d7dc'}
          borderColor={markColor ? markColor : '#d7d7dc'}
          backgroundColor={markColor ? markColor : '#d7d7dc'}
        />
        <Box as="span" width="2px" backgroundColor="#d7d7dc" flexGrow={1} />
      </Box>
      <Box color="#6e6e78" fontSize="14px" flex="1">
        <Box as="p">{rightContent}</Box>
      </Box>
    </ListItem>
  );
}

TimelineItem.propTypes = propTypes;
export default TimelineItem;
