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
  Divider,
  Switch,
  FormControl,
  FormLabel,
} from '@chakra-ui/react';
import React from 'react';
import { useTable, useSortBy } from 'react-table';

import Text from '../../../../components/Text';

const PermissionsCard = () => {
  const data = React.useMemo(
    () => [
      {
        Functionality: 'Nome da funcionalidade',
        Description: 'Lorem ipsum dolor sit amet',
        enabled: (
          <FormControl>
            <FormLabel htmlFor="email-alerts" mb="0"></FormLabel>
            <Switch id="email-alerts" />
          </FormControl>
        ),
      },
      {
        Functionality: 'Nome da funcionalidade',
        Description: 'Lorem ipsum dolor sit amet',
        enabled: (
          <FormControl>
            <FormLabel htmlFor="email-alerts" mb="0"></FormLabel>
            <Switch id="email-alerts" />
          </FormControl>
        ),
      },
    ],
    []
  );

  const columns = React.useMemo(
    () => [
      {
        Header: 'Funcionalidade',
        accessor: 'Functionality',
      },
      {
        Header: 'Descrição',
        accessor: 'Description',
      },
      {
        Header: 'Habilitado',
        accessor: 'enabled',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data }, useSortBy);

  return (
    <>
      <Box w="100%" mt="2%">
        <Box>
          <Box>
            <Text m="0" fontSize="md" fontWeight="600">
              Area do sistema
            </Text>
          </Box>
          <Box mt="1%">
            <Divider orientation="horizontal" borderColor="gray.600" mb="2%" />
          </Box>
          <Box w="100%">
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
                        {...column.getHeaderProps(
                          column.getSortByToggleProps()
                        )}
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
                            irow == rows.length - 1 && icell == 0
                              ? '10px'
                              : null
                          }
                          borderTopRightRadius={
                            irow == 0 && icell == row.cells.length - 1
                              ? '10px'
                              : null
                          }
                          borderBottomRightRadius={
                            irow == rows.length - 1 &&
                            icell == row.cells.length - 1
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
        </Box>
        <Box mt="3%">
          <Box>
            <Text m="0" fontSize="md" fontWeight="600">
              Area do sistema
            </Text>
          </Box>
          <Box mt="1%">
            <Divider orientation="horizontal" borderColor="gray.600" mb="2%" />
          </Box>
          <Box w="100%">
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
                        {...column.getHeaderProps(
                          column.getSortByToggleProps()
                        )}
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
                            irow == rows.length - 1 && icell == 0
                              ? '10px'
                              : null
                          }
                          borderTopRightRadius={
                            irow == 0 && icell == row.cells.length - 1
                              ? '10px'
                              : null
                          }
                          borderBottomRightRadius={
                            irow == rows.length - 1 &&
                            icell == row.cells.length - 1
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
        </Box>
        <Box mt="3%">
          <Box>
            <Text m="0" fontSize="md" fontWeight="600">
              Area do sistema
            </Text>
          </Box>
          <Box mt="1%">
            <Divider orientation="horizontal" borderColor="gray.600" mb="2%" />
          </Box>
          <Box w="100%">
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
                        {...column.getHeaderProps(
                          column.getSortByToggleProps()
                        )}
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
                            irow == rows.length - 1 && icell == 0
                              ? '10px'
                              : null
                          }
                          borderTopRightRadius={
                            irow == 0 && icell == row.cells.length - 1
                              ? '10px'
                              : null
                          }
                          borderBottomRightRadius={
                            irow == rows.length - 1 &&
                            icell == row.cells.length - 1
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
        </Box>
        <Box mt="3%">
          <Box>
            <Text m="0" fontSize="md" fontWeight="600">
              Area do sistema
            </Text>
          </Box>
          <Box mt="1%">
            <Divider orientation="horizontal" borderColor="gray.600" mb="2%" />
          </Box>
          <Box w="100%">
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
                        {...column.getHeaderProps(
                          column.getSortByToggleProps()
                        )}
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
                            irow == rows.length - 1 && icell == 0
                              ? '10px'
                              : null
                          }
                          borderTopRightRadius={
                            irow == 0 && icell == row.cells.length - 1
                              ? '10px'
                              : null
                          }
                          borderBottomRightRadius={
                            irow == rows.length - 1 &&
                            icell == row.cells.length - 1
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
        </Box>
      </Box>
    </>
  );
};

export default PermissionsCard;
