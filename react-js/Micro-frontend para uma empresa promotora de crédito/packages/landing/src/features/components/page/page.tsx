import { useRef, FC, ReactNode } from 'react';

import { Button, Flex, Icon } from '@chakra-ui/react';
import { useMount, useScroll } from 'react-use';

import {
  CustomHeading,
  bemTheme,
  useToasterContainerHeight,
} from '@pcf/design-system';
import { ArrowLeftIcon } from '@pcf/design-system-icons';

import {
  PageContextData,
  PageContextProvider,
  usePageContext,
} from './page.context';

import { MenuBar, MenuContentProps, MenuItem } from '../menu-bar';

export interface PageProps extends MenuContentProps {
  children: ReactNode;
  menuItems?: MenuItem[];
  showContextContent?: boolean;
}

const PageFC: FC<PageProps> = ({
  menuItems,
  showContextContent = true,
  children,
  ...menuContentProps
}) => {
  const height = useToasterContainerHeight();

  const scrollRef = useRef<HTMLDivElement>(null);
  const { menuBarConfig, setPageRef } = usePageContext() as PageContextData;

  const scrollPos = useScroll(scrollRef);

  useMount(() => {
    setPageRef(scrollRef);
  });

  const shouldShowContextContent =
    menuBarConfig.hasContextMenu &&
    scrollPos.y > menuBarConfig.fixedMenuScrollOffset;

  return (
    <Flex
      flex={1}
      className="page"
      width="100%"
      h="100%"
      flexDirection="column"
      overflowX="auto"
      ref={scrollRef}
      id="page"
    >
      <MenuBar
        {...menuContentProps}
        mt={height}
        items={menuItems}
        background={
          shouldShowContextContent
            ? menuBarConfig.contextMenuBarColor
            : menuBarConfig.menuBarColor
        }
        fixed={shouldShowContextContent}
        menuBarAlternativeContent={
          showContextContent && shouldShowContextContent ? (
            <Flex flex={1}>
              {menuBarConfig.contextMenuCallback && (
                <Button
                  color="grey.100"
                  variant="link"
                  minW="0px"
                  onClick={menuBarConfig.contextMenuCallback}
                  leftIcon={
                    <Icon
                      display="flex"
                      as={ArrowLeftIcon}
                      height="20px"
                      width="20px"
                    />
                  }
                />
              )}
              {menuBarConfig.contextMenuTitle && (
                <CustomHeading
                  as="h4"
                  textStyle="regular16_20"
                  color="grey.100"
                  flex={1}
                  textAlign="center"
                >
                  {menuBarConfig.contextMenuTitle}
                </CustomHeading>
              )}
            </Flex>
          ) : null
        }
      />

      <Flex
        id="page-content"
        direction="column"
        flexGrow={1}
        minH="fit-content"
        marginTop={
          menuBarConfig.menuBarColor === 'transparent'
            ? `-${bemTheme.sizes.menu.height}`
            : 0
        }
      >
        {children}
      </Flex>
    </Flex>
  );
};

export const Page: FC<PageProps> = (props) => {
  return (
    <PageContextProvider>
      <PageFC {...props} />
    </PageContextProvider>
  );
};
