import { ArrowDownIcon, ArrowUpIcon } from '@chakra-ui/icons';
import {
  Box,
  Button,
  Divider,
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
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
} from '@chakra-ui/react';
import React, { useState, useRef } from 'react';
import { Link } from 'react-router-dom';
import { useTable, useSortBy } from 'react-table';

import AlertModal from '../../../../components/Icons/AlertModal';
import Copy from '../../../../components/Icons/Copy';
import Delete from '../../../../components/Icons/Delete';
import Download from '../../../../components/Icons/Download';
import Edit from '../../../../components/Icons/Edit';
import Search from '../../../../components/Icons/Search';
import ThreeDotsIcon from '../../../../components/Icons/ThreeDots';
import Input from '../../../../components/Input';
import Pagination from '../../../../components/PagePagination';
import Text from '../../../../components/Text';

const GroupCard = () => {
  const [open, setOpen] = useState(false);
  const [trColor, setTrColor] = useState(-1);
  const iconRef = useRef();

  const changeColor = (irow) => {
    setTrColor(irow);
  };

  const pagination = 1;

  const data = React.useMemo(
    () => [
      {
        url_keyword: 'youtube.com',
        group: 'QA',
        rule_type: 'Padrão',
        state: 'Liberado',
        rule_application: 'Não aplicada',
      },
    ],
    []
  );
  const columns = React.useMemo(
    () => [
      {
        Header: 'URL/Palabra chave',
        accessor: 'url_keyword',
      },
      {
        Header: 'Grupo',
        accessor: 'group',
      },
      {
        Header: 'Tipo de regra',
        accessor: 'rule_type',
      },
      {
        Header: 'Estado',
        accessor: 'state',
      },
      {
        Header: 'Aplicação da regra',
        accessor: 'rule_application',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data }, useSortBy);
  return (
    <>
      <Box
        w="100%"
        d="flex"
        flexDirection="row"
        justifyContent="space-between"
        mt="2%"
      >
        <Box w="376px">
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
          <Button
            variant="outline"
            colorScheme="blue"
            color="blue.500"
            h="45px"
            w="176px"
            mr="40px"
            onClick={setOpen}
          >
            Aplicar regras
          </Button>
          <Link to="/block-site-url-group-register">
            <Button bg="blue.500" color="white" h="45px" w="176px">
              Cadastrar regra
            </Button>
          </Link>
        </Box>
      </Box>
      <Box mt="3%">
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
                            <Link to="/block-site-url-group-edit">
                              <Edit boxSize={6} mr="5px" />
                              Editar regra
                            </Link>
                          </MenuItem>
                          <MenuItem color="red.500" fontSize="sm">
                            <Delete boxSize={6} mr="5px" />
                            Remover regra
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
              Você deseja mesmo aplicar todas regra de aplicationes?
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
                  Aplicar
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

export default GroupCard;
