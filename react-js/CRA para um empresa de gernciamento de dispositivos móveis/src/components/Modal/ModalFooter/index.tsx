import {
  Box,
  Button,
  Divider,
  ModalFooter as ModalFooterChakra,
} from '@chakra-ui/react';
import React from 'react';
import { FormattedMessage } from 'react-intl';

interface ModalFooterProps {
  children?: React.ReactNode;
  onCancel?: () => void;
  labelCancel?: React.ReactNode;
  onConfirm?: () => void;
  labelConfirm?: React.ReactNode;
}

const ModalFooter = ({ children, onConfirm, labelConfirm, onCancel, labelCancel }: ModalFooterProps) => {
  return (
    <ModalFooterChakra d="flex" flexDirection="column" alignSelf="center" w="100%">
      {children || (
        <>
          <Box mb="19px">
            <Divider borderColor="gray.600" orientation="horizontal" />
          </Box>
          <Box d="flex" flexDirection="row">
            {onConfirm && (
              <Box mr="14px">
                <Button
                  w="180px"
                  h="45px"
                  fontWeight="normal"
                  colorScheme="blue"
                  onClick={onConfirm}
                >
                  {labelConfirm || <FormattedMessage id="global.remove" />}
                </Button>
              </Box>
            )}
            {onCancel && (
              <Box>
                <Button
                  w="180px"
                  h="45px"
                  fontWeight="normal"
                  variant="outline"
                  colorScheme="blue"
                  onClick={onCancel}
                >
                  {labelCancel || <FormattedMessage id="global.cancel" />}
                </Button>
              </Box>
            )}
          </Box>
        </>
      )}
    </ModalFooterChakra>
  );
};

export default ModalFooter;
