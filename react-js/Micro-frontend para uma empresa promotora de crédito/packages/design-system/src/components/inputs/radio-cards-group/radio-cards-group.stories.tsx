import { Story } from '@storybook/react';
import { Flex } from '@chakra-ui/react';

import { RadioCard, RadioCardProps } from './radio-card';
import { RadioCardsGroupProps, RadioCardsGroup } from './radio-cards-group';
import { RadioCardVariant } from './radio-card-variant';

export default {
  title: 'Inputs/Radio Cards',
  component: RadioCardsGroup,
};

interface StoryProps extends RadioCardsGroupProps {
  cards: RadioCardProps[];
}

const Template: Story<StoryProps> = ({ name, onChange, cards, isRadio }) => {
  return (
    <RadioCardsGroup
      name={name || 'default name'}
      onChange={onChange}
      isRadio={isRadio}
    >
      {cards.map((cardProps, index) => (
        <RadioCard
          key={`${index}`}
          value={cardProps.value}
          title={cardProps.title}
          information={cardProps.information}
          subtitle={cardProps.subtitle}
          customContent={cardProps.customContent}
          customFooter={cardProps.customFooter}
        />
      ))}
    </RadioCardsGroup>
  );
};

export const defaultRadioCards = Template.bind({});
defaultRadioCards.args = {
  name: 'My favorite framework',
  cards: [
    {
      title: 'React',
      information: 'Clearly the favorite.',
      value: 'react',
    },
    {
      title: 'Preact',
      information: 'Whats better than React but lighter?',
      value: 'preact',
    },
    {
      title: 'Vue',
      information: 'Hipster tech.',
      value: 'vue',
    },
  ],
};

export const customContent = Template.bind({});
customContent.args = {
  name: 'My favorite framework',
  cards: [
    {
      title: 'React',
      information: 'Clearly the favorite.',
      value: 'react',
      subtitle: 'pick your poison',
      customFooter: <div>I have a custom footer here</div>,
      customContent: <Flex ml="16px">I have a custom content here</Flex>,
    },
    {
      title: 'Preact',
      information: 'Whats better than React but lighter?',
      value: 'preact',
      subtitle: 'pick your poison',
      customFooter: <div>I have a custom footer here</div>,
      customContent: <Flex ml="16px">I have a custom content here</Flex>,
    },
  ],
};

export const multiselect = Template.bind({});
multiselect.args = {
  name: 'Frameworks I like',
  isRadio: false,
  cards: [
    {
      title: 'React',
      information: 'Clearly the favorite.',
      value: 'react',
    },
    {
      title: 'Preact',
      information: 'Whats better than React but lighter?',
      value: 'preact',
    },
    {
      title: 'Vue',
      information: 'Hipster tech.',
      value: 'vue',
    },
  ],
};

const VariantTemplate: Story<StoryProps> = ({
  name,
  onChange,
  cards,
  isRadio,
}) => {
  return (
    <RadioCardsGroup
      name={name || 'default name'}
      onChange={onChange}
      isRadio={isRadio}
    >
      {cards.map((cardProps, index) => (
        <RadioCardVariant
          key={`${index}`}
          value={cardProps.value}
          header={<>{cardProps.title}</>}
          content={<>{cardProps.information}</>}
          footer={<>{cardProps.subtitle}</>}
        />
      ))}
    </RadioCardsGroup>
  );
};
export const variant = VariantTemplate.bind({});
variant.args = {
  name: 'Frameworks I like',
  cards: [
    {
      title: 'React',
      information: 'Clearly the favorite.',
      value: 'react',
      subtitle: 'footer',
    },
    {
      title: 'Preact',
      information: 'Whats better than React but lighter?',
      value: 'preact',
      subtitle: 'footer',
    },
    {
      title: 'Vue',
      information: 'Hipster tech.',
      value: 'vue',
      subtitle: 'footer',
    },
  ],
};
