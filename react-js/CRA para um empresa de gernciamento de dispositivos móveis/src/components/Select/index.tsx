import { TriangleDownIcon } from '@chakra-ui/icons';
import { Select as SelectChakra } from '@chakra-ui/react';
import PropTypes from 'prop-types';

const Select = ({ children, ...rest }) => (
  <SelectChakra
    icon={<TriangleDownIcon />}
    iconSize="10"
    iconColor="gray.600"
    placeholder="Selecione..."
    borderColor="gray.600"
    name="companyState"
    height="45px"
    color="black.500"
    _disabled={{ bg: 'gray.400', color: '#6e6e78' }}
    {...rest}
  >
    {children}
  </SelectChakra>
);

Select.propTypes = {
  children: PropTypes.any,
};

export default Select;
