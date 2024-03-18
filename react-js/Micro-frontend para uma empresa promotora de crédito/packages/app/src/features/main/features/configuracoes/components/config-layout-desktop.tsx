import { FC } from 'react';

import { Flex } from '@chakra-ui/react';

import { rightToLeft, Drawer } from '@pcf/design-system';
import { ArrowLeftIcon } from '@pcf/design-system-icons';
import { useNavigatePathUp } from 'hooks';

export interface ConfigLayoutDesktopProps {
  title: string;
}

export const ConfigLayoutDesktop: FC<ConfigLayoutDesktopProps> = ({
  title,
  children,
}) => {
  const navigateUp = useNavigatePathUp();

  return (
    <Flex
      direction="column"
      height="100%"
      animation={`250ms ${rightToLeft} ease-in-out`}
    >
      <Drawer.Title onClick={navigateUp} icon={ArrowLeftIcon} title={title} />

      <Drawer.Body>{children}</Drawer.Body>
    </Flex>
  );
};

export { ConfigLayoutDesktop as default };
