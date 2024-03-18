import { FC } from 'react';

import {
  Flex,
  Box,
  Text,
  List,
  ListItem,
  Center,
  Icon,
  useMediaQuery,
} from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';
import {
  LogoBemHorizontalWhiteIcon,
  CheckIcon,
} from '@pcf/design-system-icons';
import { CapturaLead } from 'features/captura-lead';
import { Footer } from 'features/components';
import { AdvantagesSection } from 'features/landing/components/advantages-section/advantages-section';
import { BemImage } from 'features/components/images';

import { OportunidadesSection } from './oportunidades-section';
import { ArteDesktop, ArteDesktop2, Money } from './assets/index';

export const Campanha: FC = () => {
  const [isSmallerThan1180] = useMediaQuery('(max-width: 1180px)');

  return (
    <Box w="100%">
      <Flex
        as="header"
        minH="797px"
        maxH="1206px"
        bg="primary.gradient"
        flexDir={['column', 'column', 'column', 'row']}
        px={['31px', '31px', '31px', 0]}
        borderBottomRightRadius="20px"
      >
        <Flex
          ml={[0, 0, 0, '160px']}
          mt={['60px', '60px', '60px', '49px']}
          w={['100%', '100%', '100%', '60%']}
          flexDir="column"
          alignItems={['center', 'center', 'center', 'flex-start']}
        >
          <LogoBemHorizontalWhiteIcon width="auto" height="30px" />

          <CustomHeading
            textAlign={['center', 'center', 'center', 'start']}
            mt={[6, 6, 6, '54px']}
            as="h1"
            maxW="848px"
            color="white !important"
            textStyle={['bold32', 'bold32', 'bold32', 'bold48']}
          >
            {isSmallerThan1180
              ? 'Somos especialistas em crédito consignado para INSS e SIAPE. Simule sua proposta hoje!'
              : 'Somos especialistas em crédito consignado para INSS e SIAPE.'}
          </CustomHeading>

          {isSmallerThan1180 && (
            <Box mt="26px">
              <BemImage
                width="158px"
                height="140px"
                src={Money}
                alt="duas moedas de ouro com um cifrão desenhado dentro"
              />
            </Box>
          )}

          <Text
            mt={[9, 9, 9, 8]}
            color="white !important"
            textAlign={['center', 'center', 'center', 'start']}
            textStyle={['regular24', 'regular24', 'regular24', 'regular32']}
          >
            {isSmallerThan1180 ? (
              <>
                É Seguro! <br /> É Tranquilo! <br /> <b>É Bem Promotora!</b>
              </>
            ) : (
              <>
                Simule sua proposta hoje! <br />É Seguro! É Tranquilo!{' '}
                <b>É Bem Promotora!</b>
              </>
            )}
          </Text>

          <Box
            w="100%"
            sx={{
              '.step-container': {
                alignItems: 'flex-start',
                justifyContent: 'flex-start',
                '& form > div:first-of-type': {
                  margin: 0,
                },
              },
            }}
          >
            <CapturaLead simpleStepsInnerButton simpleSteps />
          </Box>
        </Flex>

        {!isSmallerThan1180 && (
          <Box mt="147px">
            <BemImage
              alt="ilustração uma pessoa em pé apontando o dinheiro"
              width="465px"
              height="670px"
              src={ArteDesktop}
            />
          </Box>
        )}
      </Flex>

      <Flex
        as="main"
        alignItems="center"
        minH="825px"
        flexDir="column"
        px={6}
        pb={[8, 8, 8, 9]}
      >
        <OportunidadesSection />
      </Flex>

      <Flex
        minH="520px"
        bg="secondary.regular"
        justifyContent="space-around"
        p="32px 65px 45px 42px"
        alignItems="center"
        flexDir={['column', 'column', 'column', 'row']}
      >
        <BemImage
          alt="duas pessoas analisando um gráfico"
          width={['233px', '233px', '233px', '462px']}
          height={['202px', '202px', '202px', '396px']}
          src={ArteDesktop2}
        />

        <Box>
          <Text textStyle="bold24_32" color="white">
            Empréstimos para:{' '}
          </Text>

          <List
            spacing={['27px', '27px', '27px', '37px']}
            mt={['30px', '30px', '30px', '50px']}
          >
            <ListItem
              textStyle="regular20"
              color="white"
              display="flex"
              alignItems="center"
            >
              <Center
                flexShrink={0}
                w="28px"
                h="28px"
                borderRadius="full"
                bg="white"
                mr="28px"
              >
                <Icon color="secondary.regular" as={CheckIcon} />
              </Center>
              Aposentados e<br />
              Pensionistas do INSS
            </ListItem>

            <ListItem
              textStyle="regular20"
              color="white"
              display="flex"
              alignItems="center"
            >
              <Center
                w="28px"
                flexShrink={0}
                h="28px"
                borderRadius="full"
                bg="white"
                mr="28px"
              >
                <Icon color="secondary.regular" as={CheckIcon} />
              </Center>
              Funcionários
              <br />
              Públicos Federais
            </ListItem>
          </List>
        </Box>
      </Flex>

      <Box mt={['44px', '44px', '44px', '89px']}>
        <AdvantagesSection />
      </Box>

      <Footer />
    </Box>
  );
};
