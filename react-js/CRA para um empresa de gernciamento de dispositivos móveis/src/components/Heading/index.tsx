import { Heading as HeadingChakra } from '@chakra-ui/react';
import PropTypes from 'prop-types';

const Heading = ({ children, ...rest }) => (
  <HeadingChakra color="black.500" fontSize="5xl" fontWeight="light" {...rest}>
    {children}
  </HeadingChakra>
);

Heading.propTypes = {
  children: PropTypes.any,
};

export default Heading;
