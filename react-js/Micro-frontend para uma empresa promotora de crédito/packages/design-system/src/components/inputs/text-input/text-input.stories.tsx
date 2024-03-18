import { useState } from 'react';

import { Story } from '@storybook/react';

import { BemTextInput, BemTextInputProps } from './text-input';

import { FormItem } from '../form-item';

export default {
  title: 'Inputs/Input',
  component: BemTextInput,
};

const Template: Story<BemTextInputProps> = (args) => {
  const [value, setValue] = useState('init');

  return (
    <FormItem
      label="Text"
      width="250px"
      errorMessage={!value ? 'No value in here' : undefined}
    >
      <BemTextInput {...args} onChange={(e) => setValue(e.target.value)} />
    </FormItem>
  );
};

export const Text = Template.bind({});
Text.args = {};

export const Number = Template.bind({});
Number.args = { ...Text.args, type: 'number' };
