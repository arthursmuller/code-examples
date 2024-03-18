import { useEffect, useRef, useState, FC, Children } from 'react';

import { Box, Flex } from '@chakra-ui/react';
import { useScroll } from 'react-use';

import { NavigationAction } from './navigation-action';
import { NavigationBullet } from './navigation-bullet';

import { ColorSchemes } from '../../bem-chakra-theme/foundations/colors';

export interface SwipeableContainerProps {
  schemeColor?:
    | ColorSchemes.grey
    | ColorSchemes.secondary
    | `${ColorSchemes.grey}`
    | `${ColorSchemes.secondary}`;
  arrowPos?: 'bottom' | 'middle' | 'none';
  fixedPerPage?: number;
}

export const SwipeableContainer: FC<SwipeableContainerProps> = ({
  schemeColor = ColorSchemes.grey,
  arrowPos = 'middle',
  fixedPerPage,
  children,
}) => {
  const [mounted, setMounted] = useState<boolean>(false);
  const scrollRef = useRef<HTMLDivElement>(null);
  const containerRef = useRef<HTMLDivElement>(null);
  const [{ containerWidth, perPage, pages }, setViewConfig] = useState({
    containerWidth: 0,
    perPage: 1,
    pages: 1,
  });

  const { x } = useScroll(scrollRef);
  const curBullet = Math.round(x / Math.floor(containerWidth / perPage));

  useEffect((): (() => void) | undefined => {
    setMounted(true);

    if (containerRef.current) {
      const updateWidth = (): void => {
        const nextContainerWidth = containerRef?.current?.offsetWidth || 0;
        const firstChildWidth =
          (scrollRef?.current?.children[0]?.children[0] as HTMLElement)
            ?.offsetWidth || 1;
        let nextPerPage =
          fixedPerPage || Math.floor(nextContainerWidth / firstChildWidth) || 1;

        if (nextPerPage > (scrollRef?.current?.children?.length || 0))
          nextPerPage = scrollRef?.current?.children?.length || 1;

        setViewConfig({
          containerWidth: nextContainerWidth,
          perPage: nextPerPage,
          pages: Math.ceil(Children.count(children) / nextPerPage),
        });
      };

      window.addEventListener('resize', updateWidth);

      updateWidth();

      return () => window.removeEventListener('resize', updateWidth);
    }

    return undefined;
  }, [containerRef, children, fixedPerPage]);

  function navigate(direction: 'forward' | 'back'): void {
    scrollRef?.current?.scrollBy(
      (containerWidth / perPage) * (direction === 'forward' ? 1 : -1),
      0,
    );
  }

  function navigateToChild(index: number): void {
    scrollRef?.current?.scrollBy(
      (containerWidth / perPage) * (index - curBullet),
      0,
    );
  }

  return (
    <Box
      id="Carousel"
      ref={containerRef}
      width="100%"
      overflow="hidden"
      paddingBottom="16px"
      position="relative"
    >
      <Flex
        id="Container"
        overflowX="auto"
        ref={scrollRef}
        paddingBottom={2}
        style={{
          scrollSnapType: 'x mandatory',
          scrollBehavior: 'smooth',
          WebkitOverflowScrolling: 'touch',
          scrollbarWidth: 'none',
          scrollbarColor: 'transparent',
        }}
        sx={{
          '::-webkit-scrollbar': { width: '0' },
          '::-webkit-scrollbar-track': { background: 'transparent' },
          '::-webkit-scrollbar-thumb': {
            background: 'transparent',
            border: 'none',
          },
        }}
      >
        {Children.map(children, (child, index) => (
          <Flex
            key={`child-${index}`}
            minWidth={mounted ? containerWidth / perPage : `100%`}
            opacity={mounted ? 1 : 0}
            transition="opacity 250ms"
            {...(mounted && fixedPerPage
              ? { maxWidth: containerWidth / perPage }
              : {})}
            justifyContent="center"
            alignItems="center"
            position="relative"
            flexShrink={0}
            style={{ scrollSnapAlign: 'start' }}
          >
            {child}
          </Flex>
        ))}
      </Flex>

      {pages > 1 && (
        <Box
          id="Bottom-navigation"
          position="absolute"
          bottom="0"
          left="0"
          right="0"
          textAlign="center"
        >
          {arrowPos === 'bottom' && (
            <NavigationAction
              direction="left"
              schemeColor={schemeColor}
              mr="32px"
              onClick={() => navigate('back')}
              disabled={curBullet === 0}
            />
          )}

          {new Array(pages).fill(undefined).map((_, index) => (
            <NavigationBullet
              key={`bullet-${index}`}
              schemeColor={
                schemeColor as ColorSchemes.grey | ColorSchemes.secondary
              }
              active={curBullet === index}
              onClick={() => navigateToChild(index)}
            />
          ))}

          {arrowPos === 'bottom' && (
            <NavigationAction
              direction="right"
              schemeColor={
                schemeColor as ColorSchemes.grey | ColorSchemes.secondary
              }
              ml="32px"
              onClick={() => navigate('forward')}
              disabled={pages - 1 === curBullet}
            />
          )}
        </Box>
      )}

      {arrowPos === 'middle' && pages > 1 && (
        <>
          <NavigationAction
            direction="left"
            schemeColor={schemeColor}
            position="absolute"
            left="1"
            top="45%"
            onClick={() => navigate('back')}
            disabled={curBullet === 0}
          />

          <NavigationAction
            direction="right"
            schemeColor={schemeColor}
            position="absolute"
            right="1"
            top="45%"
            onClick={() => navigate('forward')}
            disabled={pages - 1 === curBullet}
          />
        </>
      )}
    </Box>
  );
};
