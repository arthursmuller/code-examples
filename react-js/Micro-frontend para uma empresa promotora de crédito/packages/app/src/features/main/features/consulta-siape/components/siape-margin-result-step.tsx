import { FC } from 'react';

import { Flex, Text, Button, Icon, Box } from '@chakra-ui/react';

import {
  formatCurrency,
  FullLayoutCard,
  Loader,
  useStepsContainerContext,
  BemErrorBoundary,
} from '@pcf/design-system';
import { StatusCheckCircleIcon, InfoIcon } from '@pcf/design-system-icons';
import { useRendimentosMargemSiapeQuery } from '@pcf/core';

import { SiapeMarginResultError } from './siape-margin-result-error';

import { RequestMarginSiape } from '../model';

const NotAllowed: FC = ({ children }) => {
  const { finish } = useStepsContainerContext<RequestMarginSiape>();

  return (
    <Flex direction="column" alignItems="center">
      <Icon
        as={InfoIcon}
        marginY={6}
        width={10}
        height={10}
        color="warning.dark"
      />

      <Text
        color="warning.dark"
        textStyle="bold24"
        marginBottom={6}
        textAlign="center"
      >
        Caso você deseje liberar seus dados mais tarde, essa opção estará
        disponível nos seus rendimentos
      </Text>

      {children}

      <Flex justifyContent="flex-end" width="100%">
        <Button
          marginY={6}
          minWidth={['100%', '100%', '250px']}
          colorScheme="secondary"
          onClick={finish}
        >
          Continuar
        </Button>
      </Flex>
    </Flex>
  );
};

const Allowed: FC = () => {
  const { data, isLoading, isFetching, refetch } =
    useRendimentosMargemSiapeQuery(undefined, { useErrorBoundary: false });
  const { nextStep } = useStepsContainerContext<RequestMarginSiape>();

  const noData = data?.pendenteInformacoesBanco || !data?.items.length;

  return (
    <Flex direction="column" height="100%">
      {isLoading || isFetching ? (
        <Loader fullWidth />
      ) : (
        <Flex direction="column" alignItems="center">
          {noData ? (
            <SiapeMarginResultError onNext={refetch} />
          ) : (
            <>
              <Icon
                as={StatusCheckCircleIcon}
                marginY={6}
                width={12}
                height={12}
                color="warning.dark"
              />

              <Text color="error.dark" textStyle="bold24" marginBottom={6}>
                Seus dados foram acessados com sucesso!
              </Text>

              {data?.items.map(
                ({
                  matricula,
                  valorMaximoParcela,
                  pendenteInformacoesBanco,
                }) => (
                  <Flex
                    layerStyle="card"
                    direction="column"
                    padding={0}
                    key={matricula}
                    width={['100%', '100%', '700px']}
                  >
                    <Flex
                      backgroundColor="secondary.mid-dark"
                      borderTopRadius="8px"
                      color="grey.100"
                      padding={4}
                    >
                      Matrícula: {matricula}
                    </Flex>
                    <Flex padding={4}>
                      {pendenteInformacoesBanco ? (
                        <Text textStyle="bold16" color="error.dark">
                          Pendente liberação com o banco.
                        </Text>
                      ) : (
                        <Text>
                          <Box as="span" textStyle="bold16">
                            Valor:{' '}
                          </Box>
                          {formatCurrency(valorMaximoParcela)}
                        </Text>
                      )}
                    </Flex>
                  </Flex>
                ),
              )}

              <Flex justifyContent="flex-end" width="100%">
                <Button
                  marginY={6}
                  minWidth={['100%', '100%', '250px']}
                  colorScheme="secondary"
                  onClick={() => nextStep()}
                >
                  Tente novamente
                </Button>
              </Flex>
            </>
          )}
        </Flex>
      )}
    </Flex>
  );
};

export const SiapeMarginResultStep: FC = ({ children }) => {
  const { data } = useStepsContainerContext<RequestMarginSiape>();

  return data.allowSiapeConsult ? (
    <Allowed />
  ) : (
    <NotAllowed>{children}</NotAllowed>
  );
};

export const SiapeMarginResultStepCard: FC = ({ children }) => (
  <FullLayoutCard>
    <BemErrorBoundary>
      <SiapeMarginResultStep>{children}</SiapeMarginResultStep>
    </BemErrorBoundary>
  </FullLayoutCard>
);
