import { Story } from '@storybook/react';
import { useForm } from 'react-hook-form';
import NumberFormat from 'react-number-format';
import { isValidCPF } from '@brazilian-utils/brazilian-utils';

import { FormItemControl } from './form-item-control';

import { BemTextInput } from '../text-input';

export default {
  title: 'Inputs/Form Item Control',
  component: FormItemControl,
};

const Template: Story = (args) => {
  const {
    control,
    formState: { errors },
  } = useForm<{ storyBook: string }>({
    mode: 'onChange',
  });

  return (
    <FormItemControl
      {...args}
      name="storyBook"
      errorMessage={errors?.storyBook?.message}
      control={control}
    />
  );
};

export const WithAs = Template.bind({});
WithAs.args = {
  label: 'Text Input w/ as props (as)',
  width: '250px',
  required: true,
  defaultValue: '',
  as: BemTextInput,

  type: 'number',
};

export const WithRender = Template.bind({});
WithRender.args = {
  label: 'Tax Number Input (render)',
  width: '250px',
  required: true,
  defaultValue: '',
  render: (data) => <NumberFormat {...data} format="###.###.###-##" mask="_" />,
};

export const Validators = Template.bind({});
Validators.args = {
  ...WithRender.args,
  rules: {
    validate: {
      cpfValidator(cpf) {
        if (!isValidCPF(cpf)) {
          return 'CPF inválido';
        }
        return true;
      },
    },
  },
};
