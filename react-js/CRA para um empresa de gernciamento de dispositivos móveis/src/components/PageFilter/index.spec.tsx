import React from 'react';

import { render, screen,  } from '../../helper/test/test-utils';
import PageFilter from './index';

describe('Component PageFilter', () => {
  
  it('should match to snapshot', () => {
    render(<PageFilter>children</PageFilter>);
    expect(screen).toMatchSnapshot();
  });

});
