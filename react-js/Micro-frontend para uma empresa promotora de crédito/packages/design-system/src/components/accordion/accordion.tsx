import React, { FC } from 'react';

import { Accordion, AccordionProps } from '@chakra-ui/react';

import { BemAccordionItem } from './accordion-item';

export interface BemAccordionProps extends AccordionProps {
  items?: BemAccordionItem[];
}

export const BemAccordion: FC<BemAccordionProps> = ({
  items,
  children,
  ...rest
}) => (
  <Accordion allowMultiple {...rest}>
    {items && items.map((item) => <BemAccordionItem {...item} />)}

    {children}
  </Accordion>
);
