import { FC } from 'react';

import { useForm } from 'react-hook-form';
import { Box, Button, Flex } from '@chakra-ui/react';

import { BemSelect, StepCard, FormItemControl } from '@pcf/design-system';
import { useProdutos } from '@pcf/core';

import { useCapturaLeadContext } from '../captura-lead.context';
import { FormBaseProps, ProdutoFormData } from '../models/captura-lead.model';

const Produto: FC<FormBaseProps<ProdutoFormData>> = ({
  onSuccess,
}: FormBaseProps<ProdutoFormData>) => {
  const {
    handleSubmit,
    formState: { errors },
    control,
  } = useForm<ProdutoFormData>();
  const { formData, isCreatingLead, onSave } = useCapturaLeadContext();
  const { data: produtos, isLoading } = useProdutos();

  async function onSubmit(data: ProdutoFormData): Promise<void> {
    onSuccess(data);
  }

  async function handleChange(option: string): Promise<void> {
    if (option && formData.produto !== option) {
      await onSave({
        produto: option,
        requerConvenio: !!produtos?.find((p) => p.id === +option)
          ?.requerConvenio,
      });
    }
  }

  const productOptions = produtos?.map((p) => ({
    value: p.id.toString(),
    name: p.nome,
  }));

  return (
    <Flex as="form" onSubmit={handleSubmit(onSubmit)} flexDirection="column">
      <StepCard
        title="Simule sua Proposta"
        subTitle="Qual produto você deseja?"
        textAlign={["center", "center", "start"]}
      >
        <Box width={['100%', '100%', '275px']} mt="6">
          <FormItemControl
            name="produto"
            label="Tipo de produto"
            defaultValue={formData.produto}
            errorMessage={errors?.produto?.message}
            required
            control={control}
            as={BemSelect}
            options={productOptions || []}
            isLoading={isLoading}
            onBlur={(option) => {
              handleChange(option);
            }}
          />
        </Box>
      </StepCard>

      <Flex px={6} justifyContent={['center', 'center', 'flex-end']}>
        <Button
          mt="24px"
          width={['100%', '100%', 'auto']}
          isLoading={isCreatingLead}
          type="submit"
        >
          Continuar
        </Button>
      </Flex>
    </Flex>
  );
};

export default Produto;
