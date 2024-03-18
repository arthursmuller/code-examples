import { FC } from 'react';

import { BemAccordionItem } from '@pcf/design-system';
import { ClockIcon } from '@pcf/design-system-icons';

import { ProposalsTable } from './components';

export const ProposalsSection: FC = () => {
  return (
    <BemAccordionItem
      title="Minhas Propostas"
      icon={ClockIcon}
      accordionPanelProps={{ padding: 0 }}
      body={<ProposalsTable />}
    />
  );
};
