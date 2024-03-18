import { FC } from 'react';

import { Flex } from '@chakra-ui/react';

import { StepItem, StepItemData, StepStatusSize } from './step-item';

export interface StepsProgressBarProps {
  items: StepItemData[];
  isHorizontal?: boolean;
  size?: StepStatusSize;
  showLabels?: boolean;
}

export const StepsProgressBar: FC<StepsProgressBarProps> = ({
  items = [],
  isHorizontal,
  size = 'md',
  showLabels = true,
}) => {
  return (
    <Flex
      display="flex"
      alignItems="flex-start"
      h="auto"
      minHeight="100%"
      flexDir={isHorizontal ? 'row' : 'column'}
      w="100%"
      justifyContent="space-between"
    >
      {items.map(({ label, ...rest }) => (
        <StepItem
          showLabels={showLabels}
          label={label}
          isHorizontal={isHorizontal}
          key={label}
          size={size}
          {...rest}
        />
      ))}
    </Flex>
  );
};
