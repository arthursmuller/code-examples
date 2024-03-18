import { Story } from '@storybook/react';

import { ButtonCard, ButtonCardProps } from './button-card';

import { ColorSchemes } from '../../bem-chakra-theme/foundations/colors';

const [primary, secondary] = Object.values(ColorSchemes);

export default {
  title: 'Button-card',
  component: ButtonCard,
  argTypes: {
    colorScheme: {
      control: {
        type: 'select',
        options: [primary, secondary],
      },
    },
  },
};

const Template: Story<ButtonCardProps> = (args) => <ButtonCard {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: 'Minhas Mensagens',
  description: 'Confira aqui as mensagens que a Bem mandou para você.',
};

export const Disabled = Template.bind({});
Disabled.args = {
  title: 'Minhas Mensagens',
  description: 'Confira aqui as mensagens que a Bem mandou para você.',
  disabled: true,
};
