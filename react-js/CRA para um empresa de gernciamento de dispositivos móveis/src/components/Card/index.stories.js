import React from 'react';

import Card from './index';

export default {
  title: 'Card',
  component: Card,
};

const Template = (args) => <Card {...args} />;

export const Default = Template.bind({});
Default.args = {
  bg: 'white',
  width: '90%',
  d: 'flex',
  flexDirection: 'column',
  m: '3% 0% 1% 3%',
  p: '1.6% 1.3%',
  children: 'Card text',
};
