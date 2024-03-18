import { FC, ReactElement } from 'react';

import { Text } from '@chakra-ui/react';

interface SubtitleProps {
  subtitle?: string | ReactElement;
  subtitleOrange?: string;
  width?: string | string[];
}

export const Subtitle: FC<SubtitleProps> = ({
  subtitle,
  subtitleOrange = 'Empréstimo fácil',
  width,
}) => (
  <Text
    textStyle="regular20_24"
    color="white"
    mt={5}
    w={width || ['250px', '250px', '296px']}
  >
    <Text as="span" bg="primary.light">
      {subtitleOrange}
    </Text>{' '}
    {subtitle || 'e rápido para o que você precisar.'}
  </Text>
);
