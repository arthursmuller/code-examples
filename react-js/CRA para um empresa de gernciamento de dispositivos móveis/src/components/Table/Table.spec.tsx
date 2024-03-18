import React from 'react';

import { render, screen } from '../../helper/test/test-utils';
import Table from './Table';


const headers = [
  {
    header: 'header1',
    accessor: 'accessor1',
  },
  {
    header: 'header2',
    accessor: 'accessor2',
    canSort: true,
    isSorted: true,
    isSortedDesc: true,
  },
  {
    header: 'header3',
    accessor: 'accessor3',
    canSort: false,
  },
];

const manyRowsManyColumns = [
  {
    cells: [
      {
        field: 'field_row1col1',
        value: 'value_row1col1',
      },
      {
        field: 'field_row1col2',
        value: 'value_row1col2',
      },
      {
        field: 'field_row1col3',
        value: 'value_row1col3',
      },
    ],
  },
  {
    cells: [
      {
        field: 'field_row2col1',
        value: 'value_row2col1',
      },
      {
        field: 'field_row2col2',
        value: 'value_row2col2',
      },
      {
        field: 'field_row2col3',
        value: 'value_row2col3',
      },
    ],
  },
  {
    cells: [
      {
        field: 'field_row3col1',
        value: 'value_row3col1',
      },
      {
        field: 'field_row3col2',
        value: 'value_row3col2',
      },
      {
        field: 'field_row3col3',
        value: 'value_row3col3',
      },
    ],
  },
];

describe('Component Table', () => {
  it('should match to snapshot', () => {
    render(<Table headerColumns={headers} rows={manyRowsManyColumns} />);
    expect(screen).toMatchSnapshot();

    render(<Table headerColumns={headers} rows={manyRowsManyColumns} handleSort={() => false} />);
    expect(screen).toMatchSnapshot();
  });
});
