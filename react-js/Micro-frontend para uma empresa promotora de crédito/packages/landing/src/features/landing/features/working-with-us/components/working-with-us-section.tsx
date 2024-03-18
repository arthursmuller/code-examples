import { FC } from 'react';

import {
  Button,
  Flex,
  useBreakpointValue,
  Text,
  Link,
  Box,
} from '@chakra-ui/react';
import NextLink from 'next/link';

import { CustomHeading } from '@pcf/design-system';
import { BemImage } from 'features/components/images';

import WorkingWithUsIcon from '../assets/working-with-us-icon.svg';

export const WorkingWithUsSection: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <Flex
      as="section"
      justifyContent="center"
      flexDir={['column', 'column', 'row']}
      alignItems={['center', 'center', 'flex-start']}
    >
      {isMobile && (
        <CustomHeading
          mb="38px"
          textStyle="bold40"
          color="primary.regular"
          textAlign="center"
        >
          Venha trabalhar com a gente!
        </CustomHeading>
      )}

      <Box mr={[0, 0, '90px']}>
        <BemImage
          src={WorkingWithUsIcon}
          width={['233px', '233px', '382px']}
          height={['257px', '257px', '423px']}
          alt="ilustração trabalhe conosco"
        />
      </Box>

      <Flex flexDir="column" alignItems="center">
        {!isMobile && (
          <CustomHeading
            mt="58px"
            textStyle="bold24_40"
            color="primary.regular"
            textAlign="center"
          >
            Venha trabalhar com a gente!
          </CustomHeading>
        )}

        <Flex
          flexDir="column"
          alignItems="center"
          layerStyle="card"
          maxW={['278px', '278px', '470px']}
          mt="34px"
        >
          <CustomHeading
            textAlign="center"
            textStyle="bold24"
            color="secondary.mid-light"
          >
            Confira nossas vagas disponíveis
          </CustomHeading>

          <NextLink passHref href="https://bempromotora.pandape.com.br/">
            <Button as={Link} isExternal mt={5} variant="outline" size="sm">
              Ver vagas
            </Button>
          </NextLink>
        </Flex>

        <Text textStyle="bold20" my={4}>
          Ou
        </Text>

        <Flex
          flexDir="column"
          alignItems="center"
          layerStyle="card"
          maxW={['278px', '278px', '470px']}
        >
          <CustomHeading
            textAlign="center"
            textStyle="bold24"
            color="secondary.mid-light"
          >
            Cadastre seu currículo no Pandapé
          </CustomHeading>

          <NextLink passHref href="https://bempromotora.pandape.com.br/">
            <Button as={Link} isExternal mt={5} variant="outline" size="sm">
              Cadastrar currículo
            </Button>
          </NextLink>
        </Flex>
      </Flex>
    </Flex>
  );
};
