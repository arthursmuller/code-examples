import { Text, Box, Button, Flex, Icon, Center } from '@chakra-ui/react';

import { StepCard, CustomHeading } from '@pcf/design-system';
import { CheckIcon } from '@pcf/design-system-icons';
import { AutenticacaoModel } from '@pcf/core';
import { useAuthContext } from 'app/auth/auth.context';

interface NewPasswordSuccessStepProps {
  autenticacaoModel?: AutenticacaoModel;
}

export const NewPasswordSuccessStep: React.FC<NewPasswordSuccessStepProps> = ({
  autenticacaoModel = {},
}) => {
  const { onLoginSuccess } = useAuthContext();

  const handleLogin = (): void => {
    const { email, token } = autenticacaoModel;

    if (email && token) {
      onLoginSuccess({ email, jwt: token });
    }
  };

  return (
    <>
      <StepCard>
        <Flex justifyContent="center">
          <Center
            borderRadius="full"
            w="56px"
            h="56px"
            borderColor="secondary.mid-dark"
            borderWidth="5px"
            flexShrink={0}
          >
            <Icon
              as={CheckIcon}
              color="secondary.mid-dark"
              width="31px"
              height="21px"
            />
          </Center>

          <CustomHeading
            as="h1"
            textStyle="bold20"
            color="secondary.mid-dark"
            mb="24px"
            ml={4}
          >
            Pronto! Sua senha foi redefinida.
          </CustomHeading>
        </Flex>

        <Text as="p" textStyle="regular16" mt="2px" textAlign="center">
          Guarde bem estas informações.
        </Text>
      </StepCard>

      <Box px="6">
        <Button
          onClick={handleLogin}
          isFullWidth
          mt={['24px', '24px', '32px', '32px']}
        >
          Acessar minha conta
        </Button>
      </Box>
    </>
  );
};
