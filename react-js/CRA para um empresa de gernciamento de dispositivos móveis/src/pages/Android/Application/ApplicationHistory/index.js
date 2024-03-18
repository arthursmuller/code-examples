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
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
  Button,
  Divider,
} from '@chakra-ui/react';
import React, { useState, useRef } from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import { Link } from 'react-router-dom';
import { useTable, useSortBy } from 'react-table';

import Heading from '../../../../components/Heading';
import Copy from '../../../../components/Icons/Copy';
import Download from '../../../../components/Icons/Download';
import Edit from '../../../../components/Icons/Edit';
import Eye from '../../../../components/Icons/Eye';
import Search from '../../../../components/Icons/Search';
import ThreeDotsIcon from '../../../../components/Icons/ThreeDots';
import Input from '../../../../components/Input';
import Pagination from '../../../../components/PagePagination';
import Text from '../../../../components/Text';
import { searchApplication } from '../../../../store/application';

const ApplicationHistory = () => {
  let { id } = useParams();
  const dispatch = useDispatch();
  const { applications } = useSelector((state) => state.application);
  const [trColor, setTrColor] = useState(-1);

  const { filter, pagination, history, list } = applications;
  const application = list.find((app) => app.id == id);
  const iconRef = useRef();

  const changeColor = (irow) => {
    setTrColor(irow);
  };

  const handleSearch = ({ target }) => {
    dispatch(searchApplication(target.value));
  };

  const columns = React.useMemo(
    () => [
      {
        Header: 'Usuário',
        accessor: 'user',
      },
      {
        Header: 'Tipo de mensagem',
        accessor: 'message_type',
      },
      {
        Header: 'Data',
        accessor: 'date',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data: history }, useSortBy);

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="application_history.title" />
          <strong>{application.package_name}</strong>
        </Heading>
        <Text width="90%">
          <FormattedMessage id="application_history.title_text" />
        </Text>
      </Box>
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
                <Tr
                  key={irow}
                  {...row.getRowProps()}
                  bg={trColor !== irow ? 'white' : 'gray.400'}
                >
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
                      <Menu placement="top" onClose={() => changeColor(-1)}>
                        <MenuButton
                          as={ThreeDotsIcon}
                          boxSize={6}
                          ref={iconRef}
                          cursor="pointer"
                          onClick={() => changeColor(irow)}
                        >
                          Actions
                        </MenuButton>
                        <MenuList>
                          <MenuItem color="black.500" fontSize="sm">
                            <Link
                              to={`/android/history-application/${application.id}/status/${row.original.id}`}
                            >
                              <Eye boxSize={6} mr="5px" />
                              <FormattedMessage id="application_history.sent_status" />
                            </Link>
                          </MenuItem>
                        </MenuList>
                      </Menu>
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

export default ApplicationHistory;
