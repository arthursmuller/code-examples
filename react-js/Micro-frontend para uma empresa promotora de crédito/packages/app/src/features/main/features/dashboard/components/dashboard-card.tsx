import { FC, FunctionComponent, ReactElement, ReactNode } from 'react';

import { Flex, Icon, Text } from '@chakra-ui/react';

interface DashboardCardProps {
  icon: FunctionComponent;
  title: string;
  body: FunctionComponent | ReactElement | ReactNode;
}

export const DashboardCard: FC<DashboardCardProps> = ({
  title,
  icon,
  body,
}) => (
  <Flex
    flexDir="column"
    layerStyle="card"
    w="100%"
    p="16px"
    mx="2px"
    mt="33px"
    mb={6}
    minHeight="160px"
  >
    <Flex alignItems="center" mb="12px">
      <Icon
        width="24px"
        height="24px"
        mr="16px"
        color="primary.regular"
        as={icon}
      />
      <Text as="p" textStyle="bold12" color="primary.regular">
        {title}
      </Text>
    </Flex>

    {body}
  </Flex>
);
