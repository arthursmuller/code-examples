import { FC } from 'react';

import { Flex, Box, Button, Text, useBreakpointValue } from '@chakra-ui/react';
import Link from 'next/link';

import { CustomHeading } from '@pcf/design-system';
import { RoutesEnum as PublicRoutesEnum } from 'app/routes/routes.enum';

export const CreateAccountCard: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });
  const textSize = useBreakpointValue(['16', '16', '24']);

  return (
    <Box
      position="relative"
      mt={['30px', '30px', '60px']}
      maxWidth="635px"
      marginRight={[7, 7, 0]}
    >
      <Flex
        flexDir="column"
        justifyContent="space-between"
        layerStyle={['card', 'card', 'card-without-border-radius-left']}
        w="100%"
        minH={['177px', '177px', '328px']}
        p={['24px', '24px', '32px 80px 24px 48px']}
      >
        {!isMobile && (
          <CustomHeading textStyle="bold48" color="secondary.mid-dark">
            Simule você mesmo a sua proposta
          </CustomHeading>
        )}

        <Text
          textAlign={['center', 'center', 'initial']}
          textStyle={`regular${textSize}`}
        >
          <Text color="primary.regular" textStyle={`bold${textSize}`} as="span">
            Criando uma conta
          </Text>{' '}
          sua simulação fica mais completa e seu acesso mais rápido.
        </Text>

        <Box w={['100%', '100%', 'auto']}>
          <Link href={PublicRoutesEnum.SignUp}>
            <Button w={['100%', '100%', 'auto']} mt={2}>
              Criar uma conta
            </Button>
          </Link>
        </Box>
      </Flex>
    </Box>
  );
};
