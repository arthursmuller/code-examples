import { Story } from '@storybook/react';

import { Loader, LoaderProps } from './loader';

export default {
  title: 'Loader',
  component: Loader,
  argTypes: {
    theme: {
      control: {
        type: 'select',
        options: ['primary', 'white'],
      },
    },
    size: {
      control: {
        type: 'select',
        options: ['sm', 'md', 'lg'],
      },
    },
  },
};

const Template: Story<LoaderProps> = (args) => (
  <div
    style={{
      backgroundColor: args.theme === 'white' ? 'orange' : 'none',
      width: '240px',
    }}
  >
    <Loader {...args} />
  </div>
);

export const Default = Template.bind({});
Default.args = {
  fullHeight: false,
  theme: 'primary',
  size: 'md',
};
