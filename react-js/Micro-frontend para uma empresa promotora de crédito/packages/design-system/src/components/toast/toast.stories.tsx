import { Story } from '@storybook/react';

import { InternetOnIcon } from '@pcf/design-system-icons';

import { Toast, ToastProps } from './toast';

export default {
  title: 'Toast',
  component: Toast,
  argTypes: {
    status: {
      control: {
        type: 'inline-radio',
        options: ['error', 'success'],
      },
    },
  },
};

const Template: Story<ToastProps> = (args) => <Toast {...args} />;

export const Default = Template.bind({});
Default.args = {
  status: 'success',
  title: 'Your title',
  description: 'Your description',
  icon: InternetOnIcon,
};
