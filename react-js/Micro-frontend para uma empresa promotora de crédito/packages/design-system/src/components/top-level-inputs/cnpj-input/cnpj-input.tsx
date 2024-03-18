import { FC } from 'react';

import NumberFormat from 'react-number-format';
import { isValidCNPJ } from '@brazilian-utils/brazilian-utils';

import {
  FormItemControl,
  FormItemControlProps,
} from '../../inputs/form-item/form-item-control';

interface CpnjInputProps extends Partial<FormItemControlProps<NumberFormat>> {
  defaultValue: string;
  control;
  errorMessage?: string;
  required?: boolean;

  onBlur?(value: string): void;
}

export const CnpjInput: FC<CpnjInputProps> = (props) => {
  return (
    <FormItemControl
      {...props}
      label="CNPJ"
      name="cnpj"
      rules={{
        validate: {
          cnpjValidator(cnpj) {
            if (!isValidCNPJ(cnpj)) {
              return 'CNPJ inválido';
            }
            return true;
          },
        },
      }}
      as={NumberFormat}
      format="##.###.###/####-##"
      mask="_"
    />
  );
};
