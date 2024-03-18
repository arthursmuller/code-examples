import { FC, forwardRef } from 'react';

import {
  Flex,
  Box,
  RadioGroup,
  Radio,
  Button,
  IconButton,
  Text,
  Icon,
} from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import {
  StepCard,
  CustomController,
  FormElRef,
  topToBottom,
} from '@pcf/design-system';
import { useConvenios } from '@pcf/core';
import { ArrowOutlineLeftIcon } from '@pcf/design-system-icons';

import { ConvenioFormData, FormBaseProps } from '../models/captura-lead.model';
import { useCapturaLeadContext } from '../captura-lead.context';

type RadioGroupConvenioProps = {
  onChange?: (value: string) => void;
  defaultValue?: string;
};

export const RadioGroupConvenioFormItem = forwardRef<
  FormElRef,
  RadioGroupConvenioProps
>(({ onChange, defaultValue }, ref) => {
  const { data: convenios } = useConvenios();
  const { onSave } = useCapturaLeadContext();

  function handleChange(radioValue: string): void {
    if (radioValue) {
      onSave({ convenio: radioValue });
    }

    onChange && onChange(radioValue);
  }

  return (
    <RadioGroup
      width="100%"
      onChange={handleChange}
      defaultValue={defaultValue || '-1'}
      name="convenio"
      ref={ref}
      position="relative"
    >
      <Flex direction="column">
        {convenios?.map(({ id, nome }) => (
          <Radio key={`${id}`} value={`${id}`} marginBottom="24px">
            {nome}
          </Radio>
        ))}
      </Flex>
    </RadioGroup>
  );
});

const Convenio: FC<FormBaseProps<ConvenioFormData>> = ({
  onSuccess,
}: FormBaseProps<ConvenioFormData>) => {
  const {
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<ConvenioFormData>();
  const { formData, onPrevious, isFinishingFormFilling } =
    useCapturaLeadContext();

  function onSubmit(data: ConvenioFormData): void {
    onSuccess(data);
  }

  return (
    <Flex as="form" onSubmit={handleSubmit(onSubmit)} flexDir="column">
      <StepCard
        title="Simule sua Proposta"
        subTitle="Qual o seu convênio?"
        textAlign={["center", "center", "start"]}
      >
        <Flex mt="6" width="100%" flexDir="column">
          <CustomController
            defaultValue={`${formData.convenio}`}
            name="convenio"
            control={control}
            rules={{ required: 'Campo obrigatório' }}
            as={RadioGroupConvenioFormItem}
          />

          <Flex alignItems="center" width="100%">
            {!!errors?.convenio?.message && (
              <Text
                fontSize="12px"
                color="#E53E3E"
                animation={`.25s ${topToBottom}`}
              >
                {errors?.convenio?.message}
              </Text>
            )}
          </Flex>
        </Flex>
      </StepCard>

      <Flex justifyContent="space-between" alignItems="center" px={6} mt="6">
        <IconButton
          onClick={onPrevious}
          fontSize="40px"
          aria-label="Voltar"
          icon={<Icon as={ArrowOutlineLeftIcon} w={4} h="22px" mr="3px" />}
          borderRadius="full"
        />

        <Box>
          <Button
            isLoading={isFinishingFormFilling}
            loadingText="Requisitando"
            type="submit"
          >
            Requisitar simulação
          </Button>
        </Box>
      </Flex>
    </Flex>
  );
};

export default Convenio;
