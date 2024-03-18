import { FC } from 'react';

import { useForm } from 'react-hook-form';
import {
  Flex,
  Box,
  Button,
  IconButton,
  Grid,
  GridItem,
  Icon,
} from '@chakra-ui/react';

import { CpfInput, PhoneInput, StepCard, EmailInput } from '@pcf/design-system';
import { LeadCriacaoModel } from '@pcf/core';
import { ArrowOutlineLeftIcon } from '@pcf/design-system-icons';
import { useAppContext } from 'app/app.context';

import {
  DadosClienteFormData,
  FormBaseProps,
} from '../models/captura-lead.model';
import { useCapturaLeadContext } from '../captura-lead.context';

const DadosCliente: FC<FormBaseProps<DadosClienteFormData>> = ({
  showBack = true,
  onSuccess,
  simpleStepsInnerButton = false,
}: FormBaseProps<DadosClienteFormData>) => {

  const { onPrevious, formData, onSave, isFinishingFormFilling } =
    useCapturaLeadContext();
  const { currentCpf, setCurrentCpf } = useAppContext();

  const {
    handleSubmit,
    formState: { errors },
    control,
    getValues,
  } = useForm<DadosClienteFormData>();

  function onSubmit(data: DadosClienteFormData): void {
    onSuccess(data);
  }

  function handleOnBlur(field: string, nextValue?: string): void {
    const value: Partial<LeadCriacaoModel> =
      nextValue || getValues(field as any);

    if (!errors[field] && value !== formData[field]) {
      onSave({ [field]: value });
    }
  }

  return (
    <Flex
      as="form"
      onSubmit={handleSubmit(onSubmit)}
      flexDir="column"
      position="relative"
      noValidate
    >
      <StepCard
        title="Simule sua Proposta"
        subTitle="Precisamos de alguns dados"
        textAlign={['center', 'center', 'start']}
      >
        <Grid
          gridTemplateColumns={['1fr', '1fr', '1fr 1fr', '1fr 1fr']}
          gap="2"
          mt="6"
        >
          <CpfInput
            control={control}
            errors={errors}
            defaultValue={formData.cpf || currentCpf}
            onBlur={(nextValue: string) => {
              handleOnBlur('cpf', nextValue);
              setCurrentCpf(currentCpf);
            }}
          />

          <PhoneInput
            label="Telefone"
            name="telefone"
            defaultValue={formData.telefone || ''}
            errorMessage={errors?.telefone?.message}
            control={control}
            onBlur={() => handleOnBlur('telefone')}
          />

          <GridItem gridColumn={['', '', 'span 2']}>
            <EmailInput
              label="E-mail (opcional)"
              defaultValue={formData.email}
              errorMessage={errors?.email?.message}
              control={control}
              onBlur={() => handleOnBlur('email')}
            />
          </GridItem>
        </Grid>

        {simpleStepsInnerButton && !formData.requerConvenio && (
          <Flex justifyContent="flex-end">
            <Button
              type="submit"
              w={['100%', '100%', '100%', '130px']}
              isLoading={isFinishingFormFilling}
            >
              Simular
            </Button>
          </Flex>
        )}
      </StepCard>

      <Flex mt="24px" justifyContent="space-between" px={6} alignItems="center">
        {showBack ? (
          <IconButton
            onClick={onPrevious}
            fontSize="40px  "
            aria-label="Voltar"
            icon={<Icon as={ArrowOutlineLeftIcon} w={4} h="22px" mr="3px" />}
            borderRadius="full"
          />
        ) : (
          <span />
        )}

        {!simpleStepsInnerButton && (
          <>
            {!formData.requerConvenio ? (
              <Button
                loadingText="Requisitando"
                type="submit"
                isLoading={isFinishingFormFilling}
              >
                Requisitar simulação
              </Button>
            ) : (
              <Box>
                <Button type="submit">Continuar</Button>
              </Box>
            )}
          </>
        )}
      </Flex>
    </Flex>
  );
};

export default DadosCliente;
