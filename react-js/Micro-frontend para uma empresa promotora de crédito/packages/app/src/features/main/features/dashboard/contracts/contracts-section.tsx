import { FC } from 'react';

import { BemAccordionItem } from '@pcf/design-system';
import { TabPropostasInativaIcon } from '@pcf/design-system-icons';

import { ContractsTable } from './components/contracts-table';

export const ContractsSection: FC = () => (
  <BemAccordionItem
    title="Meus Contratos"
    icon={TabPropostasInativaIcon}
    accordionPanelProps={{ padding: 0 }}
    body={<ContractsTable />}
  />
);
