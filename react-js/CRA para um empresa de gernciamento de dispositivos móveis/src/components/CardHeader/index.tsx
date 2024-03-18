import { Box, BoxProps, Divider, Image, Text } from '@chakra-ui/react';
import { ReactNode } from 'react';

import Card from '../Card';

interface CardHeaderProps extends BoxProps {
  description?: ReactNode;
  iconUrl?: string;
  title: string;
}

const CardHeader = ({ description, iconUrl, title, ...rest }: CardHeaderProps) => {
  return (
    <Card {...rest}>
      <Box d="flex" alignItems="center">
        {iconUrl ? (
          <Image
            src={iconUrl}
            w="40px"
            h="40px"
            mr="20px"
            objectFit="contain"
          />
        ) : null}
        <Text margin="0" color="gray.500" fontSize="2xl" fontWeight="600">
          {title}
        </Text>
      </Box>
      {title && description ? (
        <Box m="16px 0 16px">
          <Divider borderColor="gray.600" orientation="horizontal" />
        </Box>
      ) : null}
      {description ? description : null}
    </Card>
  );
};

export default CardHeader;
