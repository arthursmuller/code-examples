import { FC, FunctionComponent, ReactElement, ReactNode } from 'react';

import {
  AccordionButton,
  AccordionItem,
  AccordionItemProps,
  AccordionPanel,
  AccordionPanelProps,
  Box,
  Icon,
  useBreakpointValue,
} from '@chakra-ui/react';

import { ArrowUpIcon } from '@pcf/design-system-icons';

import { BemErrorBoundary } from '../error';

export interface BemAccordionItem {
  icon?: FunctionComponent;
  title: string;
  body: FunctionComponent | ReactElement | ReactNode;
}

interface BemAccordionItemProps extends AccordionItemProps {
  icon?: FunctionComponent;
  title: string;
  body: FunctionComponent | ReactElement | ReactNode;
  accordionPanelProps?: AccordionPanelProps;
}

export const BemAccordionItem: FC<BemAccordionItemProps> = ({
  title,
  icon,
  body,
  accordionPanelProps = {},
  ...rest
}) => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  return (
    <AccordionItem
      {...rest}
      borderRadius="8px"
      borderColor="transparent"
      mb="20px"
      boxShadow="medium"
    >
      {({ isExpanded }) => (
        <>
          <AccordionButton {...(isMobile ? { _hover: {} } : {})}>
            <>
              {icon && <Icon width="28px" height="28px" as={icon} />}
              <Box ml={icon ? 4 : 0} flex="1" textAlign="left">
                {title}
              </Box>
            </>

            <Icon
              as={ArrowUpIcon}
              width="16px"
              h="10px"
              transform={isExpanded ? 'rotate(0deg)' : 'rotate(180deg)'}
              transition="transform .25s"
            />
          </AccordionButton>

          <AccordionPanel padding={4} {...accordionPanelProps}>
            <BemErrorBoundary>{body}</BemErrorBoundary>
          </AccordionPanel>
        </>
      )}
    </AccordionItem>
  );
};
