import { FC } from 'react';

import { Center, Text } from '@chakra-ui/react';

interface NoDataDisplayProps {
  entityName?: string;
  customPhrase?: string;
}

export const NoDataDisplay: FC<NoDataDisplayProps> = ({
  entityName = '',
  customPhrase,
  children,
}) => {
  const suffix: string | false =
    (entityName.endsWith('a') ||
      entityName.endsWith('agem') ||
      entityName.endsWith('idade') ||
      entityName.endsWith('ção')) &&
    'a';

  return (
    <Center
      flexDirection="column"
      padding="23px"
      width="100%"
      borderRadius="8px"
      backgroundColor="grey.200"
      marginY="16px"
    >
      <Text
        textStyle="regular24"
        color="grey.700"
        textAlign="center"
        marginBottom={children ? '20px' : 0}
      >
        {customPhrase ||
          `Você ainda não possui nenhum${suffix || ''} ${entityName} cadastrad${
            suffix || 'o'
          }`}
      </Text>

      {children}
    </Center>
  );
};
