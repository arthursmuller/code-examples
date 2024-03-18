import { SystemStyleObjectRecord } from '@chakra-ui/react';

import { FontWeights } from '../../foundations/typography';

export const Radio = {
  baseStyle: (): SystemStyleObjectRecord => {
    return {
      control: {
        border: '1px solid',
        borderRadius: 'full',
        borderColor: 'grey.600',
        bg: 'grey.200',
        transition: 'all .2s',
        _checked: {
          _before: {
            content: '""',
            w: '12px',
            h: '12px',
            borderRadius: 'full',
            bg: 'primary.regular',
            color: 'primary.regular',
          },
          bg: 'primary.washed',
          borderColor: 'primary.regular',
          _hover: {
            bg: 'primary.light',
          },
        },
      },
      label: {
        color: 'grey.800',
        _checked: {
          color: 'primary.regular',
          fontWeight: FontWeights.bold,
        },
      },
    };
  },
  sizes: {
    md: {
      control: {
        w: '24px',
        h: '24px',
      },
    },
  },
};
