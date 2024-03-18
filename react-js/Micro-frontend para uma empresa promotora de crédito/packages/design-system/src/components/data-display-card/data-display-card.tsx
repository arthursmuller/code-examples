import { FC } from 'react';

import { Flex, FlexProps } from '@chakra-ui/react';

export interface CardDisplayDisplayComposition {
  Header: FC<FlexProps>;
  Content: FC<FlexProps>;
  Footer: FC<FlexProps>;
}

const DataDisplayCardHeader: FC<FlexProps> = ({ children, ...otherProps }) => {
  return (
    <Flex
      borderRadius="8px 8px 0 0"
      bgColor="secondary.mid-dark"
      p="16px 24px"
      {...otherProps}
    >
      {children}
    </Flex>
  );
};

const DataDisplayCardContent: FC<FlexProps> = ({ children, ...otherProps }) => {
  return (
    <Flex p="12px 26px" {...otherProps}>
      {children}
    </Flex>
  );
};

const DataDisplayCardFooter: FC<FlexProps> = ({ children, ...otherProps }) => {
  return (
    <Flex
      justifyContent="center"
      alignItems="center"
      borderTop="1px solid"
      borderColor="grey.500"
      borderRadius="0 0 8px 8px"
      minH="51px"
      boxShadow="soft"
      {...otherProps}
    >
      {children}
    </Flex>
  );
};

const DataDisplayCard: FC<FlexProps> & CardDisplayDisplayComposition = ({
  children,
  ...otherProps
}) => {
  return (
    <Flex flexDir="column" maxWidth="392px" {...otherProps}>
      {children}
    </Flex>
  );
};

DataDisplayCard.Header = DataDisplayCardHeader;
DataDisplayCard.Content = DataDisplayCardContent;
DataDisplayCard.Footer = DataDisplayCardFooter;

export { DataDisplayCard };
