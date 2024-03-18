import { ArrowDownIcon, ArrowUpIcon } from '@chakra-ui/icons';
import {
  Box,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  chakra,
  Button,
  Divider,
} from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';
import { useTable, useSortBy } from 'react-table';

import Card from '../../../components/Card';
import Heading from '../../../components/Heading';
import Copy from '../../../components/Icons/Copy';
import Download from '../../../components/Icons/Download';
import Search from '../../../components/Icons/Search';
import Input from '../../../components/Input';
import Pagination from '../../../components/PagePagination';
import Text from '../../../components/Text';

const sharedDetails = () => {
  const dispatch = useDispatch();
  const { documents_detail } = useSelector((state) => state.document);

  const columns = React.useMemo(
    () => [
      {
        Header: 'Usuario',
        accessor: 'user',
      },
      {
        Header: 'Teléfono',
        accessor: 'cellphone',
      },
      {
        Header: 'Fecha',
        accessor: 'date',
      },
      {
        Header: 'Estado',
        accessor: 'state',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data: documents_detail }, useSortBy);

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="document_details.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="document_details.title_text" />
        </Text>
      </Box>
      <Card>
        <Text
          margin="0 0 20px 0"
          color="gray.500"
          fontSize="md"
          fontWeight="600"
        >
          <FormattedMessage id="document_details.doc" />
        </Text>
        <Box>
          <Divider orientation="horizontal" mb="1%" />
        </Box>
        <Box d="flex" flexDirection="row" alignItems="center">
          <Text m="0">https://controlmovil.telcel.com/messages/postman</Text>
        </Box>
      </Card>
      <Box d="flex" flexDirection="column" w="90%">
        <Text m="2% 0% 0% 0%" fontSize="2xl" fontWeight="600" color="gray.500">
          <FormattedMessage id="global.recipient" />
        </Text>
        <Divider orientation="horizontal" mt="1%" />
      </Box>
      <Box
        w="90%"
        d="flex"
        flexDirection="row"
        justifyContent="space-between"
        m="3% 0 3% 0"
      >
        <Box w="376px" ml="1.5%">
          <Input
            inputProps={{ backgroundColor: 'white' }}
            leftElement={<Search boxSize={6} color="gray.600" />}
          />
        </Box>
        <Box alignSelf="center" mr="1.5%">
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
      <Pagination pagination={1} />
    </>
  );
};

export default sharedDetails;
