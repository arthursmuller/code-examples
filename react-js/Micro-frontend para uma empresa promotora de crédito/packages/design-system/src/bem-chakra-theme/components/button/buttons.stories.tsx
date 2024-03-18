import { Story } from '@storybook/react';
import { Button, ButtonProps } from '@chakra-ui/react';

import { ColorSchemes } from '../../foundations/colors';

const colorSchemes = Object.values(ColorSchemes);

export default {
  title: 'Button',
  component: Button,
  argTypes: {
    colorScheme: {
      control: {
        type: 'select',
        options: colorSchemes,
      },
    },
    variant: {
      control: {
        type: 'select',
        options: ['solid', 'outline', 'ghost', 'link'],
      },
    },
  },
};

interface StoryProps extends ButtonProps {
  text: string;
}

const Template: Story<StoryProps> = ({ text, ...args }) => (
  <Button {...args}>{text}</Button>
);

export const Default = Template.bind({});
Default.args = {
  text: 'My button',
  colorScheme: 'primary',
  isLoading: false,
  loadingText: 'Loading',
  disabled: false,
};

export const Solid: Story<StoryProps> = () => (
  <div style={{ display: 'flex', gap: '16px' }}>
    <Button variant="solid" colorScheme="primary">
      primary
    </Button>
    <Button variant="solid" colorScheme="secondary">
      secondary
    </Button>
    <Button variant="solid" colorScheme="grey">
      base
    </Button>
    <Button variant="solid" disabled>
      disabled
    </Button>
  </div>
);

export const Outlined: Story<StoryProps> = () => (
  <div style={{ display: 'flex', gap: '16px' }}>
    <Button variant="outline" colorScheme="primary">
      primary
    </Button>
    <Button variant="outline" colorScheme="secondary">
      secondary
    </Button>
    <Button variant="outline" disabled>
      disabled
    </Button>
  </div>
);

export const Ghost: Story<StoryProps> = () => (
  <div style={{ display: 'flex', gap: '16px' }}>
    <Button variant="ghost" colorScheme="primary">
      primary
    </Button>
    <Button variant="ghost" colorScheme="secondary">
      secondary
    </Button>
    <Button variant="ghost" disabled>
      disabled
    </Button>
  </div>
);

export const Links: Story<StoryProps> = () => (
  <div style={{ display: 'flex', gap: '16px' }}>
    <Button variant="link" colorScheme="primary">
      primary
    </Button>
    <Button variant="link" colorScheme="secondary">
      secondary
    </Button>
    <Button variant="link" disabled>
      disabled
    </Button>
  </div>
);
