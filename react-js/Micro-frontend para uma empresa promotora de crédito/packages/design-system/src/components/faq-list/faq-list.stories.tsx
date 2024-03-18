import { Text } from '@chakra-ui/react';
import { Story } from '@storybook/react';

import { FaqList, FaqListProps } from './faq-list';

export default {
  title: 'FAQ list',
  component: FaqList,
};

const Template: Story<FaqListProps> = (props) => <FaqList {...props} />;

export const defaultFaq = Template.bind({});
defaultFaq.args = {
  questions: [
    {
      question: 'Is Naruto better than Sasuke?',
      answer: 'That is not even a question.',
    },
    {
      question: 'Can Astorias leave the Abyss?',
      answer: (
        <Text>
          Only <strong>if</strong> he is to stay together with Sif :(
        </Text>
      ),
    },
  ],
};
