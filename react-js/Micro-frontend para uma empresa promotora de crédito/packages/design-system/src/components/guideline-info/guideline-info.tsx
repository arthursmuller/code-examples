import { FC } from 'react';

import { GridItem, Icon, Text, Center } from '@chakra-ui/react';

const colorSchemes = {
  white: {
    color: 'grey.100',
    iconColor: 'grey.100',
  },
  secondary: {
    color: 'grey.800',
    iconColor: 'secondary.mid-dark',
  },
};

export interface GuidelineInfoProps {
  icon: FC;
  information?: string;

  colorScheme?: keyof typeof colorSchemes;

  align?: string;
  color?: string;
  iconColor?: string;
  customBg?: string;
}

export const GuidelineInfo: FC<GuidelineInfoProps> = ({
  icon,
  information,
  customBg,
  colorScheme = 'secondary',
  align = 'center',
  color,
  iconColor,
  children,
}) => (
  <GridItem align={align}>
    <Center
      marginRight="8px"
      borderRadius="full"
      border="3px solid"
      padding={2}
      height="56px"
      width="56px"
      marginBottom="8px"
      background={customBg}
      color={iconColor || colorSchemes[colorScheme].iconColor}
    >
      <Icon
        as={icon}
        height="100%"
        width="100%"
        color={iconColor || colorSchemes[colorScheme].iconColor}
      />
    </Center>
    <Text
      as="p"
      textStyle="regular14"
      color={color || colorSchemes[colorScheme].color}
    >
      {information || children}
    </Text>
  </GridItem>
);
