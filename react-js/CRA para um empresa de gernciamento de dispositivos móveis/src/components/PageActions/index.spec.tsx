import React from 'react';

import { fireEvent, render, screen,  } from '../../helper/test/test-utils';
import PageAction from './index';

describe('Component PageAction', () => {
  
  it('should match to snapshot', () => {
    render(<PageAction />);
    expect(screen).toMatchSnapshot();

    render(
      <PageAction
        initialSearch="search"
        onSearch={() => false}
        onCopy={() => false}
        onExcel={() => false}
        linkNew="LinkNew"
        labelButtonNew="labelButtonNew"
      />
    );
    expect(screen).toMatchSnapshot();
  });

  it('should run onSearch param when write on input ', () => {
    const onSearch = jest.fn();
    render(<PageAction onSearch={onSearch} />);

    fireEvent.change(screen.getByTestId('search-input'), { target: { value: 'change' } });
    fireEvent.change(screen.getByTestId('search-input'), { target: { value: 'change2' } });

    expect(onSearch).toHaveBeenCalledTimes(2);
    expect(screen.getByTestId('search-input').getAttribute('value')).toBe('change2');
  });

  // TODO it('should navegate to page of linkNew param when click on button New Register', () => {
    
});
