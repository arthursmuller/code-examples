import { BoxProps, Box, Divider, Button } from '@chakra-ui/react';
import React from 'react';

import Card from '../Card';

interface FormContainerProps extends BoxProps {
  children: React.ReactNode;

  labelPrimary?: string | React.ReactNode;
  handlePrimary?: () => void;
  disabledPrimary?: boolean;

  // TODO errro na escrita, é labelSecondary
  labelSecundary?: string | React.ReactNode;
  // TODO errro na escrita, é handleSecondary
  handleSecundary?: () => void;

  labelFilter?: string | React.ReactNode;
  handleFilter?: () => void;
  disabledFilter?: boolean;
}

const FormContainer: React.FC<FormContainerProps> = ({
  labelPrimary,
  handlePrimary,
  disabledPrimary,
  labelSecundary,
  handleSecundary,
  labelFilter,
  handleFilter,
  disabledFilter,
  children,
  ...rest
}: FormContainerProps) => {
  return (
    <Card {...rest}>
      <Box d="flex" flexDirection="column">
        {children}
      </Box>
      {(handlePrimary || handleSecundary || handleFilter) && (
        <Box>
          <Divider orientation="horizontal" mb="1.5%" mt="1.5%" />
        </Box>
      )}
      <Box d="flex" flexDirection="row" alignItems="center">
        {handlePrimary && (
          <Button
            bg="blue.500"
            color="white"
            h="45px"
            w="176px"
            _disabled={{ bg: '#f0f0f0', color: 'gray.600' }}
            disabled={disabledPrimary}
            onClick={handlePrimary}
          >
            {labelPrimary}
          </Button>
        )}
        {handlePrimary && handleSecundary && (
          <Divider
            orientation="vertical"
            h="22px"
            ml="1%"
            borderColor="gray.600"
          />
        )}
        {handleSecundary && (
          <Button variant="ghost" color="blue.500" onClick={handleSecundary}>
            {labelSecundary}
          </Button>
        )}
        {handleFilter && (
          <Button
            variant="outline"
            colorScheme="blue"
            borderColor="blue.500"
            color="blue.500"
            h="45px"
            w="176px"
            disabled={disabledFilter}
            onClick={handleFilter}
          >
            {labelFilter}
          </Button>
        )}
      </Box>
    </Card>
  );
};

export default FormContainer;
