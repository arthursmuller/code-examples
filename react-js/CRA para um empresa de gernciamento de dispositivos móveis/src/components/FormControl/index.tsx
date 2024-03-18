import {
  FormControl as FormControlChakra,
  FormControlProps as FormControlPropsChakra,
  FormLabel,
} from '@chakra-ui/react';
import React from 'react';

import Text from '../Text';

interface FormControlProps extends FormControlPropsChakra {
  children: React.ReactNode;
  textLabel?: string | JSX.Element;
  error?: Error | null;
}

const FormControl: (props: FormControlProps) => JSX.Element = ({
  children,
  textLabel,
  error,
  ...rest
}) => {
  return (
    <FormControlChakra w="100%" mr={{ xl: '24px', lg: '0px' }} {...rest}>
      {textLabel && (
        <FormLabel fontSize="sm" color={error ? 'red.500' : 'gray.500'}>
          {textLabel}
        </FormLabel>
      )}
      {children}
      {error && (
        <Text fontSize="sm" color="red.500">
          {error.message}
        </Text>
      )}
    </FormControlChakra>
  );
};

export default FormControl;
