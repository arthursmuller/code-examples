import { FC } from 'react';

import { BoxProps, Center, Spinner } from '@chakra-ui/react';
import { use100vh } from 'react-div-100vh';

export interface LoaderProps extends BoxProps {
  fullHeight?: boolean;
  fullWidth?: boolean;
  theme?: 'primary' | 'white';
  size?: 'sm' | 'md' | 'lg';
}

export const Loader: FC<LoaderProps> = ({
  fullHeight,
  fullWidth,
  theme = 'primary',
  size = 'md',
  ...boxProps
}) => {
  const relativeViewHeight = use100vh();

  return (
    (!fullHeight || !!relativeViewHeight) && (
      <Center
        data-testid="loader"
        height={fullHeight ? `${relativeViewHeight}px` : '100%'}
        width={fullWidth ? '100%' : 'auto'}
        {...boxProps}
      >
        <Spinner
          size={size}
          color={theme !== 'white' ? 'primary.regular' : 'white'}
          speed="0.65s"
          thickness="4px"
        />
      </Center>
    )
  );
};
