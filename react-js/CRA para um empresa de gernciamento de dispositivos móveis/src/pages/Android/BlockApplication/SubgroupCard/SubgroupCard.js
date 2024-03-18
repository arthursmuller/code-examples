import {
  ArrowDownIcon,
  ArrowUpIcon,
  TriangleDownIcon,
  CloseIcon,
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
import React, { useState, useRef } from 'react';
import { FormattedMessage } from 'react-intl';
import { Link } from 'react-router-dom';
import { useTable, useSortBy } from 'react-table';

import AlertModal from '../../../../components/Icons/AlertModal';
import Checkmark from '../../../../components/Icons/Checkmark';
import Copy from '../../../../components/Icons/Copy';
import DeleteIcon from '../../../../components/Icons/Delete';
import Download from '../../../../components/Icons/Download';
import Edit from '../../../../components/Icons/Edit';
import Search from '../../../../components/Icons/Search';
import ThreeDotsIcon from '../../../../components/Icons/ThreeDots';
import Input from '../../../../components/Input';
import Pagination from '../../../../components/PagePagination';
import Text from '../../../../components/Text';

const SubgroupCard = ({ general }) => {
  const iconRef = useRef();
  const pagination = 1;
  const { showToaster: toaster, list } = general;
  const [trColor, setTrColor] = useState(-1);
  const [open, setOpen] = useState(false);
  const [modalType, setModalType] = useState('');

  const openModal = (modalName) => {
    setModalType(modalName);
    setOpen(true);
  };

  const changeColor = (irow) => {
    setTrColor(irow);
  };

  const IconCell = ({ cell: { value } }) => {
    return <img src={value} />;
  };

  const StateCell = ({ cell: { value } }) => {
    return (
      <chakra.span color={value === 'Liberado' ? 'green.500' : 'red.500'}>
        {value}
      </chakra.span>
    );
  };

  const columns = React.useMemo(
    () => [
      {
        Header: () => null,
        accessor: 'app_icon',
        Cell: IconCell,
      },
      {
        Header: 'Aplicativo',
        accessor: 'app',
      },
      {
        Header: 'Subgrupo',
        accessor: 'subgroup',
      },
      {
        Header: 'Tipo de regra',
        accessor: 'rule',
      },
      {
        Header: 'Estado',
        accessor: 'state',
        Cell: StateCell,
      },
      {
        Header: 'Aplicação da regra',
        accessor: 'rule_state',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data: list }, useSortBy);

  return (
    <>
      <Box
        d={toaster ? 'flex' : 'none'}
        border="solid 2px #00c3af"
        borderRadius="40px"
        h="70px"
        w="90%"
        justifyContent="space-between"
        alignItems="center"
        mb="2.5%"
      >
        <Text color="#00c3af" m="0px 0px 0px 2%">
          <Checkmark boxSize={8} color="#00c3af" mr="10px" />
          <FormattedMessage id="consumption_profile.success" />
        </Text>
        <CloseIcon
          boxSize={4}
          onClick={() => dispatch(showToaster(false))}
          color="#00c3af"
          m="0px 2% 0px 0px"
          cursor="pointer"
        />
      </Box>
      <Box
        d="flex"
        flexDirection="row"
        justifyContent="space-between"
        m="2% 0 2% 0"
      >
        <Box w="376px" ml="1.5%">
          <Input
            inputProps={{
              backgroundColor: 'white',
            }}
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
          <Button
            color="blue.500"
            borderColor="#0190fe"
            variant="outline"
            h="45px"
            w="176px"
            mr="40px"
            onClick={() => openModal('apply')}
          >
            Aplicar regras
          </Button>
          <Button colorScheme="blue" h="45px" w="176px">
            <Link to="/android/block-app/register-subgroup">
              Cadastrar regra
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
                            <Link to="/android/block-app/edit-subgroup">
                              <Edit boxSize={6} mr="5px" />
                              Editar configuração
                            </Link>
                          </MenuItem>
                          <MenuItem
                            color="red.500"
                            fontSize="sm"
                            onClick={() => openModal('remove')}
                          >
                            <DeleteIcon boxSize={6} mr="5px" />
                            Remover configuração
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
        <ModalContent w="424px" h="363px">
          {modalType == 'apply' && (
            <>
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
                  <FormattedMessage id="block_application.modal.apply_config" />
                </Text>
                <Text
                  fontWeight="normal"
                  fontSize="sm"
                  mt="10px"
                  color="black.500"
                >
                  <FormattedMessage id="block_application.modal.remove_config_text" />
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
                      <FormattedMessage id="block_application.apply" />
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
            </>
          )}
          {modalType == 'remove' && (
            <>
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
                  <FormattedMessage id="block_application.modal.remove_config" />
                </Text>
                <Text
                  fontWeight="normal"
                  fontSize="sm"
                  mt="10px"
                  color="black.500"
                >
                  <FormattedMessage id="block_application.modal.remove_config_text" />
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
            </>
          )}
        </ModalContent>
      </Modal>
    </>
  );
};

export default SubgroupCard;
