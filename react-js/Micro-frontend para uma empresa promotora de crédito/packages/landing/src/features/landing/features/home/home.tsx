import { FC, useState } from 'react';
import dynamic from 'next/dynamic';

import { CapturaLeadSection } from 'features/landing/components/captura-lead-section';

import {
  CreditoConsignado,
  BemPromotora,
  Header,
  // AcesseSuaConta,
  FixedBagdeSimulacao,
  WhatsAppBadge,
  Produtos,
} from './components';

import { useFeatureFlag } from '../../../../app/feature-toggle';
import { LandingTemplate } from '../../landing-template';
import { useMount } from 'react-use';

const MapLazy = dynamic(() => import('../../components/map'));

// TODO: tentative code
const useDidMount = () => {
  const [mounted, setMount] = useState(false);
  useMount(() => {
    setMount(true)
  })

  return mounted
}

export const Home: FC = () => {
  const { WHATSAPP } = useFeatureFlag();
  const mounted = useDidMount();

  return (
    <>
      <Header />

      <FixedBagdeSimulacao />

      {WHATSAPP && <WhatsAppBadge />}

      <CreditoConsignado />

      <CapturaLeadSection />

      <BemPromotora />

      {mounted && <MapLazy />}

      <Produtos />

      {/* <AcesseSuaConta /> */}
    </>
  );
};

export const HomeProvided: FC = () => (
  <LandingTemplate>
    <Home />
  </LandingTemplate>
);
