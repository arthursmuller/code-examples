import { FC } from 'react';

import NumberFormat from 'react-number-format';
import { isValidCPF } from '@brazilian-utils/brazilian-utils';

import {
  FormItemControl,
  FormItemControlProps,
} from '../../inputs/form-item/form-item-control';

interface CpfInputProps extends Partial<FormItemControlProps<NumberFormat>> {
  defaultValue: string | undefined;
  control;
  errors;
  label?: string;

  onBlur?(value: string): void;
}

export const CpfInput: FC<CpfInputProps> = ({
  errors,
  label = 'CPF',
  ...props
}) => (
  <FormItemControl
    {...props}
    label={label}
    name="cpf"
    errorMessage={errors?.cpf?.message}
    rules={{
      validate: {
        cpfValidator(cpf) {
          if (!!cpf && !isValidCPF(cpf)) {
            return 'CPF inválido';
          }
          return true;
        },
      },
    }}
    as={NumberFormat}
    format="###.###.###-##"
    mask="_"
    onBlur={(e) => {
      props.onBlur && props.onBlur(e.target.value);
    }}
  />
);
