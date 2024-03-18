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
import { Link } from 'react-router-dom';
import { useTable, useSortBy } from 'react-table';

import Card from '../../../../components/Card';
import Heading from '../../../../components/Heading';
import AlertModal from '../../../../components/Icons/AlertModal';
import Copy from '../../../../components/Icons/Copy';
import Delete from '../../../../components/Icons/Delete';
import Download from '../../../../components/Icons/Download';
import Edit from '../../../../components/Icons/Edit';
import Search from '../../../../components/Icons/Search';
import ThreeDotsIcon from '../../../../components/Icons/ThreeDots';
import Input from '../../../../components/Input';
import Pagination from '../../../../components/PagePagination';
import Select from '../../../../components/Select';
import Text from '../../../../components/Text';

const ManageAccessProfiles = () => {
  const [open, setOpen] = useState(false);
  const [trColor, setTrColor] = useState(-1);

  const iconRef = useRef();

  const changeColor = (irow) => {
    // console.log(irow);
    setTrColor(irow);
  };

  const data = React.useMemo(
    () => [
      {
        access_profile_name: 'Financeiro',
        description: 'Lorem ipsum dolor sit amet',
        access_profile_user_qtd: '10',
      },
      {
        access_profile_name: 'RH',
        description: 'Lorem ipsum dolor sit amet',
        access_profile_user_qtd: '23',
      },
    ],
    []
  );

  const columns = React.useMemo(
    () => [
      {
        Header: 'Nome do perfil de acesso',
        accessor: 'access_profile_name',
      },
      {
        Header: 'Descrição',
        accessor: 'description',
      },
      {
        Header: 'Quantidade de usuários no perfil de acesso',
        accessor: 'access_profile_user_qtd',
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
        <Heading>Gerenciar perfis de acesso</Heading>
        <Text width="90%">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean et
          sollicitudin urna. Duis efficitur, erat rhoncus dictum pharetra, lacus
          sem aliquet nulla, id efficitur nibh est vel felis.
        </Text>
      </Box>
      <Card>
        <Text
          margin="0 0 20px 0"
          color="gray.600"
          fontSize="md"
          fontWeight="bold"
        >
          Filtrar
        </Text>
        <Box d="flex" flexDirection="row">
          <FormControl w="376px" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              Perfil de acesso
            </FormLabel>
            <Select placeholder="" backgroundColor="white" />
          </FormControl>
          <FormControl w="376px">
            <FormLabel fontSize="sm" color="gray.500">
              Teléfono
            </FormLabel>
            <Select placeholder="" backgroundColor="white" />
          </FormControl>
        </Box>
      </Card>
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
            Excel
          </Button>
          <Button color="blue.500" variant="link" mr="40px" fontWeight="normal">
            <Copy boxSize={6} />
            Copiar
          </Button>
          <Link to="/register-access-profile">
            <Button
              bg="blue.500"
              color="white"
              h="45px"
              w="176px"
              fontWeight="400"
              _hover={{ opacity: '70%' }}
            >
              Cadastrar novo
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
                            <Link to="/edit-access-profile">
                              <Edit boxSize={6} mr="5px" />
                              Editar perfil de acesso
                            </Link>
                          </MenuItem>
                          <MenuItem
                            color="red.500"
                            fontSize="sm"
                            onClick={setOpen}
                          >
                            <Delete boxSize={6} mr="5px" />
                            Remover perfil de acesso
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
              Você deseja mesmo remover este perfil de acesso?
            </Text>
            <Text fontWeight="normal" fontSize="sm" mt="10px" color="black.500">
              Esta ação não pode ser desfeita.
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
                  Remover
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
                  Cancelar
                </Button>
              </Box>
            </Box>
          </ModalFooter>
        </ModalContent>
      </Modal>
    </>
  );
};

export default ManageAccessProfiles;
