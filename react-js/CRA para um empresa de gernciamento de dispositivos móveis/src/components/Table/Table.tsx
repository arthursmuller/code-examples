import { Table as TableChakra } from '@chakra-ui/react';
import React from 'react';

import TableBodyComponent from './TableBody';
import TableHeaderComponent from './TableHeader';
import { Header, TableProps } from './TableInterfaces';

const Table: React.FC<TableProps> = (
  { headerColumns, rows, handleSort }: TableProps) => {
    const onSort = (header: Header) => {
      handleSort && handleSort({
        sortingDirection: !header.isSorted ? 'ASC' : (header.isSortedDesc ? 'ASC' : 'DESC' ),
        sortingProperty: header.accessor,
        page: 1,
      });
    };

  return (
    <TableChakra>
      <TableHeaderComponent headers={headerColumns} onSort={onSort} />
      <TableBodyComponent rows={rows} />
    </TableChakra>
  )
}

export default Table;