import { FC } from 'react';

import { Box } from '@chakra-ui/react';

import { BemAccordion } from '@pcf/design-system';
import { ContractsSection } from 'features/main/features/dashboard/contracts';
import { ProposalsSection } from 'features/main/features/dashboard/proposals';

export const Dashboard: FC = () => {
  return (
    <Box p="24px" flex={1} overflow="auto">
      <BemAccordion defaultIndex={[0]} colorScheme="secondary">
        <ContractsSection />
        <ProposalsSection />
      </BemAccordion>
    </Box>
  );
};

export default Dashboard;
