import { isValidEmail } from '@brazilian-utils/brazilian-utils';

import { FormItemControl } from '../../inputs/form-item/form-item-control';
import { BemTextInput } from '../../inputs/text-input';

export interface EmailInputProps {
  label?: string;
  name?: string;
  defaultValue?: string;
  required?: boolean;
  errorMessage?: string;
  control;

  disabled?: boolean;

  onBlur?(): void;
}

export const EmailInput: React.FC<EmailInputProps> = ({
  label = 'E-mail',
  name = 'email',
  defaultValue,
  errorMessage,
  required,
  disabled,
  control,
  ...rest
}) => {
  return (
    <FormItemControl
      label={label}
      name={name}
      required={required}
      type="email"
      disabled={disabled}
      defaultValue={defaultValue}
      errorMessage={errorMessage}
      rules={{
        validate: {
          emailValidator(email) {
            if (email && !isValidEmail(email)) {
              return 'E-mail inválido';
            }
            return true;
          },
        },
      }}
      control={control}
      as={BemTextInput}
      {...rest}
    />
  );
};
