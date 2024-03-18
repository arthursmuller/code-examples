import { Story } from '@storybook/react';
import { useForm } from 'react-hook-form';
import { Flex } from '@chakra-ui/react';

import { PhoneInput, PhoneInputProps } from './phone-input';

export default {
  title: 'Top Level Input/Phone Input',
  component: PhoneInput,
};

const Template: Story<PhoneInputProps> = (args) => {
  const {
    control,
    formState: { errors },
  } = useForm({ mode: 'onChange' });

  return (
    <Flex as="form" w="250px">
      <PhoneInput
        {...args}
        name="telefone"
        errorMessage={errors?.telefone?.message}
        control={control}
      />
    </Flex>
  );
};

export const Default = Template.bind({});
Default.args = {
  label: 'Telefone',
  defaultValue: '',
  acceptLandlinePhone: true,
  acceptMobilePhone: true,
};
