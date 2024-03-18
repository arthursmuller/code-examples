import { Box, Text } from '@chakra-ui/react';
import PropTypes from 'prop-types';
import { FormattedMessage } from 'react-intl';

import Button from '../Button';
import Expand from '../Icons/Expand';
import Select from '../Select';

const Pagination = ({ pagination }) => {
  return (
    <Box
      w="90%"
      d="flex"
      flexDirection="row"
      mt="40px"
      justifyContent="space-between"
    >
      <Box d="flex" flexDirection="row" ml="1%">
        <Text color="gray.500" alignSelf="center">
          <FormattedMessage id="global.showing_x_of" /> {pagination.total_pages}
        </Text>
        <Box width="127px">
          <Select color="gray.300" backgroundColor="white" ml="10%">
            <option value="10" selected>
              10
            </option>
            <option value="20">20</option>
            <option value="40">40</option>
            <option value="80">80</option>
          </Select>
        </Box>
      </Box>
      <Button variant="link" color="blue" fontWeight="normal">
        <Expand boxSize={6} />
        <FormattedMessage id="global.load_more" />
      </Button>
      <Box w="287.63px" />
    </Box>
  );
};

Pagination.propTypes = {
  pagination: PropTypes.any,
};

export default Pagination;
