import { FC } from 'react';

import { Flex, keyframes } from '@chakra-ui/react';
import { use100vh } from 'react-div-100vh';

import { bemTheme, zIndexes } from '@pcf/design-system';

const moveFromTop = keyframes`
 0% {
    opacity: 0;
    transform:translateY(90vh);
  }
 100% {
    opacity: 1;
    transform:translateY(0);
 }
`;

interface MenuMobileProps {
  mt?: number;
}

export const MenuMobile: FC<MenuMobileProps> = ({ children, mt = 0 }) => {
  const relativeViewHeight = use100vh();

  return (
    <Flex
      position="fixed"
      zIndex={zIndexes.menu}
      width="calc(100% + 7px)"
      height={`calc(${relativeViewHeight}px - (${bemTheme.sizes.menu.height} - 1px + ${mt}px))`}
      bottom={0}
      bg="grey.100"
      animation={`.5s ${moveFromTop}`}
      id="mobile-menu"
      overflow="scroll"
      overflowX="hidden"
    >
      <Flex
        width="100%"
        flexDirection="column"
        alignItems="center"
        overflowY="auto"
        overflowX="hidden"
        minH="fit-content"
      >
        {children}
      </Flex>
    </Flex>
  );
};
