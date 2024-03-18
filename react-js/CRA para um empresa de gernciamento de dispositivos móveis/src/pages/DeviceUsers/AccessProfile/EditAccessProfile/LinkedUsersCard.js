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
  Divider,
} from '@chakra-ui/react';
import React from 'react';
import { useTable, useSortBy } from 'react-table';

import Card from '../../../../components/Card';
import Copy from '../../../../components/Icons/Copy';
import Delete from '../../../../components/Icons/Delete';
import Download from '../../../../components/Icons/Download';
import Search from '../../../../components/Icons/Search';
import Input from '../../../../components/Input';
import Pagination from '../../../../components/PagePagination';
import Select from '../../../../components/Select';
import Text from '../../../../components/Text';

const LinkedUsersCard = () => {
  const data = React.useMemo(
    () => [
      {
        company: 'Vostok One',
        group: 'Analytics',
        user: 'Samsung SM J4 v142',
        phone: '+ 55 (51) 9 8224 8751',
      },
      {
        company: 'ACME LTDA',
        group: 'Analytics',
        user: 'Huawei emui MAR LX3A v142',
        phone: '+ 55 (51) 9 8224 8751',
      },
    ],
    []
  );

  const columns = React.useMemo(
    () => [
      {
        Header: 'Empresa',
        accessor: 'company',
      },
      {
        Header: 'Grupo',
        accessor: 'group',
      },
      {
        Header: 'Usuário',
        accessor: 'user',
      },
      {
        Header: 'Teléfono',
        accessor: 'phone',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data }, useSortBy);

  const pagination = 1;

  return (
    <>
      <Card w="100%" mt="2%">
        <Text margin="0px 0px 20px 0px" fontSize="2xl" fontWeight="600">
          Vincular usuário
        </Text>
        <Box>
          <Divider orientation="horizontal" borderColor="gray.600" mb="1.5%" />
        </Box>
        <Box d="flex" flexDirection="row">
          <FormControl w="376px" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              Selecione o usuário
            </FormLabel>
            <Select placeholder="" backgroundColor="white" />
          </FormControl>
          <Button
            colorScheme="blue"
            variant="outline"
            mt="30px"
            h="45px"
            w="176px"
          >
            Vincular
          </Button>
        </Box>
      </Card>
      <Box d="flex" flexDirection="column" m="1.5% 0 3% 0">
        <Text m="0" fontSize="md" fontWeight="600">
          Usuários vinculados ao perfil de acesso
        </Text>
        <Box mt="1%">
          <Divider orientation="horizontal" borderColor="gray.600" mb="1.5%" />
        </Box>
        <Box d="flex" flexDirection="row" justifyContent="space-between">
          <Box w="376px" ml="1.5%">
            <Input
              inputProps={{ backgroundColor: 'white' }}
              leftElement={<Search boxSize={6} color="gray.600" />}
            />
          </Box>
          <Box alignSelf="center" mr="1.5%">
            <Button
              color="blue.500"
              variant="link"
              mr="40px"
              fontWeight="normal"
            >
              <Download boxSize={6} />
              Excel
            </Button>
            <Button
              color="blue.500"
              variant="link"
              mr="40px"
              fontWeight="normal"
            >
              <Copy boxSize={6} />
              Copiar
            </Button>
          </Box>
        </Box>
      </Box>
      <Box>
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
                      key={idata}
                      {...cell.getCellProps()}
                      isNumeric={cell.column.isNumeric}
                    >
                      {cell.render('Cell')}
                    </Td>
                  ))}
                  <Td
                    textAlign="right"
                    paddingLeft="1rem"
                    paddingRight="1rem"
                    borderTopRightRadius={irow == 0 ? '10px' : null}
                    borderBottomRightRadius={
                      irow == rows.length - 1 ? '10px' : null
                    }
                  >
                    <>
                      <Box>
                        <Delete boxSize={6} mr="5px" />
                      </Box>
                    </>
                  </Td>
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

export default LinkedUsersCard;
