import React from 'react';

import Heading from './index';

export default {
  title: 'Heading',
  component: Heading,
};

const Template = (args) => <Heading {...args} />;

export const Default = Template.bind({});
Default.args = {
  color: 'black.500',
  fontSize: '5xl',
  fontWeight: 'light',
  children: 'Heading',
};
