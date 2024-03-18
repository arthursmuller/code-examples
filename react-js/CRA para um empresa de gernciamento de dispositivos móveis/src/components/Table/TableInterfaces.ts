import {
  TableCellProps,
  TableColumnHeaderProps,
  TableRowProps,
} from '@chakra-ui/react';

import { ListMetadata } from '../../types/generic_list';

export interface Header extends TableColumnHeaderProps {
  header: string;
  accessor: string;
  canSort?: boolean;
  isSorted?: boolean;
  isSortedDesc?: boolean;
}

export interface HeaderProps {
  headers: Header[];
  onSort?: (header: Header) => void;
}

export interface BodyCell {
  field: string;
  value: unknown;
  transform?: (transformProps: {
    item?: BodyCell,
    isExpanded?: boolean,
    toggleExpanded?: () => Set<number>,
  }) => unknown;
  chackraProps?: TableCellProps;
}

export interface Body {
  cells: BodyCell[];
  rows?: TableRowProps;
}

export interface BodyProps {
  rows: Body[];
  expandedRows?: Set<number>;
}

export interface TableProps {
  headerColumns: Header[];
  rows: Body[];
  handleSort?: (params: ListMetadata) => void;
}
