import { Story } from '@storybook/react';

import { AddButton, AddButtonProps } from '.';

export default {
  title: 'Add Button',
  component: AddButton,
};

const Template: Story<AddButtonProps> = (args) => <AddButton {...args} />;

export const Default = Template.bind({});
Default.args = {
  text: 'Cadastrar Telefone',
};

export const Disabled = Template.bind({});
Disabled.args = {
  text: 'Cadastrar Telefone alternativo',
  disabled: true,
};
