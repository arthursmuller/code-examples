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
import { useTable, useSortBy } from 'react-table';

import Card from '../../../components/Card';
import Heading from '../../../components/Heading';
import Copy from '../../../components/Icons/Copy';
import Date from '../../../components/Icons/Date';
import Download from '../../../components/Icons/Download';
import Expand from '../../../components/Icons/Expand';
import Search from '../../../components/Icons/Search';
import Input from '../../../components/Input';
import Select from '../../../components/Select';
import Text from '../../../components/Text';

const ManageGPS = () => {
  const data = React.useMemo(
    () => [
      {
        user: 'Alcatel v142',
        phone_number: '5551982248751',
        gps: 'Conectado',
        date: '05/01/2021 16:41:10 UTC',
      },
      {
        user: 'Alcatel v142',
        phone_number: '5551982248751',
        gps: '862563041017611',
        date: '05/01/2021 16:41:20 UTC',
      },
    ],
    []
  );

  const columns = React.useMemo(
    () => [
      {
        Header: 'Usuário',
        accessor: 'user',
      },
      {
        Header: 'Teléfono',
        accessor: 'phone_number',
      },
      {
        Header: 'GPS',
        accessor: 'gps',
      },
      {
        Header: 'Fecha',
        accessor: 'date',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data }, useSortBy);

  return (
    <>
      <Box>
        <Heading>GPS</Heading>
        <Text width="90%">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
          minim veniam, quis nostrud exercitation ullamco laboris nisi.
        </Text>
      </Box>
      <Card mt="3%">
        <Text
          margin="0 0 20px 0"
          color="gray.600"
          fontSize="md"
          fontWeight="bold"
        >
          Filtrar
        </Text>
        <Box d="flex" flexDirection="row">
          <FormControl w="100%" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              Fecha de
            </FormLabel>
            <Input rightElement={<Date boxSize={6} color="gray.500" />} />
          </FormControl>
          <FormControl w="100%" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              Fecha hasta
            </FormLabel>
            <Input rightElement={<Date boxSize={6} color="gray.500" />} />
          </FormControl>
          <FormControl w="100%">
            <FormLabel fontSize="sm" color="gray.500">
              Usuario
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
            inputProps={{ backgroundColor: 'white' }}
            leftElement={<Search boxSize={6} color="gray.600" />}
          />
        </Box>
        <Box alignSelf="center">
          <Button color="blue.500" variant="link" mr="40px" fontWeight="normal">
            <Download boxSize={6} />
            Excel
          </Button>
          <Button color="blue.500" variant="link" mr="40px" fontWeight="normal">
            <Copy boxSize={6} />
            Copiar
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
                  {row.cells.map((cell) => (
                    <Td
                      color="gray.500"
                      fontSize="sm"
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
      <Box
        w="90%"
        d="flex"
        flexDirection="row"
        mt="40px"
        justifyContent="space-between"
      >
        <Box d="flex" flexDirection="row" ml="1%">
          <Text color="gray.500" alignSelf="center">
            Mostrando 1-10 de 55
          </Text>
          <Box width="127px">
            <Select
              placeholder="10"
              color="gray.300"
              backgroundColor="white"
              ml="10%"
            />
          </Box>
        </Box>
        <Button variant="link" color="blue.500" fontWeight="normal">
          <Expand boxSize={6} />
          Carregar mais
        </Button>
        <Box w="287.63px" />
      </Box>
    </>
  );
};

export default ManageGPS;
