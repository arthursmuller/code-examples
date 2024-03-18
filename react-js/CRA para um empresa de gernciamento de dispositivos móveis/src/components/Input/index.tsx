import {
  Input as InputChakra,
  InputGroup,
  InputLeftElement,
  InputRightElement,
} from '@chakra-ui/react';
import PropTypes from 'prop-types';

export type InputProps = {
  inputProps: unknown;
  leftElement?: unknown;
  rightElement?: unknown;
  validation?: unknown;
};

export const inputChakraStyles = {
  borderColor: 'gray.600',
  _placeholder: { color: 'gray.500' },
  height: '45px',
  borderRadius: '5px',
  color: 'black.500',
  fontSize: '14px',
  _disabled: { bg: 'gray.400' },
};

const Input = ({
  inputProps,
  leftElement,
  rightElement,
  validation,
}: InputProps) => (
  <InputGroup>
    {leftElement && <InputLeftElement>{leftElement}</InputLeftElement>}
    <InputChakra {...inputChakraStyles} {...inputProps} {...validation}/>
    {rightElement && <InputRightElement>{rightElement}</InputRightElement>}
  </InputGroup>
);

Input.propTypes = {
  inputProps: PropTypes.object,
  leftElement: PropTypes.any,
  rightElement: PropTypes.any,
  validation: PropTypes.any,
};

export default Input;
