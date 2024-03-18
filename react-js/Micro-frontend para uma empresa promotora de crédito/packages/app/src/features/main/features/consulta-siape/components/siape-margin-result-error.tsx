import { FC, ReactFragment } from 'react';

import { Text, Icon, Divider, Flex, Button, Box } from '@chakra-ui/react';

import { InfoIcon, StatusCheckCircleIcon } from '@pcf/design-system-icons';

interface SiapeMarginResultErrorProps {
  onNext?: () => void;
}

export const ProceedSteps: FC<{ steps: ReactFragment[] }> = ({ steps }) => (
  <Flex direction="column">
    {steps?.map((s, i) => (
      <Text key={i + 1} marginBottom={2} textStyle="regular16_20">
        <Box color="error.regular" as="span" textStyle="bold16_20">
          {i + 1}º Passo:
        </Box>{' '}
        {s}
      </Text>
    ))}
  </Flex>
);

export const SiapeMarginResultError: FC<SiapeMarginResultErrorProps> = ({
  onNext,
}) => (
  <>
    <Icon
      as={StatusCheckCircleIcon}
      marginY={6}
      width={10}
      height={10}
      color="error.dark"
    />

    <Text
      color="error.dark"
      textStyle="bold24"
      marginBottom={6}
      textAlign="center"
    >
      Identificamos problemas para acessar suas informações
    </Text>

    <Text textStyle="regular14" marginBottom={6} textAlign="center">
      Para autorizar a consulta de margem pelo Banrisul, siga o passo a passo
      abaixo
    </Text>

    <Icon
      as={InfoIcon}
      width="20px"
      marginBottom={2}
      color="secondary.mid-dark"
    />

    <Text textStyle="regular14" textAlign="center">
      O banco a ser pesquisado no SIGEPE deve ser o Banco do Estado do Rio
      Grande do Sul, pois o mesmo é o originador do crédito
    </Text>

    <Divider borderColor="grey.400" marginY={6} />

    <ProceedSteps
      steps={[
        <>
          <strong>Acesse o Sigepe aqui</strong> e faça seu login
        </>,
        <>
          Clique na janela de <strong>SIGEPE SERVIDOR OU PENSIONISTA</strong>
        </>,
        <>
          Acesse <strong>Consignações</strong>
        </>,
        <>
          Vá em <strong>Gerar Autorização de Consignatário</strong>
        </>,
        <>
          Selecione{' '}
          <strong>
            Facultativo 30% – Novo Contrato e Renovação (Empréstimo)
          </strong>
        </>,
        <>
          Escolha o <strong>Banco do Estado do Rio Grande do Sul </strong>
        </>,
        <>
          O SIGEPE irá enviar uma confirmação de autorização para o seu e-mail
          cadastrado, <strong>acesse a caixa de entrada</strong> do seu e-mail e{' '}
          <strong>localize o código.</strong> Após isso, basta{' '}
          <strong>inserir este código de validação no sistema</strong> e
          confirmar.
        </>,
      ]}
    />

    <Flex justifyContent="flex-end" width="100%">
      <Button
        marginY={6}
        minWidth={['100%', '100%', '250px']}
        colorScheme="secondary"
        onClick={onNext}
      >
        Tente novamente
      </Button>
    </Flex>
  </>
);
