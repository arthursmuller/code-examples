import { ArrowDownIcon, ArrowUpIcon } from '@chakra-ui/icons';
import { Thead, Tr, Th } from '@chakra-ui/react';
import React from 'react';

import { HeaderProps } from './TableInterfaces';

const TableHeaderComponent: React.FC<HeaderProps> = ({
  headers,
  onSort,
}: HeaderProps) => {
  return (
    headers && (
      <Thead>
        <Tr key={Math.random()}>
          {headers.map((headerColumn, index) => {
            const {
              // eslint-disable-next-line @typescript-eslint/no-unused-vars
              accessor,
              header,
              canSort,
              isSorted,
              isSortedDesc,
              ...chakraProps
            } = headerColumn;
            
            return (
              <Th
                color="black.500"
                fontSize="14px"
                fontWeight="normal"
                {...chakraProps}
                key={index}
                onClick={() => canSort && onSort && onSort(headerColumn)}
                cursor={headerColumn.canSort && onSort ? 'pointer' : 'default'}
                role="columnheader"
              >
                {header}
                <span className="pl-4">
                  {isSorted ? (
                    isSortedDesc ? (
                      <ArrowDownIcon color="gray.500" />
                    ) : (
                      <ArrowUpIcon color="gray.500" />
                    )
                  ) : null}
                </span>
              </Th>
            );
          })}
        </Tr>
      </Thead>
    )
  );
};

export default TableHeaderComponent;
