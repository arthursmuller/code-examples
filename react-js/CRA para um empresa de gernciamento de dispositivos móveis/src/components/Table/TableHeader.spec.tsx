import { Table } from '@chakra-ui/react';
import React from 'react';

import { render, screen, fireEvent } from '../../helper/test/test-utils';
import TableHeader from './TableHeader';

const TableTableHeader = (props) => <Table><TableHeader {...props} /></Table>
const headerOneColumn = [
  {
    header: 'header',
    accessor: 'accessor',
  },
];
const headerManyColumn = [
  {
    header: 'header1',
    accessor: 'accessor1',
  },
  {
    header: 'header2',
    accessor: 'accessor2',
    canSort: true,
  },
  {
    header: 'header3',
    accessor: 'accessor3',
    canSort: false,
  },
  {
    header: 'header4',
    accessor: 'accessor4',
    canSort: true,
  },
];

describe('Component TableHeader', () => {
  it('should match to snapshot', () => {
    render(<TableTableHeader headers={headerManyColumn} />);
    expect(screen).toMatchSnapshot();

    render(<TableTableHeader headers={headerManyColumn} onSort={() => false} />);
    expect(screen).toMatchSnapshot();
  });

  it('should run onSort parameter when click on column with canSort=true', () => {
    const onSort = jest.fn(header => header);
    render(<TableTableHeader headers={headerManyColumn} onSort={onSort} />);

    fireEvent.click(screen.getByText('header4'));
    fireEvent.click(screen.getByText('header2'));
    fireEvent.click(screen.getByText('header2'));
    fireEvent.click(screen.getByText('header4'));

    expect(onSort).toHaveBeenCalledTimes(4);
  });

  it('not should run onSort parameter when click on column with canSort!=true', () => {
    const onSort = jest.fn(header => header);
    render(<TableTableHeader headers={headerManyColumn} onSort={onSort} />);

    fireEvent.click(screen.getByText('header1'));
    fireEvent.click(screen.getByText('header3'));

    expect(onSort).toHaveBeenCalledTimes(0);
  });

  it('should show amount columns equal headerManyColumn.lenght', async () => {
    render(<TableTableHeader headers={headerManyColumn} onSort={() => false} />);
    expect(await screen.queryAllByRole('columnheader').length).toEqual(headerManyColumn.length);
  });

  it('should show amount columns equal headerOneColumn.lenght', async () => {
    render(<TableTableHeader headers={headerOneColumn} onSort={() => false} />);
    expect(await screen.queryAllByRole('columnheader').length).toEqual(headerOneColumn.length);
  });

});
