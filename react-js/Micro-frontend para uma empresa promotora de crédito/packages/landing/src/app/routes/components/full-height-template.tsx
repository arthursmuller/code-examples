import { FC } from 'react';

import { useMount } from 'react-use';
import { use100vh } from 'react-div-100vh';
import { Flex, useTheme } from '@chakra-ui/react';

import { usePageContext } from 'features/components/page/page.context';

export const FullHeightTemplate: FC = ({ children }) => {
  const { setMenuBarConfig } = usePageContext();
  const { sizes } = useTheme();
  const height = use100vh();

  useMount(
    () =>
      setMenuBarConfig &&
      setMenuBarConfig({
        menuBarColor: 'primary.gradient',
        fixedMenuScrollOffset: 80,
      }),
  );

  return (
    <Flex
      minH={height ? `${height - parseInt(sizes.menu.height, 10)}px` : '100%'}
      height="auto"
      flex={1}
      direction="column"
    >
      {children}
    </Flex>
  );
};
