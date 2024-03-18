import { FC } from 'react';

import { Flex, Text, Button, useBreakpointValue } from '@chakra-ui/react';

import { RadioCardLogicProps, RadioCardVariant } from '@pcf/design-system';
import { RendimentoResponseModel } from '@pcf/core';

export interface MatriculaCardProps extends RadioCardLogicProps {
  matricula: RendimentoResponseModel;
  edit?: (id: number) => void;
  isSimulationFlow: boolean;
}

export const MatriculaCard: FC<MatriculaCardProps> = ({
  matricula,
  edit,
  isSimulationFlow,
  ...props
}) => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const EditLink: FC<{ color?: string }> = ({ color }) => (
    <Button
      variant="link"
      onClick={() => edit && edit(matricula.id)}
      color={color || 'white'}
    >
      Editar matrícula
    </Button>
  );

  return (
    <RadioCardVariant
      {...props}
      header={
        <>
          <Text textStyle="regular16" color="white" flex={1}>
            Matrícula {matricula.matricula} |{' '}
            {matricula.siapeTipoFuncional ? 'SIAPE' : 'INSS'}
          </Text>

          {!isMobile && !isSimulationFlow && <EditLink />}
        </>
      }
      content={
        <>
          <Flex flex={1} flexWrap="wrap">
            <Text
              textStyle="regular16"
              color="secondary.mid-dark"
              marginRight={1}
            >
              Banco:
            </Text>
            <Text textStyle="regular16" marginBottom={2}>
              {matricula.contaCliente.banco.codigo} -{' '}
              {matricula.contaCliente.banco.nome}
            </Text>
          </Flex>

          <Flex flex={1} flexWrap="wrap">
            <Text
              textStyle="regular16"
              color="secondary.mid-dark"
              marginRight={1}
            >
              Tipo de Conta:
            </Text>
            <Text textStyle="regular16">
              {matricula.contaCliente.tipoConta.nome}
            </Text>
          </Flex>
        </>
      }
      footer={
        <>
          <Flex>
            <Flex
              flex={1}
              justifyContent="center"
              padding="8px"
              borderRight="1px solid"
              borderColor="grey.500"
            >
              <Text textStyle="regular16">
                Agência: {matricula.contaCliente.agencia}
              </Text>
            </Flex>
            <Flex flex={1} justifyContent="center" padding="8px">
              <Text textStyle="regular16">
                Conta: {matricula.contaCliente.conta}
              </Text>
            </Flex>
          </Flex>
          {isMobile && !isSimulationFlow && (
            <Flex
              flex={1}
              justifyContent="center"
              padding="8px"
              borderTop="1px solid"
              borderColor="grey.500"
            >
              <EditLink color="secondary.mid-dark" />
            </Flex>
          )}
        </>
      }
      asDisplay={!isSimulationFlow}
    />
  );
};
