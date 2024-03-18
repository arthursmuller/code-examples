import { FC, ReactElement } from 'react';

import { Divider, Flex, Text } from '@chakra-ui/react';

export interface InfoLineProps {
  label: string;
  value?: string;
  custom?: ReactElement;
}

export const InfoLine: FC<InfoLineProps> = ({ label, value, custom }) => (
  <>
    <Flex justifyContent="space-between" marginBottom="8px">
      <Text as="p" textStyle="regular16">
        {label}:
      </Text>

      {custom || (
        <Text as="p" textStyle="bold16" textAlign="end">
          {value}
        </Text>
      )}
    </Flex>

    <Divider borderColor="grey.300" marginBottom="8px" />
  </>
);
