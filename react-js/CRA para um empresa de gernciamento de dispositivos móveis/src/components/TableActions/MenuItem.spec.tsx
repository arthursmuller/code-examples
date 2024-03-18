import { Menu } from '@chakra-ui/react';

import { render, screen, fireEvent } from '../../helper/test/test-utils';
import MenuItem from "./MenuItem";

const MenuMenuItem = (props) => (
  <Menu><MenuItem {...props} /></Menu>
)

describe('Component MenuItem from Table Action', () => {
  it('should match with snapshot', () => {
    const { container } = render(<MenuMenuItem text="text" onClick={() => false} />);
    expect(container).toMatchSnapshot();
  });

  it('should render text prop', () => {
    render(<MenuMenuItem text="test" onClick={() => false} />);

    expect(screen.getByText('test')).toBeInTheDocument();
  });

  it('should run onClick function when click on component', () => {
    const onClick = jest.fn();
    render(<MenuMenuItem text="test" onClick={onClick} />);

    fireEvent.click(screen.getByText('test'));

    expect(onClick).toHaveBeenCalledTimes(1);
  });
});
