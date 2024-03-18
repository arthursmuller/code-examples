import { useEffect, useState } from 'react';

import { Story } from '@storybook/react';

import { BemSelect } from './bem-select';

import { FormItem } from '../form-item';
import { BemSelectProps, SelectOption } from './select.model';

export default {
  title: 'Inputs/Select',
  component: BemSelect,
};

const customOptions: SelectOption[] = [
  { value: '1', name: 'Chocolate Chocolate Chocolate Chocolate' },
  { value: '2', name: 'Strawberry' },
  { value: '3', name: 'Vanilla' },
];

const Template: Story<BemSelectProps> = (args) => {
  const [opts, setOpts] = useState<SelectOption[]>([]);

  useEffect(() => {
    setTimeout(() => setOpts(customOptions), 3000);
  }, []);

  return (
    <FormItem label="Select" width="250px">
      <BemSelect {...args} options={opts} isLoading={!opts.length} />
    </FormItem>
  );
};

export const Default = Template.bind({});
Default.args = {
  defaultValue: '2',
  options: customOptions,
};
