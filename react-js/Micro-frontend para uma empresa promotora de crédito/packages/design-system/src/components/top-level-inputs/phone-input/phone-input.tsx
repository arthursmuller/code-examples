import { useState, FC, forwardRef } from 'react';

import NumberFormat from 'react-number-format';
import {
  isValidLandlinePhone,
  isValidMobilePhone,
} from '@brazilian-utils/brazilian-utils';
import { useUpdateEffect } from 'react-use';

import { FormElRef } from 'components/inputs/form-item';

import {
  getInitialMask,
  isValidLandlinePhoneFirstNumber,
  LANDLINE_PHONE_MASK,
  MOBILE_PHONE_MASK,
} from './phone.utils';

import { FormItemControl } from '../../inputs/form-item/form-item-control';

interface InnerInputProps {
  value?: string;
  currentMask?: string;
  disabled?: boolean;
  name?: string;
  onChange?: (e) => void;
  onBlur?: (e) => void;
}

export const InnerInput = forwardRef<FormElRef, InnerInputProps>(
  (
    { value: externalValue, currentMask, disabled, name, onChange, onBlur },
    ref,
  ) => {
    const [internalValue, setInternalValue] = useState<string | undefined>(
      externalValue || '',
    );

    useUpdateEffect(() => {
      if (internalValue !== externalValue) {
        setInternalValue(externalValue ?? '');
        onBlur && onBlur({ target: { value: externalValue ?? '' } });
      }
    }, [externalValue]);

    return (
      <NumberFormat
        disabled={disabled}
        value={internalValue}
        format={currentMask}
        name={name}
        onValueChange={({ value }) => {
          setInternalValue(value);
          onChange(value);
        }}
        onBlur={onBlur}
        mask="_"
        autoComplete="out"
        ref={ref}
      />
    );
  },
);

export interface PhoneInputProps {
  label: string;
  name: string;
  defaultValue: string;
  required?: boolean;
  errorMessage?: string;
  control;
  background?: string;
  disabled?: boolean;

  onBlur?(value: string): void;

  acceptMobilePhone?: boolean;
  acceptLandlinePhone?: boolean;
}

export const PhoneInput: FC<PhoneInputProps> = ({
  label,
  name,
  defaultValue,
  required = true,
  errorMessage,
  control,
  onBlur,
  acceptMobilePhone = true,
  acceptLandlinePhone = true,
  disabled = false,
  background,
}) => {
  const [currentMask, setCurrentMask] = useState<string>(() => {
    if (!acceptMobilePhone && !acceptLandlinePhone) {
      throw new Error(
        'PhoneInput must have props acceptMobilePhone or acceptLandlinePhone',
      );
    }

    if (acceptMobilePhone && acceptLandlinePhone) {
      return getInitialMask(defaultValue);
    }

    if (acceptMobilePhone) {
      return MOBILE_PHONE_MASK;
    }

    if (acceptLandlinePhone) {
      return LANDLINE_PHONE_MASK;
    }

    return getInitialMask(defaultValue);
  });

  function safeSetMask(nextMask: string): void {
    if (currentMask !== nextMask) {
      setCurrentMask(nextMask);
    }
  }

  function handleChange(value: string): void {
    if (acceptLandlinePhone && acceptMobilePhone)
      if (value && value.length > 2) {
        if (isValidLandlinePhoneFirstNumber(value)) {
          safeSetMask(LANDLINE_PHONE_MASK);
        } else {
          safeSetMask(MOBILE_PHONE_MASK);
        }
      }
  }

  return (
    <FormItemControl
      background={background}
      label={label}
      name={name}
      defaultValue={defaultValue}
      required={required}
      disabled={disabled}
      errorMessage={errorMessage}
      rules={{
        validate: {
          phoneValidator(phone) {
            if (
              acceptMobilePhone &&
              !!phone &&
              currentMask === MOBILE_PHONE_MASK &&
              !isValidMobilePhone(phone)
            ) {
              return 'Celular inválido';
            }

            if (
              acceptLandlinePhone &&
              !!phone &&
              currentMask === LANDLINE_PHONE_MASK &&
              !isValidLandlinePhone(phone)
            ) {
              return 'Telefone inválido';
            }

            return true;
          },
        },
      }}
      control={control}
      onChange={handleChange}
      onBlur={onBlur}
      as={InnerInput}
      currentMask={currentMask}
    />
  );
};
