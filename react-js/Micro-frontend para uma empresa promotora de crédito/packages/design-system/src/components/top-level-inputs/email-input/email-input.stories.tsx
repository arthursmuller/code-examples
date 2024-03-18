import { Story } from '@storybook/react';
import { useForm } from 'react-hook-form';
import { Flex } from '@chakra-ui/react';

import { EmailInput, EmailInputProps } from './email-input';

export default {
  title: 'Top Level Input/Email Input',
  component: EmailInput,
};

const Template: Story<EmailInputProps> = (args) => {
  const {
    control,
    formState: { errors },
  } = useForm({ mode: 'onChange' });

  return (
    <Flex as="form" w="250px">
      <EmailInput
        {...args}
        errorMessage={errors?.email?.message}
        control={control}
      />
    </Flex>
  );
};

export const Default = Template.bind({});
Default.args = {
  defaultValue: '',
};
