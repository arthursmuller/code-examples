import { FC } from 'react';

import { useForm } from 'react-hook-form';
import { Box, Button, Flex, Grid, Icon, Text } from '@chakra-ui/react';
import NumberFormat from 'react-number-format';

import { HelpIcon } from '@pcf/design-system-icons';
import { SelectBancos } from 'features/main/features/perfil/rendimentos/components/selects';
import { useClienteLogado } from '@pcf/core';
import { UnloadPrompt } from 'components';
import {
  BemCurrencyInput,
  BemTextInput,
  FormItemControl,
  FullLayoutCard,
  useStepsContainerContext,
} from '@pcf/design-system';

import { PortabilidadeInfoFormData } from '../../models/portabilidade-form.model';

export const PortabilidadeForm: FC = () => {
  const { nextStep, data } =
    useStepsContainerContext<PortabilidadeInfoFormData>();

  const {
    handleSubmit,
    formState: { errors, isDirty },
    control,
    setValue,
  } = useForm<PortabilidadeInfoFormData>({ defaultValues: data });

  const { data: clienteLogado } = useClienteLogado();

  return (
    <FullLayoutCard>
      <UnloadPrompt shouldBlock={isDirty} />

      <Flex
        as="form"
        direction="column"
        onSubmit={handleSubmit((data) =>
          nextStep({ ...data, cpf: clienteLogado?.cpf }),
        )}
      >
        <Grid
          my={8}
          gridRowGap={4}
          gridColumnGap={6}
          gridTemplateColumns={['1fr', '1fr', 'repeat(2, 1fr)']}
        >
          <FormItemControl
            label="Origem"
            name="origem"
            defaultValue={data?.origem}
            errorMessage={errors?.origem?.message}
            control={control}
            as={SelectBancos}
            required
          />

          <Flex
            alignItems="center"
            height="56px"
            marginX={[4, 4, 0]}
            marginTop={[-3, -3, 0]}
          >
            <Icon as={HelpIcon} marginRight="12px" color="primary.regular" />
            <Button
              variant="link"
              as="a"
              color="primary.regular"
              target="_blank"
              textDecoration="underline"
              disabled
            >
              Não sabe como proceder? Clique aqui!
            </Button>
          </Flex>

          <FormItemControl
            label="Contrato"
            name="contrato"
            defaultValue={data?.contrato}
            errorMessage={errors?.contrato?.message}
            control={control}
            as={NumberFormat}
            format="##########"
            onBlur={({ target: { value } }) =>
              value?.trim() &&
              setValue('contrato', value.trim().padStart(10, '0'))
            }
            required
          />
          <FormItemControl
            label="Saldo Devedor"
            name="saldoDevedor"
            required
            defaultValue={data?.saldoDevedor}
            errorMessage={errors?.saldoDevedor?.message}
            control={control}
            as={BemCurrencyInput}
          />
          <FormItemControl
            label="Quantidade Parcelas"
            name="parcelas"
            required
            defaultValue={data?.parcelas}
            errorMessage={errors?.parcelas?.message}
            control={control}
            as={BemTextInput}
            type="number"
          />
          <Flex direction="column">
            <FormItemControl
              label="Quantidade Parcelas Pagas"
              name="parcelasPagas"
              required
              defaultValue={data?.parcelasPagas}
              errorMessage={errors?.parcelasPagas?.message}
              control={control}
              as={BemTextInput}
              type="number"
            />
            <Flex justifyContent="flex-end">
              <Text textStyle="bold14">
                Taxa:{' '}
                <Box as="span" color="primary.regular">
                  1,25%
                </Box>
              </Text>
            </Flex>
          </Flex>

          <Flex direction="column">
            <FormItemControl
              label="Prestação"
              name="prestacao"
              required
              defaultValue={data?.prestacao}
              errorMessage={errors?.prestacao?.message}
              control={control}
              as={BemCurrencyInput}
              rules={{
                validate: {
                  min: (value: number) =>
                    value >= 122.73 ? true : 'Valor de prestação inválido',
                },
              }}
            />
            <Flex justifyContent="flex-end">
              <Text textStyle="bold14">
                Prestação Mínima Portável:{' '}
                <Box as="span" color="primary.regular">
                  R$ 122,73
                </Box>
              </Text>
            </Flex>
          </Flex>

          <span />
        </Grid>

        <Flex justifyContent="flex-end">
          <Button
            colorScheme="secondary"
            type="submit"
            width={['100%', '100%', '288px']}
            marginBottom={8}
          >
            Simular Portabilidade
          </Button>
        </Flex>
      </Flex>
    </FullLayoutCard>
  );
};
