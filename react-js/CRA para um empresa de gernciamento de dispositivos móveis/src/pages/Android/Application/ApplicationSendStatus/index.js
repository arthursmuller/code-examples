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
import React, { useState, useRef } from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import { useTable, useSortBy } from 'react-table';

import Card from '../../../../components/Card';
import Heading from '../../../../components/Heading';
import Copy from '../../../../components/Icons/Copy';
import Download from '../../../../components/Icons/Download';
import Search from '../../../../components/Icons/Search';
import Input from '../../../../components/Input';
import Pagination from '../../../../components/PagePagination';
import Text from '../../../../components/Text';
import { searchApplication } from '../../../../store/application';

const ApplicationSendStatus = () => {
  let { id, history: history_id } = useParams();
  const dispatch = useDispatch();
  const { applications } = useSelector((state) => state.application);

  const { filter, pagination, history, list } = applications;
  const application = list.find((app) => app.id == id);
  const history_obj = history.find((hs) => hs.id == history_id);

  const handleSearch = ({ target }) => {
    dispatch(searchApplication(target.value));
  };

  const StatusCell = ({ cell: { value } }) => {
    return (
      <chakra.span color={value ? 'green.500' : ''}>
        <FormattedMessage
          id={value ? 'application_status.sent' : 'application_status.sending'}
        />
      </chakra.span>
    );
  };

  const columns = React.useMemo(
    () => [
      {
        Header: 'Usuário',
        accessor: 'user',
      },
      {
        Header: 'Telefone',
        accessor: 'phone',
      },
      {
        Header: 'Fecha',
        accessor: 'date',
      },
      {
        Header: 'Estado',
        accessor: 'status',
        Cell: StatusCell,
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data: history_obj.status }, useSortBy);

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="application_status.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="application_status.title_text" />
        </Text>
      </Box>
      <Card w="90%">
        <Text fontSize="md" fontWeight="600" p="0" m="0">
          URL
        </Text>
        <Divider mt="30px" mb="30px" />
        <Text fontSize="sm" p="0" m="0">
          {application.url}
        </Text>
      </Card>
      <Divider w="90%" />
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
              value: filter.search,
              onChange: handleSearch,
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
                <Tr key={irow} {...row.getRowProps()} bg="white">
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

export default ApplicationSendStatus;
