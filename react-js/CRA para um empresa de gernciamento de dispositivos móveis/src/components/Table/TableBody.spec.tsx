import { Table } from '@chakra-ui/react';
import React from 'react';

import { render, screen, fireEvent } from '../../helper/test/test-utils';
import TableBody from './TableBody';
import { Body } from './TableInterfaces';

const TableTableBody = (props) => (
  <Table>
    <TableBody {...props} />
  </Table>
);

const manyRowsManyColumns: Body[] = [
  {
    cells: [
      {
        field: 'field_row1col1',
        value: 'value_row1col1',
      },
      {
        field: 'expander',
        value: 'expander1',
      },
      {
        field: 'field_row1col2',
        value: 'value_row1col2',
      },
      {
        field: 'field_row1col3',
        value: '',
        transform: ({ toggleExpanded }) => <div onClick={toggleExpanded}>ToggleExpander1</div>,
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
        field: 'expander',
        value: 'expander2',
      },
      {
        field: 'field_row2col2',
        value: 'value_row2col2',
      },
      {
        field: 'field_row2col3',
        value: '',
        transform: ({ toggleExpanded }) => <div onClick={toggleExpanded}>ToggleExpander2</div>,
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
        field: 'expander',
        value: 'expander3',
      },
      {
        field: 'field_row3col2',
        value: 'value_row3col2',
      },
      {
        field: 'field_row3col3',
        value: '',
        transform: ({ toggleExpanded }) => <div onClick={toggleExpanded}>ToggleExpander3</div>,
      },
    ],
  },
];

describe('Component TableBody', () => {
  it('should match to snapshot', () => {
    render(<TableTableBody rows={manyRowsManyColumns} />);
    expect(screen).toMatchSnapshot();
  });
  
  it('should render one row and many columns', () => {
    const oneRowManyColumns = [manyRowsManyColumns[0]];
    render(<TableTableBody rows={oneRowManyColumns} />);
    expect(screen.getByRole('rowgroup')).toBeInTheDocument();
    expect(screen.getByRole('rowgroup')).toHaveTextContent('value_row1col1');
    expect(screen.getByRole('rowgroup')).toHaveTextContent('value_row1col2');
  });

  it('should render many rows and many columns', () => {
    render(<TableTableBody rows={manyRowsManyColumns} />);
    expect(screen.getByRole('rowgroup')).toBeInTheDocument();
    expect(screen.getByRole('rowgroup')).toHaveTextContent('value_row1col1');
    expect(screen.getByRole('rowgroup')).toHaveTextContent('value_row1col2');
    expect(screen.getByRole('rowgroup')).toHaveTextContent('value_row2col1');
    expect(screen.getByRole('rowgroup')).toHaveTextContent('value_row2col2');
    expect(screen.getByRole('rowgroup')).toHaveTextContent('value_row3col1');
    expect(screen.getByRole('rowgroup')).toHaveTextContent('value_row3col2');
  });
  
  it('not should render column with field=expander in row-normal', () => {
    render(<TableTableBody rows={manyRowsManyColumns} />);

    expect(screen.getAllByTestId('row-normal')[0]).toHaveTextContent('value_row1col1');
    expect(screen.getAllByTestId('row-normal')[0]).toHaveTextContent('value_row1col2');
    expect(screen.getAllByTestId('row-normal')[0]).not.toHaveTextContent('expander1');
    expect(screen.getAllByTestId('row-normal')[2]).toHaveTextContent('value_row3col1');
    expect(screen.getAllByTestId('row-normal')[2]).toHaveTextContent('value_row3col2');
    expect(screen.getAllByTestId('row-normal')[2]).not.toHaveTextContent('expander3');
  });
  
  it('should render hidden column with field=expander in row-expander ', () => {
    render(<TableTableBody rows={manyRowsManyColumns} />);

    expect(screen.getAllByTestId('row-expander')[0]).toHaveTextContent('expander1');
    expect(screen.getAllByTestId('row-expander')[1]).toHaveTextContent('expander2');
    expect(screen.getAllByTestId('row-expander')[0]).toHaveAttribute('aria-hidden', 'true');
    expect(screen.getAllByTestId('row-expander')[1]).toHaveAttribute('aria-hidden', 'true');
  });
  
  it('should toggle show/hidden row with content of field=expander in row-expander when click on div with toggleExpanded function', () => {
    render(<TableTableBody rows={manyRowsManyColumns} />);

    fireEvent.click(screen.getByText('ToggleExpander1'));
    fireEvent.click(screen.getByText('ToggleExpander2'));
    expect(screen.getAllByTestId('row-expander')[0]).toHaveAttribute('aria-hidden', "false");
    expect(screen.getAllByTestId('row-expander')[1]).toHaveAttribute('aria-hidden', "false");

    fireEvent.click(screen.getByText('ToggleExpander2'));
    expect(screen.getAllByTestId('row-expander')[0]).toHaveAttribute('aria-hidden', "false");
    expect(screen.getAllByTestId('row-expander')[1]).toHaveAttribute('aria-hidden', "true");
  });
});
