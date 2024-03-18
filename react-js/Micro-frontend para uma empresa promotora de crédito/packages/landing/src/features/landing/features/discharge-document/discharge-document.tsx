import { FC } from 'react';

import { Flex } from '@chakra-ui/react';

import { SubPageHeader } from 'features/landing/components/sub-page-header/sub-page-header';
import { CustomHeading } from '@pcf/design-system';

import { DischargeDocumentForm } from './components/discharge-document-form';
import DischargeDocumentImg from './assets/documento-quitacao@2x.jpg';

export const DischargeDocument: FC = () => {
  return (
    <>
      <SubPageHeader
        backgroundImage={DischargeDocumentImg}
        backgroundImageAlt="Uma mulher com blusa de tricô de manga comprida segura um documento em suas mãos"
      >
        <SubPageHeader.Title title="Documento de Quitação" />
        <SubPageHeader.Subtitle
          subtitle="Obtenha suas informações e solicite os dados para a antecipação de contratos."
          subtitleOrange=""
        />
      </SubPageHeader>

      <Flex
        minH="590px"
        flexDir="column"
        justifyContent="center"
        alignItems="center"
        pb="66px"
        color="secondary.regular"
      >
        <CustomHeading
          w={['auto', 'auto', '650px']}
          mx={6}
          as="h2"
          color="secondary.mid-dark"
          textStyle={['regular24', 'regular24', 'bold32']}
          textAlign="center"
          my={9}
        >
          Para fazer download do arquivo, <b>preencha o formulário abaixo</b>
        </CustomHeading>
        <DischargeDocumentForm />
      </Flex>
    </>
  );
};
