import { useState } from 'react';

import { Story } from '@storybook/react';

import { FormItem, FormItemProps } from '.';

import { BemTextInput } from '../text-input';

export default {
  title: 'Inputs/Form Item',
  component: FormItem,
};

const Template: Story<FormItemProps> = (args) => {
  const [value, setValue] = useState('init');

  return (
    <FormItem errorMessage={!value ? 'No value in here' : undefined} {...args}>
      <BemTextInput onChange={(e) => setValue(e.target.value)} />
    </FormItem>
  );
};

export const Simple = Template.bind({});
Simple.args = {
  label: 'My Label',
  width: '250px',
};

export const Informative = Template.bind({});
Informative.args = {
  ...Simple.args,
  info: 'Fullfill this field when in doubt',
};

export const Error = Template.bind({});
Error.args = { ...Simple.args, errorMessage: 'Required field' };

export const WithStatusIcon = Template.bind({});
WithStatusIcon.args = { ...Simple.args, hasStatusIcon: true };

export const Disable = Template.bind({});
Disable.args = { ...Simple.args, disabled: true };
