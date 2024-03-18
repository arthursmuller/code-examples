import { Tbody, Tr, Td } from '@chakra-ui/react';
import React, { Fragment } from 'react';

import { toggleElement } from '../../helper/set';
import { BodyCell, BodyProps } from './TableInterfaces';

const TableBodyComponent: React.FC<BodyProps> = ({ rows }: BodyProps) => {
  const [rowsExpanded, setRowsExpandedState] = React.useState<Set<number>>(
    new Set()
  );

  const renderValue = (idRow: number, item: BodyCell) => {
    if (typeof item.transform == 'function') {
      const setRowsExpanded = (rowId) => {
        const newSet = toggleElement(rowsExpanded, rowId);
        setRowsExpandedState(newSet);
        return newSet;
      };
      return item.transform({
        item,
        isExpanded: rowsExpanded.has(idRow),
        toggleExpanded: () => setRowsExpanded(idRow),
      });
    }
    return item.value;
  };

  const columnsIsVisible = (item: BodyCell) => {
    return item.field !== 'expander';
  };

  const columnsNotIsVisible = (item: BodyCell) => {
    return item.field === 'expander';
  };

  const expanderDisplay = (idRow: number) => {
    return rowsExpanded.has(idRow) ? 'table-row' : 'none';
  };

  return rows &&
    <Tbody role="rowgroup" bg="white" borderRadius="10px">
      {rows.map((rowCurrent, irow) => {
        const { cells } = rowCurrent;
        const columnsVisible = cells.filter(columnsIsVisible);
        const columnsNotVisible = cells.filter(columnsNotIsVisible);
        return (
          <Fragment key={irow}>
            <Tr
              role="row"
              data-testid="row-normal"
              {...rowCurrent.rows}
            >
              {columnsVisible.map((cell, icell) => (
                <Td
                  role="cell"
                  color="gray.500"
                  fontSize="sm"
                  paddingLeft="1rem"
                  paddingRight="1rem"
                  {...cell.chackraProps}
                  borderTopLeftRadius={irow == 0 && icell == 0 ? '10px' : null}
                  borderBottomLeftRadius={
                    irow == Object.keys(rows).length - 1 && icell == 0
                      ? '10px'
                      : null
                  }
                  borderTopRightRadius={
                    irow == 0 && icell == cells.length - 1 ? '10px' : null
                  }
                  borderBottomRightRadius={
                    irow == Object.keys(rows).length - 1 &&
                    icell == cells.length - 1
                      ? '10px'
                      : null
                  }
                  key={icell}
                >
                  {renderValue(irow, cell)}
                </Td>
              ))}
            </Tr>
            {columnsNotVisible.map((cell) => (
              <Tr
                key={cell.field}
                data-testid="row-expander"
                aria-hidden={expanderDisplay(irow) === 'none'}
                d={expanderDisplay(irow)}
              >
                <Td colSpan={columnsVisible.length}>
                  {renderValue(irow, cell)}
                </Td>
              </Tr>
            ))}
          </Fragment>
        );
      })}
    </Tbody>
};

export default TableBodyComponent;
