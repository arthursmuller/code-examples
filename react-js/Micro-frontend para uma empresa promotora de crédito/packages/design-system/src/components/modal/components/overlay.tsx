import { FC, HtmlHTMLAttributes } from 'react';

import { Box } from '@chakra-ui/react';

import { zIndexes } from '../../../consts/z-indexes.enum';
import { useKeyboardEvent } from '../../../hooks/use-keyboard-event';

interface OverlayProps extends HtmlHTMLAttributes<HTMLElement> {
  text?: string;
  closeOnClick: boolean;
  hideModal?: () => void;
}

export const Overlay: FC<OverlayProps> = ({
  children,
  closeOnClick,
  hideModal,
}) => {
  useKeyboardEvent('Escape', hideModal);

  let props = {};

  if (closeOnClick) {
    props = {
      onClick: hideModal,
    };
  }

  return (
    <Box
      position="fixed"
      width="100%"
      height="100%"
      top="0"
      left="0"
      right="0"
      bottom="0"
      zIndex={zIndexes.modal}
      backgroundColor="rgba(0, 0, 0, 0.7)"
      {...props}
    >
      {children}
    </Box>
  );
};
