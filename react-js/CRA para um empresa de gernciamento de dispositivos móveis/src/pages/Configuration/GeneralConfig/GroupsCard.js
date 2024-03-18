import {
  ArrowDownIcon,
  ArrowUpIcon,
  TriangleDownIcon,
  ChevronDownIcon,
  ChevronRightIcon,
} from '@chakra-ui/icons';
import {
  Box,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
  chakra,
  Button,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  Divider,
} from '@chakra-ui/react';
import { get } from 'lodash';
import React, { useState, useRef } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { useTable, useSortBy, useExpanded } from 'react-table';

import AlertModal from '../../../components/Icons/AlertModal';
import Copy from '../../../components/Icons/Copy';
import DeleteIcon from '../../../components/Icons/Delete';
import Download from '../../../components/Icons/Download';
import Edit from '../../../components/Icons/Edit';
import Search from '../../../components/Icons/Search';
import ThreeDotsIcon from '../../../components/Icons/ThreeDots';
import Input from '../../../components/Input';
import Pagination from '../../../components/PagePagination';
import Text from '../../../components/Text';
import Toaster from '../../../components/Toaster';
import { closeGroupToaster } from '../../../store/generalConfig';
import ListDrillDown from './ListDrillDown';

const GroupsCard = () => {
  const intl = useIntl();
  const groupConfig = useSelector((state) => state.generalConfig);
  const dispatch = useDispatch();
  const [trColor, setTrColor] = useState(-1);
  const [open, setOpen] = useState(false);

  const { showToasterGroupCreate, showToasterGroupEdit } = groupConfig;

  const iconRef = useRef();

  const changeColor = (irow) => {
    setTrColor(irow);
  };

  const pagination = 1;

  const data = React.useMemo(
    () => [
      {
        group: 'A teste',
        week_start: 'Lunes',
        week_end: 'Lunes',
        hour_start: '09:00',
        hour_end: '23:59',
        block_out_of_hour: 'Sí',
        gps_hour_start: '09:00',
        gps_hour_end: '23:59',
        drilldown: {
          track_gps: 'Sí',
          gps_precision: 'Definido en el nivel arriba',
          block_wifi: 'No',
          block_url: 'Definido en el nivel arriba',
          block_apps: 'Definido en el nivel arriba',
          warning_email: 'Definido en el nivel arriba',
          hotspot: 'Definido en el nivel arriba',
          safe_start: 'Definido en el nivel arriba',
          add_user: 'Definido en el nivel arriba',
          install_external_sd: 'Definido en el nivel arriba',
        },
      },
      {
        group: 'QA',
        week_start: 'Martes',
        week_end: 'Martes',
        hour_start: '09:00',
        hour_end: '23:59',
        block_out_of_hour: 'No',
        gps_hour_start: '09:00',
        gps_hour_end: '23:59',
        drilldown: {
          track_gps: 'Sí',
          gps_precision: 'Definido en el nivel arriba',
          block_wifi: 'No',
          block_url: 'Definido en el nivel arriba',
          block_apps: 'Definido en el nivel arriba',
          warning_email: 'Definido en el nivel arriba',
          hotspot: 'Definido en el nivel arriba',
          safe_start: 'Definido en el nivel arriba',
          add_user: 'Definido en el nivel arriba',
          install_external_sd: 'Definido en el nivel arriba',
        },
      },
    ],
    []
  );

  const columns = React.useMemo(
    () => [
      {
        Header: () => null,
        id: 'expander',
        Cell: ({ row }) => (
          <span {...row.getToggleRowExpandedProps()}>
            {row.isExpanded ? (
              <ChevronDownIcon boxSize="10" color="#d7d7dc" />
            ) : (
              <ChevronRightIcon boxSize="10" color="#d7d7dc" />
            )}
          </span>
        ),
      },
      {
        Header: intl.formatMessage({ id: 'global.group' }),
        accessor: 'group',
      },
      {
        Header: intl.formatMessage({ id: 'general_config.form.week_start' }),
        accessor: 'week_start',
      },
      {
        Header: intl.formatMessage({ id: 'general_config.form.week_end' }),
        accessor: 'week_end',
      },
      {
        Header: intl.formatMessage({ id: 'general_config.form.hour_start' }),
        accessor: 'hour_start',
      },
      {
        Header: intl.formatMessage({ id: 'general_config.form.hour_end' }),
        accessor: 'hour_end',
      },
      {
        Header: intl.formatMessage({ id: 'general_config.form.total_block' }),
        accessor: 'block_out_of_hour',
      },
      {
        Header: intl.formatMessage({
          id: 'general_config.form.gps_hour_start',
        }),
        accessor: 'gps_hour_start',
      },
      {
        Header: intl.formatMessage({ id: 'general_config.form.gps_hour_end' }),
        accessor: 'gps_hour_end',
      },
    ],
    []
  );

  const {
    getTableProps,
    getTableBodyProps,
    headerGroups,
    rows,
    prepareRow,
    visibleColumns,
    state: { expanded },
  } = useTable({ columns, data }, useSortBy, useExpanded);

  const renderRowSubComponent = (row) => {
    const deviceData = get(row, 'original.drilldown');
    return <ListDrillDown data={deviceData} />;
  };

  return (
    <>
      <Toaster
        w="100%"
        mt="3%"
        open={showToasterGroupCreate || showToasterGroupEdit}
        onClose={() => dispatch(closeGroupToaster())}
      >
        {showToasterGroupCreate && (
          <FormattedMessage id="general_config.group.toaster_create" />
        )}
        {showToasterGroupEdit && (
          <FormattedMessage id="general_config.group.toaster_edit" />
        )}
      </Toaster>
      <Box
        d="flex"
        flexDirection="row"
        justifyContent="space-between"
        m="2% 0 2% 0"
      >
        <Box w="376px" ml="1.5%">
          <Input
            inputProps={{
              placeholder: intl.formatMessage({ id: 'global.select_group' }),
              backgroundColor: 'white',
            }}
            rightElement={<TriangleDownIcon boxSize={3} color="gray.600" />}
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
          <Button colorScheme="blue" h="45px" w="176px">
            <Link to="/general-config-register">
              <FormattedMessage id="global.register_new" />
            </Link>
          </Button>
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
                <>
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
                            <MenuItem fontSize="sm">
                              <Link to="/general-config-edit">
                                <Edit boxSize={6} mr="5px" />
                                <FormattedMessage id="global.edit_config" />
                              </Link>
                            </MenuItem>
                            <MenuItem
                              color="red.500"
                              fontSize="sm"
                              onClick={() => setOpen(true)}
                            >
                              <DeleteIcon boxSize={6} mr="5px" />
                              <FormattedMessage id="global.remove_config" />
                            </MenuItem>
                          </MenuList>
                        </Menu>
                      </>
                    </Td>
                  </Tr>
                  {row.isExpanded ? (
                    <tr>
                      <td colSpan={10}>{renderRowSubComponent(row)}</td>
                    </tr>
                  ) : null}
                </>
              );
            })}
          </Tbody>
        </Table>
      </Box>
      <Pagination pagination={pagination} />

      <Modal isOpen={open} onClose={() => setOpen(false)} isCentered>
        <ModalOverlay />
        <ModalContent w="424px" h="363px">
          <ModalHeader d="flex" flexDirection="column" alignItems="center">
            <AlertModal boxSize={24} mt="20px" />
          </ModalHeader>
          <ModalBody d="flex" flexDirection="column" alignItems="center">
            <Text
              fontWeight="bold"
              fontSize="md"
              color="black.500"
              textAlign="center"
            >
              <FormattedMessage id="consumption_profile.group.modal_title" />
            </Text>
            <Text fontWeight="normal" fontSize="sm" mt="10px" color="black.500">
              <FormattedMessage id="consumption_profile.group.subtitle" />
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

export default GroupsCard;
