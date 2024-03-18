import { FC } from 'react';

import { Flex } from '@chakra-ui/react';
import { useMount } from 'react-use';

import { usePageContext } from 'features/components/page/page.context';
import { LandingContextProvider } from 'features/landing/landing.context';

export const LandingTemplate: FC = ({ children }) => {
  const { setMenuBarConfig } = usePageContext();

  useMount(
    () =>
      setMenuBarConfig &&
      setMenuBarConfig({
        menuBarColor: 'transparent',
        contextMenuBarColor: 'primary.gradient',
      }),
  );

  return (
    <LandingContextProvider>
      <Flex width="100%" flexDirection="column" overflow="hidden">
        {children}
      </Flex>
    </LandingContextProvider>
  );
};
