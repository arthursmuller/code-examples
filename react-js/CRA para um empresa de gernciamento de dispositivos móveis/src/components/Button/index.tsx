import { Button as ButtonChakra } from '@chakra-ui/react';
import PropTypes from 'prop-types';

const Button = ({ color, children, ...rest }) => (
  <ButtonChakra colorScheme={color} _hover={{ opacity: '70%' }} {...rest}>
    {children}
  </ButtonChakra>
);

Button.propTypes = {
  color: PropTypes.string,
  children: PropTypes.any,
};

export default Button;
