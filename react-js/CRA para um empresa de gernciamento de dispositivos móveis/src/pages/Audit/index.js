import { ArrowDownIcon, ArrowUpIcon } from '@chakra-ui/icons';
import {
  Box,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  FormControl,
  FormLabel,
  chakra,
  Button,
} from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';
import { useTable, useSortBy } from 'react-table';

import Card from '../../components/Card';
import Datepicker from '../../components/Datepicker';
import Heading from '../../components/Heading';
import Copy from '../../components/Icons/Copy';
import Download from '../../components/Icons/Download';
import Search from '../../components/Icons/Search';
import Input from '../../components/Input';
import Pagination from '../../components/PagePagination';
import Select from '../../components/Select';
import Text from '../../components/Text';
import { filterUpdate } from '../../store/audit';

const Audit = () => {
  const dispatch = useDispatch();
  const { filter, audits } = useSelector((state) => state.audit);
  const pagination = 1;

  const handleFilterChange = (field) => (value) => {
    dispatch(filterUpdate({ ...filter, [field]: value }));
  };

  const handleTextFilterChange =
    (field) =>
    ({ target }) => {
      dispatch(filterUpdate({ ...filter, [field]: target.value }));
    };

  const columns = React.useMemo(
    () => [
      {
        Header: 'Atividade',
        accessor: 'activity',
      },
      {
        Header: 'Dispositivo',
        accessor: 'device',
      },
      {
        Header: 'Usuário',
        accessor: 'user',
      },
      {
        Header: 'Fecha',
        accessor: 'date',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data: audits }, useSortBy);

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="audit.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="audit.title_text" />
        </Text>
      </Box>
      <Card mt="3%">
        <Text
          margin="0 0 20px 0"
          color="gray.600"
          fontSize="md"
          fontWeight="bold"
        >
          <FormattedMessage id="audit.filter" />
        </Text>
        <Box d="flex" flexDirection="row">
          <FormControl w="100%" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="audit.start_date" />
            </FormLabel>
            <Datepicker
              selected={filter.start_date}
              onChange={handleFilterChange('start_date')}
            />
          </FormControl>
          <FormControl w="100%" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="audit.end_date" />
            </FormLabel>
            <Datepicker
              selected={filter.end_date}
              onChange={handleFilterChange('end_date')}
            />
          </FormControl>
          <FormControl w="100%">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="audit.user" />
            </FormLabel>
            <Select placeholder="Todos" backgroundColor="white" />
          </FormControl>
        </Box>
      </Card>
      <Box
        w="90%"
        d="flex"
        flexDirection="row"
        justifyContent="space-between"
        m="2% 0 2% 0"
      >
        <Box w="376px" ml="1.5%">
          <Input
            inputProps={{
              backgroundColor: 'white',
              value: filter.text_search,
              onChange: handleTextFilterChange('text_search'),
            }}
            leftElement={<Search boxSize={6} color="gray.600" />}
          />
        </Box>
        <Box alignSelf="center">
          <Button color="blue.500" variant="link" mr="40px" fontWeight="normal">
            <Download boxSize={6} />
            <FormattedMessage id="global.excel" />
          </Button>
          <Button color="blue.500" variant="link" mr="40px" fontWeight="normal">
            <Copy boxSize={6} />
            <FormattedMessage id="global.copy" />
          </Button>
        </Box>
      </Box>
      <Box w="90%">
        <Table {...getTableProps()}>
          <Thead>
            {headerGroups.map((headerGroup, i, ih) => (
              <Tr key={i} {...headerGroup.getHeaderGroupProps()}>
                {headerGroup.headers.map((column) => (
                  <Th
                    color="black.500"
                    fontSize="sm"
                    fontWeight="normal"
                    key={ih}
                    {...column.getHeaderProps(column.getSortByToggleProps())}
                  >
                    {column.render('Header')}
                    <chakra.span pl="4">
                      {column.isSorted ? (
                        column.isSortedDesc ? (
                          <ArrowDownIcon color="gray.500" />
                        ) : (
                          <ArrowUpIcon color="gray.500" />
                        )
                      ) : null}
                    </chakra.span>
                  </Th>
                ))}
              </Tr>
            ))}
          </Thead>
          <Tbody bg="white" borderRadius="10px" {...getTableBodyProps()}>
            {rows.map((row, irow, idata) => {
              prepareRow(row);
              return (
                <Tr key={irow} {...row.getRowProps()}>
                  {row.cells.map((cell, icell) => (
                    <Td
                      color="gray.500"
                      fontSize="sm"
                      paddingLeft="1rem"
                      paddingRight="1rem"
                      borderTopLeftRadius={
                        irow == 0 && icell == 0 ? '10px' : null
                      }
                      borderBottomLeftRadius={
                        irow == rows.length - 1 && icell == 0 ? '10px' : null
                      }
                      borderTopRightRadius={
                        irow == 0 && icell == row.cells.length - 1
                          ? '10px'
                          : null
                      }
                      borderBottomRightRadius={
                        irow == rows.length - 1 && icell == row.cells.length - 1
                          ? '10px'
                          : null
                      }
                      key={idata}
                      {...cell.getCellProps()}
                      isNumeric={cell.column.isNumeric}
                    >
                      {cell.render('Cell')}
                    </Td>
                  ))}
                </Tr>
              );
            })}
          </Tbody>
        </Table>
      </Box>
      <Pagination pagination={pagination} />
    </>
  );
};

export default Audit;
