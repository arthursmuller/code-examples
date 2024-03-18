import { FC } from 'react';

import { Center, Flex, Icon, Text } from '@chakra-ui/react';

import textStyles from '../../../bem-chakra-theme/foundations/typography';

export interface DefaultModalHeaderProps {
  icon?: FC;
  iconBg?: string;
  bg: string;
  color?: string;
  iconColor?: string;
  title?: string;
  textStyle?: keyof typeof textStyles;
  iconSize?: string | number;
}

export const DefaultModalheader: FC<DefaultModalHeaderProps> = ({
  color = 'white',
  iconColor,
  icon,
  iconBg,
  bg,
  title = '',
  textStyle = 'bold32',
  iconSize,
}) => (
  <Flex
    direction={['column', 'column', 'row', 'row']}
    px={['16px', '16px', '30px']}
    justify="center"
    align="center"
    width="100%"
    bg={bg}
    borderTopRadius="8px"
    paddingTop={['24px', '24px', '32px']}
    marginBottom="24px"
  >
    <Center
      height="72px"
      minWidth="72px"
      bg={iconBg}
      borderRadius="100%"
      mb={['24px', '24px', '32px']}
    >
      <Icon
        as={icon}
        width={iconSize || '42px'}
        height={iconSize || '42px'}
        color={iconColor || color}
      />
    </Center>

    <Text
      mb={['24px', '24px', '32px']}
      ml={['0', '0', '16px', '16px']}
      as="h2"
      textStyle={textStyle}
      textAlign={['center', 'center', 'left', 'left']}
      color={color}
    >
      {title}
    </Text>
  </Flex>
);
