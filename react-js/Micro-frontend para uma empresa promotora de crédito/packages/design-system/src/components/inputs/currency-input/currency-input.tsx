import { forwardRef } from 'react';

import IntlCurrencyInput from 'react-intl-currency-input';

import { FormElRef } from '../form-item';

const config = {
  locale: 'pt-BR',
  formats: {
    number: {
      BRL: {
        style: 'currency',
        currency: 'BRL',
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
      },
    },
  },
};

export interface BemCurrencyInputProps {
  defaultValue?: number;
  returnMasked?: boolean;
  name?: string;
  disabled?: boolean;
  onChange?: (nextValue: number | string) => void;
  onBlur?: (nextValue: number | string) => void;
  maxValue?: number;
}

export const BemCurrencyInput = forwardRef<FormElRef, BemCurrencyInputProps>(
  (
    {
      defaultValue = 0,
      returnMasked = false,
      maxValue = 10000000,
      name,
      disabled,
      onChange,
      onBlur,
    },
    ref,
  ) => {
    const handleChange = (
      event: Event,
      value: number,
      maskedValue: string,
    ): void => {
      event.preventDefault();
      const nextValue = returnMasked ? maskedValue : value;

      onChange && onChange(nextValue);
    };

    const handleBlur = (
      event: Event,
      value: number,
      maskedValue: string,
    ): void => {
      event.preventDefault();
      const nextValue = returnMasked ? maskedValue : value;

      onBlur && onBlur(nextValue);
    };

    return (
      <IntlCurrencyInput
        currency="BRL"
        config={config}
        onChange={handleChange}
        onBlur={handleBlur}
        max={maxValue}
        value={defaultValue}
        name={name}
        disabled={disabled}
        type="tel"
      />
    );
  },
);
