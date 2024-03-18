import { Story } from '@storybook/react';

import { FullLayoutCard } from './full-layout-card';

export default {
  title: 'Full layout card',
  component: FullLayoutCard,
};

interface StoryProps {
  title: string;
  content: string;

  demoBackgroundColor: string;
  demoPaddingX: number;
}

const Template: Story<StoryProps> = ({
  title,
  content,
  demoBackgroundColor: backgroundColor,
  demoPaddingX,
}) => {
  return (
    <div
      style={{
        backgroundColor,
        paddingTop: 60,
        paddingLeft: demoPaddingX,
        paddingRight: demoPaddingX,
      }}
    >
      <FullLayoutCard title={title}>{content}</FullLayoutCard>
    </div>
  );
};

export const defaultCard = Template.bind({});
defaultCard.args = {
  demoBackgroundColor: 'blue',
  title: 'My card',
  content: '...any content here.',
  demoPaddingX: 0,
};
