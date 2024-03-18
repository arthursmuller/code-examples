import { ArrowDownIcon, ArrowUpIcon } from '@chakra-ui/icons';
import {
  Box,
  Divider,
  FormControl,
  FormLabel,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  chakra,
} from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage } from 'react-intl';
import { useTable, useSortBy } from 'react-table';

import Button from '../../../../components/Button';
import AlertIcon from '../../../../components/Icons/AlertModal';
import Delete from '../../../../components/Icons/Delete';
import Select from '../../../../components/Select';
import Text from '../../../../components/Text';

const Block = () => {
  return (
    <Box
      d="flex"
      flexDirection="row"
      alignItems="center"
      border="1px solid #f2f4f8"
      p="10px 20px"
      mt="1%"
    >
      <AlertIcon boxSize={6} />
      <Text m="0" as="i" color="gray.300">
        Nenhuma geofence adicionada a esta regra
      </Text>
    </Box>
  );
};

const Allow = () => {
  const data = React.useMemo(
    () => [
      {
        geofence_name: 'Matriz Telcel',
        adress: 'Júlio de Castilhos, 185 - Nova Petrópolis',
        geofence_radius: '300m',
      },
    ],
    []
  );
  const columns = React.useMemo(
    () => [
      {
        Header: 'Nome da geofence',
        accessor: 'geofence_name',
      },
      {
        Header: 'Endereço',
        accessor: 'adress',
      },
      {
        Header: 'Raio da geofence',
        accessor: 'geofence_radius',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data }, useSortBy);
  return (
    <Box mt="1%">
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
                    <Delete boxSize="6" />
                  </>
                </Td>
              </Tr>
            );
          })}
        </Tbody>
      </Table>
    </Box>
  );
};

const GeofenceTab = ({ action }) => {
  return (
    <>
      <Box>
        <Text m="2% 0% 0% 0%" fontWeight="600">
          <FormattedMessage id="block_application.geofence.geofence" />
        </Text>
        <Divider
          orientation="horizontal"
          borderColor="gray.600"
          m="0.5% 0% 1.5% 0%"
        />
      </Box>
      <Box>
        <FormControl w="576px">
          <FormLabel fontSize="sm" color="gray.500">
            <FormattedMessage id="block_application.geofence.select" />
          </FormLabel>
          <Box d="flex" flexDirection="row">
            <Select />
            <Button
              h="45px"
              w="176px"
              ml="2%"
              colorScheme="blue"
              borderColor="blue.500"
              color="blue.500"
              variant="outline"
            >
              <FormattedMessage id="global.add" />
            </Button>
          </Box>
        </FormControl>
      </Box>
      <Box>{action === 'block' ? <Block /> : <Allow />}</Box>
    </>
  );
};

export default GeofenceTab;
