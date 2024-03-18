import { Text, Box, Button } from '@chakra-ui/react';
import { Link } from 'react-router-dom';

import { StepCard, CustomHeading } from '@pcf/design-system';
import { PublicRoutes as PublicRoutesEnum } from 'app/routes/public/public-routes.enum';

export const SuccessStep: React.FC = () => {
  return (
    <>
      <StepCard>
        <CustomHeading
          as="h1"
          textStyle="bold32"
          color="secondary.mid-dark"
          textAlign="center"
          mb="24px"
        >
          Sucesso!
        </CustomHeading>

        <Text
          as="p"
          textStyle="regular16"
          mt="2px"
          textAlign="center"
          mb="20px"
        >
          Você receberá um e-mail com um link para redefinir sua senha. Caso não
          receba esse e-mail, cheque sua caixa de spam
        </Text>
      </StepCard>

      <Box px="6">
        <Link to={PublicRoutesEnum.Root}>
          <Button isFullWidth mt={['24px', '24px', '32px', '32px']}>
            Voltar para a home
          </Button>
        </Link>
      </Box>
    </>
  );
};
