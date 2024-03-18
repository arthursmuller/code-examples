import { FC } from 'react';

import { Center, Flex, Icon, Text, Divider } from '@chakra-ui/react';
import { formatCEP } from '@brazilian-utils/brazilian-utils';

import {
  CustomHeading,
  DefaultFormatStrings,
  ExpandableSection,
  formatCurrency,
  formatDate,
  getFormattedPhone,
  StepsProgressBar,
  StepStatus,
} from '@pcf/design-system';
import { IntencaoOperacaoModel } from '@pcf/core';
import { CheckIcon, StatusCloseErrorIcon } from '@pcf/design-system-icons';

const Line: FC<{ label: string; info: string }> = ({ label, info }) => (
  <Flex flex={1} marginBottom={3} alignItems="center">
    <Text flex={1} textStyle="regular16">
      {label}:
    </Text>
    <Text textStyle="bold16">{info}</Text>
  </Flex>
);

export const ResumoSolicitacaoInfo: FC<{
  intencaoOperacao: IntencaoOperacaoModel;
}> = ({ intencaoOperacao }) => {
  const { passosProduto, rendimento, loja } = intencaoOperacao;

  const lastStep = passosProduto[passosProduto.length - 1];
  const { excepcional: issued, completo: complete } = lastStep;

  const steps = passosProduto.map((step) => ({
    label: step.titulo || '',
    status: step.completo ? StepStatus.active : StepStatus.inactive,
    info: step.descricao,
  }));

  let stepColor = 'secondary.mid-dark';
  let icon: FC | null = null;

  const indexOfActiveStep =
    steps.findIndex((s) => s.status === StepStatus.inactive) - 1;

  const lastActiveStep = steps[indexOfActiveStep] || steps[steps.length - 1];

  if (issued) {
    steps[steps.length - 1].status = StepStatus.error;
    stepColor = 'error.dark';
    icon = StatusCloseErrorIcon;
  } else if (complete) {
    steps[steps.length - 1].status = StepStatus.success;
    stepColor = 'success.regular';
    icon = CheckIcon;
  }

  return (
    <Flex marginTop={6} direction="column">
      <Flex marginBottom={7}>
        <StepsProgressBar isHorizontal items={steps} />
      </Flex>

      <Flex>
        <Flex
          align="center"
          marginBottom={5}
          flex={1}
          justifyContent={['center', 'center', 'left']}
        >
          {!!icon && (
            <Center
              borderRadius="100%"
              padding={1}
              border="2px solid"
              borderColor={stepColor}
              marginRight={2}
            >
              <Icon as={icon} w={2} h={2} color={stepColor} />
            </Center>
          )}

          <CustomHeading as="h3" color={stepColor} textStyle="bold16_20">
            Sua Solicitação {issued ? 'foi' : 'está'} {complete ? '' : 'em'}{' '}
            {complete ? lastStep.titulo : lastActiveStep.label}
          </CustomHeading>
        </Flex>

        {/* TODO: ver histórico */}
      </Flex>

      <Text textStyle="regular16" marginBottom={12}>
        {lastActiveStep.info}
      </Text>

      <ExpandableSection
        fullWidthTitle
        renderExpanded
        title={
          <CustomHeading
            as="h3"
            textStyle="bold20_24"
            color="secondary.regular"
          >
            Dados da Solicitação
          </CustomHeading>
        }
      >
        <Flex direction="column" flex={1}>
          <Line label="Número da Proposta" info={`#${intencaoOperacao.id}`} />
          <Line label="Nome" info={intencaoOperacao.usuario.nome} />
          <Line label="CPF" info={intencaoOperacao.usuario.cpf} />
          <Line label="Conveniada" info={rendimento?.convenio.nome} />
          <Line label="Matrícula" info={rendimento?.matricula} />
          <Line
            label="Tipo de Proposta"
            info={intencaoOperacao?.produto?.nome}
          />
          <Line
            label="Data de Inclusão"
            info={formatDate(
              new Date(intencaoOperacao.dataInclusao),
              DefaultFormatStrings.input,
            )}
          />
          <Line
            label="Valor liberado"
            info={formatCurrency(intencaoOperacao.valorFinanciado)}
          />
          <Line
            label="Prestação"
            info={formatCurrency(intencaoOperacao.prestacao)}
          />
          <Line label="Prazo" info={`${intencaoOperacao.prazo} meses`} />
          <Line
            label="Primeiro vencimento"
            info={
              !!intencaoOperacao.primeiroVencimento
                ? formatDate(
                    new Date(intencaoOperacao.primeiroVencimento),
                    DefaultFormatStrings.input,
                  )
                : ''
            }
          />
          <Line label="Taxa de Juros" info={`${intencaoOperacao.taxaMes}%`} />
        </Flex>
      </ExpandableSection>

      <Divider borderColor="secondary.washed" />

      {!!loja && (
        <>
          <ExpandableSection
            fullWidthTitle
            renderExpanded
            title={
              <CustomHeading
                as="h3"
                textStyle="bold20_24"
                color="secondary.mid-dark"
              >
                Informações da Loja
              </CustomHeading>
            }
          >
            <Flex direction="column" flex={1}>
              <Text textStyle="bold16_20" marginBottom={4}>
                {loja.nome}
              </Text>
              <Text textStyle="regular16" marginBottom={2}>
                Endereço:{' '}
                {loja.tipoLogradouro?.descricao
                  ? `${loja.tipoLogradouro?.descricao} `
                  : ''}
                {loja.logradouro ?? ''}
                {loja.numero ? ` ,${loja.numero}` : ''}
                {loja.complemento ? ` ,${loja.complemento}` : ''} -{' '}
                {loja.bairro}, {loja.municipio?.descricao ?? ''} -{' '}
                {loja.municipio?.uf?.sigla ?? ''}, CEP: {formatCEP(loja.cep)}
              </Text>

              <Text textStyle="regular16" marginBottom={2}>
                Telefone:{' '}
                {loja.telefones
                  ? getFormattedPhone(loja.telefones[0].telefone)
                  : 'nenhum informado'}
              </Text>
            </Flex>
          </ExpandableSection>

          <Divider borderColor="secondary.washed" />
        </>
      )}

      {/* TODO: FALAR CHAT */}
    </Flex>
  );
};
