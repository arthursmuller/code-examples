import { useEffect, FC } from 'react';

import { Flex, VStack, Text, Grid } from '@chakra-ui/react';
import { useMount } from 'react-use';
import { useForm } from 'react-hook-form';

import {
  BemTextInput,
  ValidationText,
  FormItemControl,
  EmailInput,
} from '@pcf/design-system';

import { UserData, UserSecurityInfo } from '../models/user-info.model';

const GENERIC_ERROR_PASSWORD_MESSAGE =
  'A senha deve atender aos requisitos acima';

export interface SecurityFormProps {
  setSubmit?(formResponse): void;
  formData?: UserData;
  triggerWhenValidateChanges?(isValid: boolean): void;
  showEmailField?: boolean;
  requirePassword?: boolean;
  isNestedForm?: boolean;
  socialEmail?: string;
}

const SecurityForm: FC<SecurityFormProps> = ({
  setSubmit,
  triggerWhenValidateChanges,
  formData,
  showEmailField = true,
  requirePassword: requireCurrentPassword,
  isNestedForm,
  socialEmail,
}) => {
  const { control, handleSubmit, watch, trigger, formState, getValues } =
    useForm<UserSecurityInfo>({ mode: 'onChange', criteriaMode: 'all' });
  const { isValid, dirtyFields, errors } = formState;

  const curRepeatPassword = getValues('repeatPassword');

  useEffect(() => {
    triggerWhenValidateChanges && triggerWhenValidateChanges(isValid);
  }, [isValid, triggerWhenValidateChanges]);

  useMount(() => {
    setSubmit && setSubmit(handleSubmit);

    if (formData?.password) {
      trigger();
    }
  });

  const pwValue = watch('password');

  return (
    <Flex as={!isNestedForm ? 'form' : 'div'} flexDirection="column">
      <Grid gridTemplateColumns="1fr" gridRowGap={5} gridColumnGap={6}>
        {requireCurrentPassword && (
          <FormItemControl
            label="Insira sua senha atual"
            name="currentPassword"
            defaultValue={formData?.currentPassword}
            errorMessage={errors?.currentPassword?.message}
            required
            control={control}
            as={BemTextInput}
            type="password"
          />
        )}

        {showEmailField && (
          <EmailInput
            disabled={!!socialEmail}
            defaultValue={formData?.email || socialEmail}
            errorMessage={errors?.email?.message}
            required
            control={control}
          />
        )}

        <VStack alignItems="baseline" mt={2} width="100%">
          <Text as="p" textStyle="bold12">
            Sua senha deve ter:
          </Text>

          <ValidationText
            hasError={!!errors?.password?.types?.minCharacters || !pwValue}
          >
            8 ou mais caracteres
          </ValidationText>

          <ValidationText
            hasError={
              !!errors?.password?.types?.upperLowerCharacters || !pwValue
            }
          >
            Letras maiúsculas e minúsculas
          </ValidationText>

          <ValidationText
            hasError={!!errors?.password?.types?.numberCharacters || !pwValue}
          >
            Pelo menos um número
          </ValidationText>
        </VStack>

        <FormItemControl
          label={
            requireCurrentPassword ? 'Insira sua nova senha' : 'Crie sua senha'
          }
          name="password"
          defaultValue={formData?.password}
          errorMessage={errors?.password?.message}
          required
          control={control}
          as={BemTextInput}
          type="password"
          rules={{
            validate: {
              minCharacters: (value) =>
                value?.length >= 8 || GENERIC_ERROR_PASSWORD_MESSAGE,
              upperLowerCharacters: (value) =>
                /(?=.*[a-z])(?=.*[A-Z])/.test(value) ||
                GENERIC_ERROR_PASSWORD_MESSAGE,
              numberCharacters: (value) =>
                /(?=.*[0-9])/.test(value) || GENERIC_ERROR_PASSWORD_MESSAGE,
            },
          }}
          onBlur={() => !!curRepeatPassword && trigger('repeatPassword')}
          hasStatusIcon={!!(dirtyFields?.password || formData?.password)}
        />

        <FormItemControl
          label={
            requireCurrentPassword
              ? 'Repita sua nova senha'
              : 'Repita sua senha'
          }
          name="repeatPassword"
          defaultValue={formData?.repeatPassword}
          errorMessage={errors?.repeatPassword?.message}
          required
          control={control}
          as={BemTextInput}
          type="password"
          rules={{
            validate: (value: string) =>
              value === pwValue || 'As senhas informadas são diferentes',
          }}
          onBlur={() => !!curRepeatPassword && trigger('repeatPassword')}
          hasStatusIcon={
            !!(dirtyFields?.repeatPassword || formData?.repeatPassword)
          }
        />
      </Grid>
    </Flex>
  );
};

export default SecurityForm;
