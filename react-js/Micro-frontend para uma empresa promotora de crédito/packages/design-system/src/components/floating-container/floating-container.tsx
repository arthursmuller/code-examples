import { FC, useRef, useEffect, useState } from 'react';

import { Flex, useBreakpointValue } from '@chakra-ui/react';

import { fadeIn } from '../../animations';

const floatingProps = {
  boxShadow: '0px -2px 4px rgba(0, 0, 0, 0.25);',
  bottom: 0,
  left: 0,
  right: 0,
  paddingX: 6,
  paddingY: 2,
  background: 'grey.100',
};

export interface FloatingContainerProps {
  mobileOnly?: boolean;
  scrollContainerId?: string;
  offset?: number;
}

export const FloatingContainer: FC<FloatingContainerProps> = ({
  mobileOnly,
  scrollContainerId = 'page-content',
  children,
  offset = 80,
}) => {
  const ref = useRef<any>();
  const [height, setHeight] = useState<number>(0);
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const containerEl = document.getElementById(scrollContainerId);
  const scrollMargin =
    (containerEl?.clientHeight || 0) + offset >
    document.documentElement.clientHeight
      ? 2
      : 0;

  useEffect(() => {
    setHeight(ref.current.clientHeight);
  });

  return (
    <>
      <Flex
        ref={ref}
        direction="column"
        position={!mobileOnly || isMobile ? 'fixed' : 'relative'}
        marginRight={!mobileOnly || isMobile ? scrollMargin : 0}
        animation={`250ms ${fadeIn} ease-in-out`}
        {...(!mobileOnly || isMobile ? floatingProps : {})}
      >
        {children}
      </Flex>
      <Flex height={height} />
    </>
  );
};
