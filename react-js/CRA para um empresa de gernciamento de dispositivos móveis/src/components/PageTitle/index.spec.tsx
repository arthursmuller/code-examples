import { render, screen } from '@testing-library/react';
import React from 'react';

import PageTile from './index';

describe('Component PageTile', () => {
  it('should match to snapshot', () => {
    render(<PageTile />);
    expect(screen).toMatchSnapshot();

    render(<PageTile title="Titulo" />);
    expect(screen).toMatchSnapshot();

    render(<PageTile title="Titulo" description="decrição" />);
    expect(screen).toMatchSnapshot();
  });
  
});
