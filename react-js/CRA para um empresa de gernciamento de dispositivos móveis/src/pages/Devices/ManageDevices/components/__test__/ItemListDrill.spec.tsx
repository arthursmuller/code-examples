import React from 'react';

import { render, screen  } from '../../../../../helper/test/test-utils';
import ItemListDrill from '../ItemListDrill';

describe('Component ItemListDrill', () => {
  
  it('should match to snapshot', () => {
    render(<ItemListDrill label="label" value="value" color="#fff" />);
    expect(screen).toMatchSnapshot();
  });
  
  it('should match to snapshot 2', () => {
    render(<ItemListDrill label="label" value="value" />);
    expect(screen).toMatchSnapshot();
  });

});
