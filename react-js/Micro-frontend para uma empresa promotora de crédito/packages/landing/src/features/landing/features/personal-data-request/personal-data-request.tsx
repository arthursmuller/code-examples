import { FC } from 'react';

import { Flex } from '@chakra-ui/react';

import { SubPageHeader } from 'features/landing/components/sub-page-header/sub-page-header';
import { CustomHeading } from '@pcf/design-system';

import { PersonalDataRequestForm } from './components/personal-data-form';
import DadosPessoaisImg from './assets/lgpd@2x.jpg';

export const PersonalDataRequest: FC = () => {
  return (
    <>
      <SubPageHeader
        backgroundImage={DadosPessoaisImg}
        backgroundImageAlt="dados"
        position={['27%', '27%', 'right']}
      >
        <SubPageHeader.Title title="acesso a dados pessoais" />
        <SubPageHeader.Subtitle
          subtitle="Obtenha suas informações pessoais dentro da Bem."
          subtitleOrange=""
          width={['290px', '290px', '375px']}
        />
      </SubPageHeader>

      <Flex
        minH="590px"
        flexDir="column"
        justifyContent="center"
        alignItems="center"
        color="secondary.regular"
        paddingX={4}
      >
        <PersonalDataRequestForm />

        <Flex color="secondary.regular" maxWidth="400px">
          <CustomHeading
            w={['auto', 'auto', '650px']}
            mx={6}
            as="h2"
            textStyle={['bold16', 'bold16', 'bold24']}
            textAlign="center"
            marginY={6}
          >
            Juliano Mirapalheta Sangoi Data Protection Officer - DPO
          </CustomHeading>
        </Flex>
      </Flex>
    </>
  );
};
