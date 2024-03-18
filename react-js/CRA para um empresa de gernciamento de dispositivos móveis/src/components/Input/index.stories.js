import { CheckIcon } from '@chakra-ui/icons';
import React from 'react';

import Input from './index';

export default {
  title: 'Input',
  component: Input,
};

const Template = (args) => <Input {...args} />;

export const Default = Template.bind({});
Default.args = {
  inputProps: {
    size: 'lg',
    placeholder: 'Input large',
  },
};

export const withLeftElement = Template.bind({});
withLeftElement.args = {
  inputProps: {
    size: 'lg',
    placeholder: 'Input large',
  },
  leftElement: <CheckIcon w={6} h={6} color="green.500" />,
};

export const withRightElement = Template.bind({});
withRightElement.args = {
  inputProps: {
    size: 'lg',
    placeholder: 'Input large',
  },
  rightElement: <CheckIcon w={6} h={6} color="green.500" />,
};
