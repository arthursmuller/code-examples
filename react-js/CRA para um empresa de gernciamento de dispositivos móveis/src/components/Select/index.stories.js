import React from 'react';

import Select from './index';

export default {
  title: 'Select',
  component: Select,
};

const Template = (args) => <Select {...args} />;

export const Default = Template.bind({});
Default.args = {
  iconColor: 'gray.600',
  placeholder: 'RS',
  borderColor: 'gray.600',
  height: '45px',
  color: 'black.500',
};
