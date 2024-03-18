import { Story } from '@storybook/react';

import { BemAlert, BemAlertProps } from './alert';

export default {
  title: 'BemAlert',
  component: BemAlert,
  argTypes: {
    status: {
      control: {
        type: 'inline-radio',
        options: ['error', 'success', 'info', 'warning'],
      },
    },
    variant: {
      control: {
        type: 'inline-radio',
        options: ['subtle', 'solid', 'left-accent'],
      },
    },
  },
};

const Template: Story<BemAlertProps> = (args) => <BemAlert {...args} />;

export const Default = Template.bind({});
Default.args = {
  status: 'success',
  description: 'Your description',
};

export const WithTitle = Template.bind({});
WithTitle.args = {
  status: 'error',
  variant: 'subtle',
  description: 'Your description',
  title: 'Title',
};
