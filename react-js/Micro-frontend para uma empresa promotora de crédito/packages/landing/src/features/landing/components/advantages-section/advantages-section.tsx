import { FC } from 'react';

import { CustomHeading } from '@pcf/design-system';
import {
  BadgesSection,
  BadgeProps,
} from 'features/landing/components/badges-section';

import Calculator from './assets/badge-calculator.svg';
import Clock from './assets/badge-clock.svg';
import Percentage from './assets/badge-percentage.svg';
import Calendar from './assets/badge-calendar.svg';
// import badgePercentage from './assets/badge-percentage.json';
// import badgeCalculator from './assets/badge-calculator.json';
// import badgeCalendar from './assets/badge-calendar.json';
// import badgeClock from './assets/badge-clock.json';

const badges: BadgeProps[] = [
  {
    icon: Calendar,
    // animatedIcon: badgeCalendar,
    title: 'Melhores Prazos',
    text: 'Até 72 meses para o pagamento',
    altDescription: 'ilustração calendário',
  },
  {
    icon: Calculator,
    // animatedIcon: badgeCalculator,
    title: 'Taxas Competitivas',
    text: 'Crédito mais barato para você',
    altDescription: 'ilustração calculadora',
  },
  {
    icon: Percentage,
    // animatedIcon: badgePercentage,
    title: 'Comodidade',
    text: 'Desconto em folhas com parcelas fixas',
    altDescription: 'ilustração símbolo de percentual',
  },
  {
    icon: Clock,
    // animatedIcon: badgeClock,
    title: 'Agilidade',
    text: 'Crédito rápido e fácil',
    altDescription: 'ilustração de relógio',
  },
];

export const AdvantagesSection: FC = () => {
  return (
    <>
      <CustomHeading
        color="secondary.mid-dark"
        textStyle="bold32"
        textAlign={['start', 'start', 'center']}
        marginX={6}
        mb={['28px', '28px', '52px']}
      >
        Vantagens dos nossos produtos
      </CustomHeading>
      <BadgesSection badges={badges} />
    </>
  );
};
