import { Story } from '@storybook/react';

import { BemDateInput, BemDateInputProps } from './date-input';

import { FormItem } from '../form-item';

export default {
  title: 'Inputs/Date',
  component: BemDateInput,
};

export const Date: Story<BemDateInputProps> = (args) => (
  <FormItem label="Date" width="250px">
    <BemDateInput {...args} />
  </FormItem>
);
