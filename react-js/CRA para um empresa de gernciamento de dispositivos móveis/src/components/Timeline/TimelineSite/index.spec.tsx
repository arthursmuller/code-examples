import { render, screen, fireEvent } from '@testing-library/react';

import TimelineSite from './';

describe('Component TimelineSite', () => {
  test('should match to snapshot', () => {
    render(<TimelineSite label="label" />);

    expect(screen).toMatchSnapshot();
  });

  test('should location been equal to label parameter', () => {
    render(<TimelineSite label="http://site.com" />);

    expect(screen.getByText('http://site.com')).toHaveAttribute(
      'href',
      'http://site.com'
    );
  });
});
