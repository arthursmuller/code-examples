import React from 'react';

import Text from './index';

export default {
  title: 'Text',
  component: Text,
};

const Template = (args) => <Text {...args} />;

export const Default = Template.bind({});
Default.args = {
  color: 'gray.500',
  fontWeight: '300',
  m: '2% 0% 3% 0%',
  children: 'text',
};
