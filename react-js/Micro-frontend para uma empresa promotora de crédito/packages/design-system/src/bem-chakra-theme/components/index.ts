import { ThemeComponents } from '@chakra-ui/react';

import { Accordion } from './accordion';
import { Alert } from './alert';
import { Button } from './button/button';
import { Checkbox } from './checkbox';
import { Radio } from './radio';

const components: ThemeComponents = {
  Accordion,
  Alert,
  Button,
  Checkbox,
  Radio,
  // https://github.com/chakra-ui/chakra-ui/issues/2609
  Popover: {
    variants: {
      responsive: {
        popper: {
          maxWidth: 'unset',
          width: 'unset',
        },
      },
    },
  },
};

export default components;
