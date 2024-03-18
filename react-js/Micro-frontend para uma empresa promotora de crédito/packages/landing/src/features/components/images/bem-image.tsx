import React, { FC, useMemo } from 'react';

import Image, { ImageLoader } from 'next/image';
import { chakra, useBreakpointValue } from '@chakra-ui/react';

const ChakraNextImage = chakra(Image, {}); //eslint-disable-line

const modLoader =
  (srcPath: string, srcExt: string, mobileMod: string): ImageLoader =>
  ({ width, quality }) => {
    const mod = width < 993 ? mobileMod : '';
    const src = `${srcPath}${mod}.${srcExt}`;

    return `/_next/image?url=${src}&w=${width}&q=${quality || 75}`;
  };

interface BemImageProps {
  srcPath?: string;
  src?: any;
  alt: string;
  position?: string | string[];
  srcExt?: string;
  mobileMod?: string;
  priority?: boolean;
  zIndex?: number | string;
  height?: string | string[];
  width?: string | string[];
}

const shimmer = (w: string, h: string): string => `
<svg width="${w}" height="${h}" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
  <defs>
    <linearGradient id="g">
      <stop stop-color="#333" offset="20%" />
      <stop stop-color="#222" offset="50%" />
      <stop stop-color="#333" offset="70%" />
    </linearGradient>
  </defs>
  <rect width="${w}" height="${h}" fill="#333" />
  <rect id="r" width="${w}" height="${h}" fill="url(#g)" />
  <animate xlink:href="#r" attributeName="x" from="-${w}" to="${w}" dur="1s" repeatCount="indefinite"  />
</svg>`;

const toBase64 = (str: string): string =>
  typeof window === 'undefined'
    ? Buffer.from(str).toString('base64')
    : window.btoa(str);

const getShimmerValue = (
  value: string | string[],
  isMobile: boolean,
): string => {
  if (Array.isArray(value)) {
    if (isMobile) {
      return value[0];
    }
    return value[value.length - 1];
  }
  return value;
};

export const BemImage: FC<BemImageProps> = ({
  srcPath,
  srcExt = 'jpg',
  mobileMod = '-mob',
  src,
  alt,
  position,
  priority,
  zIndex = 1,
  height,
  width,
}) => {
  const isSvg = src?.src?.includes('.svg') || false;
  const isMobile = useBreakpointValue({ base: true, md: false });

  const srcProps = useMemo(
    () =>
      src
        ? { src }
        : {
            src: `${srcPath}.${srcExt}`,
            loader: modLoader(srcPath, srcExt, mobileMod),
          },
    [],
  );

  let conditionalProps = {};

  if (!isSvg) {
    // https://github.com/vercel/next.js/discussions/27101
    conditionalProps = {
      placeholder: 'blur',
      layout: 'fill',
      objectFit: 'cover',
    };
  } else {
    const shimmerWidth = getShimmerValue(width, isMobile);
    const shimmerHeight = getShimmerValue(height, isMobile);

    conditionalProps = {
      placeholder: 'blur',
      blurDataURL: `data:image/svg+xml;base64,${toBase64(
        shimmer(shimmerWidth, shimmerHeight),
      )}`,
    };
  }

  return (
    <ChakraNextImage
      {...(srcProps as any)}
      zIndex={zIndex}
      objectPosition={position}
      width={width || '100%'}
      height={height}
      aria-label={alt}
      alt={alt}
      transition="all 250ms"
      _groupHover={{
        transform: 'scale(1.05)',
      }}
      priority={priority}
      {...conditionalProps}
    />
  );
};
