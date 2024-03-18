import { Story } from '@storybook/react';

import { BemCurrencyInput, BemCurrencyInputProps } from './currency-input';

import { FormItem } from '../form-item';

export default {
  title: 'Inputs/Currency Input',
  component: BemCurrencyInput,
};

export const Currency: Story<BemCurrencyInputProps> = (args) => (
  <FormItem label="Currency" width="250px">
    <BemCurrencyInput {...args} />
  </FormItem>
);
