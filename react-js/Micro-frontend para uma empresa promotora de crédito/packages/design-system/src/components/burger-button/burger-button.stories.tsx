import { useState } from 'react';

import { Story } from '@storybook/react';

import { BurgerButton, BurgerButtonProps } from './burger-button';

export default {
  title: 'Burger button',
  component: BurgerButton,
};

const Template: Story<BurgerButtonProps> = (args) => {
  const [show, setShow] = useState<boolean>(false);

  return (
    <BurgerButton expanded={show} onClick={() => setShow(!show)} {...args} />
  );
};

export const Default = Template.bind({});
Default.args = {
  color: 'blue',
};
