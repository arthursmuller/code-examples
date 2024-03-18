import { FC, ReactNode } from 'react';

import { Flex, FlexProps } from '@chakra-ui/react';

import {
  PageLayoutContent,
  PageLayoutContentProps,
} from './page-layout-content';
import {
  PageLayoutBackButton,
  PageLayoutBackButtonProps,
} from './page-layout-back-button';
import { PageLayoutHeader, PageLayoutHeaderProps } from './page-layout-header';
import { PageLayoutTitle } from './page-layout-title';

export interface PageLayoutComposition {
  Header: FC<PageLayoutHeaderProps>;
  BackButton: FC<PageLayoutBackButtonProps>;
  Title: FC;
  Content: FC<PageLayoutContentProps>;
}

export interface PageLayoutProps extends FlexProps {
  children: ReactNode;
}

const PageLayout: FC<PageLayoutProps> & PageLayoutComposition = ({
  children,
  ...flexProps
}) => {
  return (
    <Flex
      flexDir="column"
      className="page-layout"
      bg="secondary.regular"
      flex={1}
      pl={[0, 0, '71px']}
      pr={[0, 0, '71px']}
      overflowX="hidden"
      {...flexProps}
    >
      {children}
    </Flex>
  );
};

PageLayout.BackButton = PageLayoutBackButton;
PageLayout.Content = PageLayoutContent;
PageLayout.Header = PageLayoutHeader;
PageLayout.Title = PageLayoutTitle;

export { PageLayout };
