import { FC } from 'react';

import { Flex } from '@chakra-ui/react';
import dynamic from 'next/dynamic';

import { SubPageHeader } from 'features/landing/components/sub-page-header/sub-page-header';
import { AdvantagesSection } from 'features/landing/components/advantages-section/advantages-section';

import Funcionarios from './assets/encontre-uma-loja@2x.jpg';

import { LandingTemplate } from '../../landing-template';

const LazyMap = dynamic(() => import('../../components/map'));

export const Stores: FC = () => {
  return (
    <>
      <SubPageHeader
        backgroundImage={Funcionarios}
        backgroundImageAlt="Consultor de terno, gravata e óculos de grau, mostra uma informação no celular para uma mulher loira de cabelos ondulados"
        position={['70%', '70%', '44%']}
      >
        <SubPageHeader.Title title="Encontre uma loja Bem" />
        <SubPageHeader.Subtitle
          subtitle="mais perto de você"
          subtitleOrange="Visite nossa loja"
        />
      </SubPageHeader>

      <LazyMap />

      <Flex marginTop={10} direction="column">
        <AdvantagesSection />
      </Flex>
    </>
  );
};

export const StoresProvided: FC = () => (
  <LandingTemplate>
    <Stores />
  </LandingTemplate>
);
