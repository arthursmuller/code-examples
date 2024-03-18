import { Story } from '@storybook/react';

import {
  ExpandableSection,
  ExpandableSectionProps,
} from './expandable-section';

export default {
  title: 'Expandable Section',
  component: ExpandableSection,
};

const Template: Story<ExpandableSectionProps> = (args) => (
  <ExpandableSection {...args} />
);

export const Default = Template.bind({});
Default.args = {
  title: 'My stuff',
  children: <div> 32435670101-785239562340913120-312 </div>,
};
