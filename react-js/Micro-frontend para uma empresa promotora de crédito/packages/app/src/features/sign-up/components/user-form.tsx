import { useEffect, FC } from 'react';

import { Box, Flex, SimpleGrid } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';
import { isValidPhone } from '@brazilian-utils/brazilian-utils';
import NumberFormat from 'react-number-format';
import { useMount, useUpdateEffect } from 'react-use';

import {
  BemTextInput,
  StepCard,
  FormItemControl,
  CpfInput,
} from '@pcf/design-system';

import { UserInfo } from '../models/user-info.model';
import { useSignUpContext } from '../sign-up.context';
import { useSocialMediaLogo } from '../useSocialMediaLogo';

const UserForm: FC = () => {
  const {
    handleSubmit,
    control,
    formState: { errors },
    formState,
    setValue,
  } = useForm<UserInfo>({
    mode: 'onChange',
  });
  const { formData, setSubmit, setCanGoForward } = useSignUpContext();

  const { isValid } = formState;

  useMount(() => {
    setSubmit(handleSubmit);
  });

  useUpdateEffect(() => {
    setValue('name', formData.name, { shouldValidate: true });
    setValue('surname', formData.surname, { shouldValidate: true });
  }, [formData]);

  const logo = useSocialMediaLogo();

  useEffect(() => {
    setCanGoForward(isValid);
  }, [isValid, setCanGoForward]);

  return (
    <StepCard
      title={
        logo
          ? 'Insira os dados abaixo para finalizar o acesso'
          : 'Crie sua conta'
      }
      customTop={logo || null}
      information="É Bem rápido. É Bem fácil!"
    >
      <Box mt="25px">
        <Flex as="form" flexDirection="column">
          <SimpleGrid columns={1} spacingY={4}>
            <FormItemControl
              label="Nome"
              name="name"
              defaultValue={formData.name}
              errorMessage={errors?.name?.message}
              required
              control={control}
              as={BemTextInput}
            />

            <FormItemControl
              label="Sobrenome"
              name="surname"
              defaultValue={formData.surname}
              errorMessage={errors?.surname?.message}
              required
              control={control}
              as={BemTextInput}
            />

            <CpfInput
              control={control}
              defaultValue={formData.cpf}
              errors={errors}
            />

            <FormItemControl
              label="Telefone (com DDD)"
              name="phone"
              defaultValue={formData.phone}
              errorMessage={errors?.phone?.message}
              required
              rules={{
                validate: {
                  phoneValidator(telefone) {
                    if (!isValidPhone(telefone)) {
                      return 'Telefone inválido';
                    }
                    return true;
                  },
                },
              }}
              control={control}
              as={NumberFormat}
              format="(##) #####-####"
              mask="_"
            />
          </SimpleGrid>
        </Flex>
      </Box>
    </StepCard>
  );
};

export default UserForm;
