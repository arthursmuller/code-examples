import { Box, Text } from '@chakra-ui/react';
import { FormattedMessage } from 'react-intl';

import { PaginationPayload } from '../../types/generic_list';
import Select from '../Select';
import Pagination from './Pagination';

interface PagePaginationProps {
  pagination: PaginationPayload;
  onPageChange: (payload: Partial<PaginationPayload>) => void;
  pageSizeFixed?: number;
}

const PagePagination = ({ pagination, onPageChange, pageSizeFixed }: PagePaginationProps) => {
  return (
    <Box
      w="90%"
      d="flex"
      flexDirection="row"
      mt="40px"
      alignItems="center"
    >
      <Box d="flex" flexDirection="row" ml="1%" alignItems="center">
        <Box minW="200px">
          <Text color="gray.500" alignSelf="center">
            <FormattedMessage
              id="global.showing_x_of"
              values={{
                rowInit: (pagination.page - 1) * pagination.pageSize + 1 || 0,
                rowEnd: pagination.page * pagination.pageSize || 0,
                totalRowa: pagination.totalItems || 0,
              }}
            />
          </Text>
        </Box>
        <Box width="127px">
          <Select
            color="gray.300"
            backgroundColor="white"
            ml="10%"
            value={pageSizeFixed || pagination.pageSize}
            disabled={pageSizeFixed}
            onChange={(e: React.ChangeEvent<HTMLSelectElement>) =>
              onPageChange({ pageSize: parseInt(e.target.value), page: 1 })
            }
          >
            <option value="10">10</option>
            <option value="20">20</option>
            <option value="40">40</option>
            <option value="80">80</option>
          </Select>
        </Box>
      </Box>
      <Pagination
        onPageChange={(page: number) => onPageChange({ page })}
        totalCount={pagination.totalItems || 0}
        currentPage={pagination.page || 0}
        pageSize={pagination.pageSize}
      />
    </Box>
  );
};

export default PagePagination;
