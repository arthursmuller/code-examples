import { FC, SVGProps } from 'react';

import { Flex, Tag } from '@chakra-ui/react';

import {
  NotificationAtivoIcon,
  NotificationInativoIcon,
} from '@pcf/design-system-icons';

const iconWithTag =
  (count: number): FC<SVGProps<SVGSVGElement>> =>
  (props) => {
    return (
      <Flex
        position="relative"
        width={10}
        justifyContent="center"
        fill="secondary.mid-dark"
      >
        <Tag
          size="sm"
          variant="solid"
          backgroundColor="secondary.regular"
          position="absolute"
          right={-2}
        >
          {count}
        </Tag>
        <NotificationAtivoIcon {...props} fill="secondary.mid-dark" />
      </Flex>
    );
  };

export const notificationIconFor = (
  notificationCount: number,
): FC<React.SVGProps<SVGSVGElement>> =>
  notificationCount ? iconWithTag(notificationCount) : NotificationInativoIcon;
