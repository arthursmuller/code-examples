import { FC } from 'react';

import { Flex, useBreakpointValue } from '@chakra-ui/react';

import { fadeIn } from '../../animations';

const margin = '12px';

export const ActionDialogContainer: FC = ({ children }) => {
  const isDesktop = useBreakpointValue({ base: false, md: true }, 'base');

  return (
    <Flex
      backgroundColor={isDesktop ? 'grey.100' : 'secondary.regular'}
      flex={1}
      maxWidth="600px"
      margin={margin}
      borderRadius="12px"
      direction="column"
      animation={`250ms ${fadeIn} ease-in-out`}
      overflow="hidden"
      maxHeight={`calc(100% - 2 * ${margin})`}
      sx={{
        '.step-container__header': {
          mb: 0,
          mt: 0,
          pt: 0,
        },
      }}
    >
      {children}
    </Flex>
  );
};
