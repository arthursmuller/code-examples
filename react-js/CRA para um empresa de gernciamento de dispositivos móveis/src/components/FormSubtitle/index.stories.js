import React from 'react';

import FormSubtitle from './index';

export default {
  title: 'FormSubtitle',
  component: FormSubtitle,
};

const Template = (args) => <FormSubtitle {...args} />;

export const Default = Template.bind({});
Default.args = {
  subtitle: 'Subtitle',
  description: 'Description',
  children: 'Children FormSubtitle',
};
