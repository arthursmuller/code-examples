import { FC, useState, forwardRef, ChangeEvent } from 'react';

import { Input, Textarea } from '@chakra-ui/react';
import { useUpdateEffect } from 'react-use';

import { FormElRef } from '../form-item';

export interface BemTextInputProps {
  defaultValue?: string | number;
  name?: string;
  type?: 'text' | 'number' | 'password' | 'email' | 'phone' | 'textarea';
  disabled?: boolean;
  onBlur?: (e) => void;
  onChange?: (e) => void;
  value?: string | number;
  maxlength?: string | number;
}

export const BemTextInput = forwardRef<FormElRef, BemTextInputProps>(
  (
    {
      defaultValue,
      name,
      onBlur,
      onChange,
      type = 'text',
      disabled,
      value: externalValue,
      maxlength,
    },
    ref,
  ) => {
    const [internalValue, setInternalValue] = useState<string | undefined>(
      defaultValue?.toString() || '',
    );

    useUpdateEffect(() => {
      if (internalValue !== externalValue?.toString()) {
        setInternalValue(externalValue?.toString() ?? '');
        onBlur &&
          onBlur({ target: { value: externalValue?.toString() ?? '' } });
      }
    }, [externalValue]);

    const handleBlur = (
      event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    ): void => {
      if (onBlur || onChange) {
        const nextEvent = {
          ...event,
          target: { ...event.target, value: event.target.value.trim() },
        };

        onBlur && onBlur(nextEvent);
        onChange && onChange(nextEvent);
      }
    };

    const handleChange = (
      event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    ): void => {
      setInternalValue(event.target.value);
      onChange && onChange(event);

      // Chrome load's autofill set, it does not contain inputType definition
      if (!(event.nativeEvent as any).inputType) {
        handleBlur(event);
      }
    };

    const Comp: FC<any> = type === 'textarea' ? Textarea : Input;

    return (
      <Comp
        type={type}
        name={name}
        disabled={disabled}
        onBlur={handleBlur}
        onChange={handleChange}
        ref={ref}
        variant="unstyled"
        value={internalValue}
        maxLength={maxlength}
      />
    );
  },
);
