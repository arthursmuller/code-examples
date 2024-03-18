import { Box, useRadio } from '@chakra-ui/react';
import PropTypes from 'prop-types';
import React from 'react';

// function RadioCard(props, ...rest) {
function RadioCard(props) {
  const { getInputProps, getCheckboxProps } = useRadio(props);

  const input = getInputProps();
  const checkbox = getCheckboxProps();

  return (
    <Box
      as="label"
      _first={{
        borderTopLeftRadius: '5px',
        borderBottomLeftRadius: '5px',
      }}
      _last={{
        borderTopRightRadius: '5px',
        borderBottomRightRadius: '5px',
      }}
      {...checkbox}
      cursor="pointer"
      borderWidth="1px"
      borderRadius="1px"
      bg="gray.400"
      color="black.500"
      maxWidth="260px"
      pl="20px"
      pr="20px"
      h="45px"
      d="flex"
      alignItems="center"
      justifyContent="center"
      _checked={{
        bg: 'gray.500',
        color: 'white',
        borderColor: 'gray.600',
      }}
    >
      <input {...input} />
      {props.children}
    </Box>
  );
}

RadioCard.propTypes = {
  children: PropTypes.any,
};

export default RadioCard;
