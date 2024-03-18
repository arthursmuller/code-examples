import { FC } from 'react';

import { Flex, Text, Button, Box, Divider } from '@chakra-ui/react';
import { Link as RouterLink } from 'react-router-dom';

import { Notificacao } from '@pcf/core';

export interface NotificationLineProps extends Notificacao {
  onClick?: () => void;
  showDivider?: boolean;
}

export const NotificationLine: FC<NotificationLineProps> = ({
  titulo,
  descricao,
  urlReferencia,
  onClick,
  showDivider,
}) => (
  <>
    {showDivider && (
      <Divider
        borderColor="secondary.washed"
        marginBottom={4}
        gridColumnEnd="span 2"
      />
    )}

    <Text
      textStyle={['bold20', 'bold20', 'regular16']}
      color={['secondary.mid-dark', 'secondary.mid-dark', 'grey.800']}
      marginBottom={[4, 4, 0]}
    >
      {titulo}
    </Text>

    <Flex
      marginBottom={4}
      direction={['column', 'column', 'row']}
      alignItems="baseline"
    >
      <Text flex={1} marginRight={urlReferencia ? 8 : 0}>
        {descricao}
      </Text>
      {urlReferencia ? (
        <Button
          variant="link"
          textDecoration="underline"
          as={RouterLink}
          to={urlReferencia}
          height="fit-content"
          onClick={onClick}
          marginTop={[4, 4, 0]}
        >
          Acessar
        </Button>
      ) : (
        <Box as="span" />
      )}
    </Flex>
  </>
);
