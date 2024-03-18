import { FC, useState } from 'react';

import { Flex, Stack, Text } from '@chakra-ui/react';

import { SolicitacaoAutorizacaoConsultaBeneficioModel } from '@pcf/core';
import { FullLayoutCard } from '@pcf/design-system';

import { DadosClienteForm } from './dados-cliente-form';
import { ConfirmacaoTokenForm } from './confirmacao-token-form';

export const CadastroRendimentoInss: FC = () => {
  const [idConsultaBeneficio, setIdConsultaBeneficio] = useState<number>();

  function handleOnSuccess(
    data: SolicitacaoAutorizacaoConsultaBeneficioModel,
  ): void {
    setIdConsultaBeneficio(data.idConsultaBeneficio);
  }

  return (
    <Flex flexDir="column" alignItems="center" mx={[0, 14]}>
      <Text textStyle="regular16" mb="34px" mt="8px" textAlign="center">
        Faça o cadastro do seu celular e valide o token que será enviado para
        ter acesso às vantagens abaixo
      </Text>

      <Stack direction={['column', 'column', 'row']} spacing={6} pb={8}>
        <DadosClienteForm
          active={!idConsultaBeneficio}
          onSuccess={handleOnSuccess}
        />

        <ConfirmacaoTokenForm
          idConsultaBeneficio={idConsultaBeneficio}
          active={!!idConsultaBeneficio}
        />
      </Stack>
    </Flex>
  );
};

export const CadadastroRendimentoCard: FC = () => {
  return (
    <FullLayoutCard>
      <CadastroRendimentoInss />
    </FullLayoutCard>
  );
};
