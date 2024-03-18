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
} from '@chakra-ui/react';
import React, { useState, useRef } from 'react';
import { FormattedMessage } from 'react-intl';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { useTable, useSortBy } from 'react-table';

import Card from '../../../components/Card';
import DatePicker from '../../../components/Datepicker';
import Heading from '../../../components/Heading';
import Copy from '../../../components/Icons/Copy';
import Download from '../../../components/Icons/Download';
import Eye from '../../../components/Icons/Eye';
import Search from '../../../components/Icons/Search';
import ThreeDotsIcon from '../../../components/Icons/ThreeDots';
import Input from '../../../components/Input';
import Pagination from '../../../components/PagePagination';
import Text from '../../../components/Text';
import Toaster from '../../../components/Toaster';
import {
  filterUpdate,
  sendDocument,
  closeToaster,
} from '../../../store/document';
import SendDocument from './sendDocument';

const SharedDocuments = () => {
  const dispatch = useDispatch();
  const {
    filter,
    documents,
    document: { showToaster },
  } = useSelector((state) => state.document);
  const [open, setOpen] = useState(false);
  const [trColor, setTrColor] = useState(-1);

  const iconRef = useRef();

  const changeColor = (irow) => {
    setTrColor(irow);
  };

  const handleFilterChange = (field) => (value) => {
    dispatch(filterUpdate({ ...filter, [field]: value }));
  };

  const handleTextFilterChange =
    (field) =>
    ({ target }) => {
      dispatch(filterUpdate({ ...filter, [field]: target.value }));
    };

  const send = (data) => {
    dispatch(sendDocument(data));
    setOpen(false);
  };

  const columns = React.useMemo(
    () => [
      {
        Header: 'Fecha',
        accessor: 'date',
      },
      {
        Header: 'Compartir nuevo',
        accessor: 'document',
      },
    ],
    []
  );

  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({ columns, data: documents }, useSortBy);

  return (
    <>
      <Box>
        <Heading>
          <FormattedMessage id="document.title" />
        </Heading>
        <Text width="90%">
          <FormattedMessage id="document.title_text" />
        </Text>
      </Box>
      <Card>
        <Text
          margin="0 0 20px 0"
          color="gray.600"
          fontSize="md"
          fontWeight="bold"
        >
          <FormattedMessage id="document.filter" />
        </Text>
        <Box d="flex" flexDirection="row">
          <FormControl w="176px" mr="24px">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="document.start_date" />
            </FormLabel>
            <DatePicker
              selected={filter.start_date}
              onChange={handleFilterChange('start_date')}
            />
          </FormControl>
          <FormControl w="176px">
            <FormLabel fontSize="sm" color="gray.500">
              <FormattedMessage id="document.end_date" />
            </FormLabel>
            <DatePicker
              selected={filter.end_date}
              onChange={handleFilterChange('end_date')}
            />
          </FormControl>
        </Box>
      </Card>
      <Toaster
        open={showToaster}
        onClose={() => dispatch(closeToaster())}
        mt="15px"
      >
        <FormattedMessage id="document.toaster_success" />
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
            inputProps={{
              backgroundColor: 'white',
              value: filter.text_search,
              onChange: handleTextFilterChange('text_search'),
            }}
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
          <Button
            bg="blue.500"
            color="white"
            h="45px"
            w="176px"
            fontWeight="400"
            _hover={{ opacity: '70%' }}
            onClick={setOpen}
          >
            <FormattedMessage id="document.new" />
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
                            <Link to="/shared-details">
                              <Eye boxSize={6} mr="5px" />
                              <FormattedMessage id="document.view" />
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
      <Pagination pagination={1} />

      <SendDocument open={open} onClose={() => setOpen(false)} onSend={send} />
    </>
  );
};

export default SharedDocuments;
