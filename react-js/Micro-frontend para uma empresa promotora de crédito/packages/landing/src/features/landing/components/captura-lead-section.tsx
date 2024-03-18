import { FC } from 'react';

import { Flex, Center } from '@chakra-ui/react';
import { useRouter } from 'next/router';

import { ProdutoModel } from '@pcf/core';

import Mockup from './mockup';

import { CapturaLead } from '../../captura-lead';

interface CapturaLeadSectionProps {
  simpleSteps?: boolean;
  product?: ProdutoModel;
}

export const CapturaLeadSection: FC<CapturaLeadSectionProps> = ({
  simpleSteps,
  product,
}) => {
  const { query } = useRouter();

  const isSimpleSteps =
    simpleSteps || Object.keys(query).some((q) => q === 'proposta-agil');

  return (
    <Center
      w="100%"
      id="simuladorContainer"
      minH={['auto', 'auto', 'auto', 'auto', '480px']}
    >
      <Flex w={['100%', '772px']}>
        <CapturaLead simpleSteps={isSimpleSteps} product={product} />
      </Flex>

      <Mockup />
    </Center>
  );
};
