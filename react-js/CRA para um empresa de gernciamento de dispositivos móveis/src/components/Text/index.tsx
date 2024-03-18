import { Text as TextChakra } from '@chakra-ui/react';
import PropTypes from 'prop-types';

const Text = ({ children, ...rest }) => (
  <TextChakra color="gray.500" fontWeight="300" m="2% 0% 3% 0%" {...rest}>
    {children}
  </TextChakra>
);

Text.propTypes = {
  children: PropTypes.any,
};

export default Text;
