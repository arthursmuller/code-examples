import { Story } from '@storybook/react';

import { BemDatePicker, BemDatePickerProps } from './date-picker';

import { FormItem } from '../form-item';

export default {
  title: 'Inputs/Date picker',
  component: BemDatePicker,
};

export const Date: Story<BemDatePickerProps> = (args) => (
  <FormItem label="Date picker" width="250px">
    <BemDatePicker {...args} />
  </FormItem>
);
