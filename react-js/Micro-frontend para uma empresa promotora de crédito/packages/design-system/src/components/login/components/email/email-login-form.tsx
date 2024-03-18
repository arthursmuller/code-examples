import { FC, ReactElement, useState } from 'react';

import {
  Flex,
  Button,
  IconButton,
  Box,
  SimpleGrid,
  ScaleFade,
} from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import {
  VisibilityHideIcon,
  VisibilityShowIcon,
} from '@pcf/design-system-icons';

import { BemTextInput, FormItemControl } from '../../../inputs';
import { EmailInput } from '../../../top-level-inputs/email-input';
import { BemAlert } from '../../../alert';

export interface EmailLoginFormData {
  email: string;
  password: string;
}

export interface EmailLoginFormProps {
  renderCustomFooter?({
    isSubmitting,
  }: {
    isSubmitting: boolean;
  }): ReactElement;
  onSubmit(data: EmailLoginFormData): void;
  initialEmail?: string;
  disableEmail?: boolean;
  navigateToRecoverPassword?: () => void;
  isSubmitting?: boolean;
  error?: string;
  onResetError?: () => void;
}

export const EmailLoginForm: FC<EmailLoginFormProps> = ({
  renderCustomFooter,
  onSubmit,
  initialEmail = '',
  disableEmail,
  navigateToRecoverPassword,
  error = '',
  isSubmitting,
  onResetError,
}) => {
  const [show, setShow] = useState<boolean>();
  const {
    handleSubmit,
    formState: { errors },
    control,
  } = useForm<EmailLoginFormData>();

  return (
    <Flex
      as="form"
      flexDirection="column"
      onSubmit={handleSubmit((data) => {
        onSubmit({ ...data, email: disableEmail ? initialEmail : data.email });
      })}
      noValidate
    >
      <SimpleGrid columns={1} spacingY={4} mb={4}>
        <EmailInput
          disabled={disableEmail}
          defaultValue={initialEmail}
          errorMessage={errors?.email?.message}
          required
          control={control}
        />

        <FormItemControl
          label="Senha"
          name="password"
          type={show ? 'text' : 'password'}
          defaultValue=""
          errorMessage={errors?.password?.message}
          required
          control={control}
          as={BemTextInput}
          icon={
            <IconButton
              color="grey.800"
              variant="ghost"
              aria-label="mostrar"
              icon={
                !show ? (
                  <VisibilityShowIcon width="24px" />
                ) : (
                  <VisibilityHideIcon width="24px" />
                )
              }
              size="sm"
              onClick={() => setShow(!show)}
            >
              {show ? 'Hide' : 'Show'}
            </IconButton>
          }
        />

        <ScaleFade in={!!error} unmountOnExit>
          <BemAlert
            status="error"
            description={error}
            mb="10px"
            onClose={() => onResetError && onResetError()}
          />
        </ScaleFade>
      </SimpleGrid>

      {!renderCustomFooter ? (
        <>
          <Box mt="2px">
            <Button
              isFullWidth
              isLoading={isSubmitting}
              loadingText="Acessando"
              type="submit"
            >
              Acessar minha conta
            </Button>
          </Box>

          <Flex justifyContent="center">
            {navigateToRecoverPassword && (
              <Button
                variant="link"
                color="secondary.regular"
                mt="23px"
                fontSize="14px"
                onClick={navigateToRecoverPassword}
              >
                Esqueceu sua senha?
              </Button>
            )}
          </Flex>
        </>
      ) : (
        renderCustomFooter({ isSubmitting })
      )}
    </Flex>
  );
};
