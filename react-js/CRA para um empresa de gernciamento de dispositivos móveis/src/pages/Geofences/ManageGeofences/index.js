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
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  Divider,
} from '@chakra-ui/react';
import React, { useState, useRef } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { useTable, useSortBy } from 'react-table';

import Heading from '../../../components/Heading';
import AlertModal from '../../../components/Icons/AlertModal';
import Copy from '../../../components/Icons/Copy';
import Delete from '../../../components/Icons/Delete';
import Download from '../../../components/Icons/Download';
import Edit from '../../../components/Icons/Edit';
import Search from '../../../components/Icons/Search';
import ThreeDotsIcon from '../../../components/Icons/ThreeDots';
import Input from '../../../components/Input';
import Pagination from '../../../components/PagePagination';
import Text from '../../../components/Text';
import Toaster from '../../../components/Toaster';
import { closeCreateToaster } from '../../../store/geofence';

const ManageGeofences = () => {
  const intl = useIntl();
  const dispatch = useDispatch();
  const geofence = useSelector((state) => state.geofence);
  const [open, setOpen] = useState(false);
  const [trColor, setTrColor] = useState(-1);

  const { showToasterAdd, showToasterEdit } = geofence;

  const iconRef = useRef();

  const changeColor = (irow) => {
    setTrColor(irow);
  };

  const data = React.useMemo(
    () => [
      {
        geofence_name: 'Vostok One',
        adress:
          'Av. Sen. Tarso Dutra, 605 - São João, Porto Alegre - RS, 90690-140, Brasil',
        geofence_radius: '200 m',
        status: 'Ativo',
      },
      {
        geofence_name: 'ACME LTDA',
        adress:
          'Av. Sen. Tarso Dutra, 605 - São João, Porto Alegre - RS, 90690-140, Brasil',
        geofence_radius: '150 m',
        status: 'Inativo',
      },
    ],
    []
  );

  const columns = React.useMemo(
    () => [
      {
        Header: intl.formatMessage({ id: 'geofence.table_header.name' }),
        accessor: 'geofence_name',
      },
      {
        Header: intl.formatMessage({ id: 'geofence.table_header.adress' }),
        accessor: 'adress',
      },
      {
        Header: intl.formatMessage({ id: 'geofence.table_header.radius' }),
        accessor: 'geofence_radius',
      },
      {
        Header: intl.formatMessage({ id: 'geofence.table_header.status' }),
        accessor: 'status',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data }, useSortBy);

  const pagination = 1;

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="geofence.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="geofence.description" />
        </Text>
      </Box>
      <Toaster
        mb="3%"
        open={showToasterAdd || showToasterEdit}
        onClose={() => dispatch(closeCreateToaster())}
      >
        {showToasterAdd && <FormattedMessage id="geofence.register_success" />}
        {showToasterEdit && <FormattedMessage id="geofence.edit_success" />}
      </Toaster>
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
          <Link to="/register-geofence">
            <Button
              bg="blue.500"
              color="white"
              h="45px"
              w="176px"
              fontWeight="400"
              _hover={{ opacity: '70%' }}
            >
              <FormattedMessage id="global.register_new" />
            </Button>
          </Link>
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
                            <Link to="/edit-geofence">
                              <Edit boxSize={6} mr="5px" />
                              <FormattedMessage id="geofence.edit_button" />
                            </Link>
                          </MenuItem>
                          <MenuItem
                            color="red.500"
                            fontSize="sm"
                            onClick={setOpen}
                          >
                            <Delete boxSize={6} mr="5px" />
                            <FormattedMessage id="geofence.delete_button" />
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

      <Modal isOpen={open} onClose={() => setOpen(false)} isCentered>
        <ModalOverlay />
        <ModalContent w="424px" h="359px">
          <ModalHeader d="flex" flexDirection="column" alignItems="center">
            <AlertModal boxSize={24} mt="20px" />
          </ModalHeader>
          <ModalBody d="flex" flexDirection="column" alignItems="center">
            <Text fontWeight="bold" fontSize="md" color="black.500">
              <FormattedMessage id="geofence.modal_title" />
            </Text>
            <Text fontWeight="normal" fontSize="sm" mt="10px" color="black.500">
              <FormattedMessage id="geofence.modal_description" />
            </Text>
          </ModalBody>
          <ModalFooter d="flex" flexDirection="column" alignSelf="center">
            <Box mb="19px" w="424px">
              <Divider borderColor="gray.600" orientation="horizontal" />
            </Box>
            <Box d="flex" flexDirection="row">
              <Box mr="14px">
                <Button
                  w="180px"
                  h="45px"
                  fontWeight="normal"
                  colorScheme="blue"
                  onClick={() => setOpen(false)}
                >
                  <FormattedMessage id="global.remove" />
                </Button>
              </Box>
              <Box>
                <Button
                  w="180px"
                  h="45px"
                  fontWeight="normal"
                  variant="outline"
                  colorScheme="blue"
                  borderColor="#0190fe"
                  onClick={() => setOpen(false)}
                >
                  <FormattedMessage id="global.cancel" />
                </Button>
              </Box>
            </Box>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </>
  );
};

export default ManageGeofences;
