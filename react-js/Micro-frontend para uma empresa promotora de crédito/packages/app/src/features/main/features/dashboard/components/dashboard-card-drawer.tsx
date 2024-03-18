import { FC } from 'react';

import {
  Flex,
  Text,
  Icon,
  VStack,
  StackDivider,
  SimpleGrid,
  FlexProps,
} from '@chakra-ui/react';

interface CardHeaderProps {
  title: string;
  icon: React.FC;
}

const CardHeader: FC<CardHeaderProps> = ({ icon, title }) => {
  return (
    <Flex>
      <Icon as={icon} boxSize="26px" color="primary.regular" mr="14px" />
      <Text textStyle="bold16" color="primary.regular">
        {title}
      </Text>
    </Flex>
  );
};

const CardBody: FC = ({ children }) => {
  return (
    <SimpleGrid
      w="100%"
      columns={2}
      spacing={3}
      sx={{ 'div:nth-of-type(2n)': { textAlign: 'center' } }}
    >
      {children}
    </SimpleGrid>
  );
};

interface DashboardCardDrawerComposition {
  Header: FC<CardHeaderProps>;
  Body: FC;
  Footer: FC<FlexProps>;
}

const DashboardCardDrawer: FC & DashboardCardDrawerComposition = ({
  children,
}) => {
  return (
    <VStack
      spacing={3}
      align="start"
      p="16px"
      px="16px"
      mb="16px"
      layerStyle="card"
      divider={<StackDivider borderColor="grey.500" />}
      boxShadow="card"
    >
      {children}
    </VStack>
  );
};

DashboardCardDrawer.Header = CardHeader;
DashboardCardDrawer.Body = CardBody;
DashboardCardDrawer.Footer = Flex;

export { DashboardCardDrawer };
