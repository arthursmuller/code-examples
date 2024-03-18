import { Story } from '@storybook/react';

import { BemAccordion, BemAccordionProps } from './accordion';
import { BemAccordionItem } from './accordion-item';

export default {
  title: 'Accordion',
  component: BemAccordion,
  argTypes: {
    colorScheme: {
      control: {
        type: 'inline-radio',
        options: ['primary', 'secondary'],
      },
    },
  },
};

const Template: Story<BemAccordionProps> = (args) => <BemAccordion {...args} />;

export const Default = Template.bind({});
Default.args = {
  colorScheme: 'primary',
  items: [
    { title: 'Naruto', body: "Doesn't cast Hadoukens." },
    { title: 'Ryu', body: "Doesn't cast Hasengans." },
    { title: 'Sub-zero', body: "Doesn't care." },
  ],
};

export const Childs = Template.bind({});
Childs.args = {
  colorScheme: 'secondary',
  children: [
    <BemAccordionItem title="Imma a child" body="no body" />,
    <BemAccordionItem title="Me too" body="no body" />,
  ],
};
