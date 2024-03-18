import { FC } from 'react';

import { AccordionProps, Box, BoxProps, Text } from '@chakra-ui/react';

import { FaqQuestion } from './faq-question.model';

import { BemAccordion } from '../accordion';

export interface FaqListProps extends AccordionProps {
  questions?: FaqQuestion[];
  chakraProps?: BoxProps;
}

export const FaqList: FC<FaqListProps> = ({
  questions,
  chakraProps,
  ...accordionProps
}) => (
  <Box marginTop="24px" flex={1} {...chakraProps}>
    <BemAccordion
      {...accordionProps}
      items={questions.map((entry, index) => ({
        key: `${index}`,
        title: entry.question,
        body:
          typeof entry.answer === 'string' ? (
            <Text textStyle="regular16" whiteSpace="pre-line">
              {entry.answer.trim()}
            </Text>
          ) : (
            entry.answer
          ),
      }))}
    />
  </Box>
);
