import { FC } from 'react';

import {
  IconButton,
  Popover,
  PopoverArrow,
  PopoverContent,
  PopoverTrigger,
  SystemStyleObject,
  useBreakpointValue,
  PopoverCloseButton,
} from '@chakra-ui/react';
import { useScroll } from 'react-use';

import { useModal, zIndexes } from '@pcf/design-system';
import { usePageContext } from 'features/components/page/page.context';
import { LogoWhatsappIcon } from '@pcf/design-system-icons';

import { WhatsAppModal } from './whats-app-dialog';

const getPositionCoordinates = (
  isMobile: boolean,
  y: number,
): SystemStyleObject => {
  if (isMobile) {
    if (y < 500) {
      return {
        bottom: '16px',
        right: '16px',
      };
    }
    return {
      top: '90px',
      right: '16px',
    };
  }
  return {
    top: '80vh',
    right: '120px',
  };
};

export const WhatsAppBadge: FC = () => {
  const { showModal } = useModal();
  const { pageRef } = usePageContext();
  const scrollPos = useScroll(pageRef);

  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  function handleClick(): void {
    showModal({ modal: <WhatsAppModal />, closeOnClickOverlay: false });
  }

  return isMobile ? (
    <IconButton
      aria-label="Falar pelo whatsapp"
      onClick={handleClick}
      variant="ghost"
      colorScheme="white"
      icon={<LogoWhatsappIcon />}
      fontSize="64px"
      boxSize="64px"
      sx={{
        position: 'fixed',
        transition: 'all .4s linear',
        ...getPositionCoordinates(isMobile, scrollPos.y),
        zIndex: zIndexes.fixedElements,
      }}
    />
  ) : (
    <Popover>
      <PopoverTrigger>
        <IconButton
          aria-label="Falar pelo whatsapp"
          variant="ghost"
          colorScheme="white"
          icon={<LogoWhatsappIcon />}
          fontSize="64px"
          boxSize="64px"
          sx={{
            position: 'fixed',
            transition: 'all .4s linear',
            ...getPositionCoordinates(isMobile, scrollPos.y),
            zIndex: zIndexes.fixedElements,
          }}
        />
      </PopoverTrigger>
      <PopoverContent border="none" zIndex={zIndexes.absoluteElements}>
        <PopoverArrow />
        <WhatsAppModal close={<PopoverCloseButton />} />
      </PopoverContent>
    </Popover>
  );
};
