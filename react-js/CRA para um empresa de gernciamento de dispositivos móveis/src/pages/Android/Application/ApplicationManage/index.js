import { ArrowDownIcon, ArrowUpIcon } from '@chakra-ui/icons';
import {
  Box,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  Checkbox,
  chakra,
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
  Button,
  Tag,
} from '@chakra-ui/react';
import React, { useState, useRef } from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { useTable, useSortBy } from 'react-table';

import Heading from '../../../../components/Heading';
import Copy from '../../../../components/Icons/Copy';
import DeleteIcon from '../../../../components/Icons/Delete';
import Download from '../../../../components/Icons/Download';
import Edit from '../../../../components/Icons/Edit';
import Search from '../../../../components/Icons/Search';
import ThreeDotsIcon from '../../../../components/Icons/ThreeDots';
import Input from '../../../../components/Input';
import Pagination from '../../../../components/PagePagination';
import Text from '../../../../components/Text';
import Toaster from '../../../../components/Toaster';
import {
  searchApplication,
  closeApplicationToaster,
  checkInstallApplication,
  checkUninstallApplication,
  sendApplication,
  closeSentToaster,
  destroyApplication,
} from '../../../../store/application';
import ConfirmDestroy from './confirmDestroy';
import SendRule from './sendRule';

const ApplicationManage = () => {
  const dispatch = useDispatch();
  const { applications } = useSelector((state) => state.application);
  const [trColor, setTrColor] = useState(-1);
  const [open, setOpen] = useState(false);
  const [selected, setSelected] = useState(null);
  const [destroy, setDestroy] = useState(false);

  const { filter, pagination, showToaster, check, showToasterSent } =
    applications;

  const iconRef = useRef();

  const changeColor = (irow) => {
    setTrColor(irow);
  };

  const send = (data) => {
    dispatch(sendApplication(data));
    setOpen(false);
  };

  const verifySend = () => {
    return !(
      Object.values(check.install).some((selected) => selected) ||
      Object.values(check.uninstall).some((selected) => selected)
    );
  };

  const openDestroy = (id) => {
    setSelected(id);
    setDestroy(true);
  };

  const destroyUser = () => {
    dispatch(destroyApplication(selected));
    setDestroy(false);
  };

  const selectionRow = ({ row, cell }) => {
    if (cell.column.id === 'install') {
      return (
        <Checkbox
          onChange={({ target }) => {
            dispatch(
              checkInstallApplication({
                [row.original.id]: target.checked,
              })
            );
          }}
        />
      );
    } else {
      return (
        <Checkbox
          onChange={({ target }) => {
            dispatch(
              checkUninstallApplication({
                [row.original.id]: target.checked,
              })
            );
          }}
        />
      );
    }
  };

  const handleSearch = ({ target }) => {
    dispatch(searchApplication(target.value));
  };

  const columns = React.useMemo(
    () => [
      {
        Header: 'Instalar',
        id: 'install',
        Cell: selectionRow,
      },
      {
        Header: 'Desinstalar',
        id: 'uninstall',
        Cell: selectionRow,
      },
      {
        Header: 'URL',
        accessor: 'url',
      },
      {
        Header: 'Nombre del paquete',
        accessor: 'package_name',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data: applications.list }, useSortBy);

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="application_manage.title" />
          <Tag
            backgroundColor="white"
            size="lg"
            variant="subtle"
            color="gray.300"
            m="10px"
            ml="20px"
            fontSize="xs"
          >
            <FormattedMessage id="application_manage.title_tag" />
          </Tag>
        </Heading>
        <Text width="90%">
          <FormattedMessage id="application_manage.title_text" />
        </Text>
      </Box>
      <Toaster
        open={showToaster}
        onClose={() => dispatch(closeApplicationToaster())}
      >
        <FormattedMessage id="application_manage.toaster_success" />
      </Toaster>
      <Toaster
        open={showToasterSent}
        onClose={() => dispatch(closeSentToaster())}
      >
        <FormattedMessage id="application_manage.toaster_sent_success" />
      </Toaster>
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

          <Button
            color="blue.500"
            borderColor="#0190fe"
            variant="outline"
            h="45px"
            w="176px"
            mr="40px"
            disabled={verifySend()}
            onClick={() => setOpen(true)}
          >
            <FormattedMessage id="global.send" />
          </Button>
          <Button colorScheme="blue" h="45px" w="176px">
            <Link to="/android/register-application">
              <FormattedMessage id="global.register_new" />
            </Link>
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
                              to={`/android/history-application/${row.original.id}`}
                            >
                              <Edit boxSize={6} mr="5px" />
                              <FormattedMessage id="global.history" />
                            </Link>
                          </MenuItem>
                          <MenuItem
                            color="red.500"
                            fontSize="sm"
                            onClick={() => openDestroy(row.original.id)}
                          >
                            <DeleteIcon boxSize={6} mr="5px" />
                            <FormattedMessage id="global.remove_record" />
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
      <ConfirmDestroy
        open={destroy}
        onClose={() => setDestroy(false)}
        onDestroy={destroyUser}
      />
      <SendRule open={open} onClose={() => setOpen(false)} onSend={send} />
      <Pagination pagination={pagination} />
    </>
  );
};

export default ApplicationManage;
