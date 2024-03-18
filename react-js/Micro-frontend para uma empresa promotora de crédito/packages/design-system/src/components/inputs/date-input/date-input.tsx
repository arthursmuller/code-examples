import { useState, FocusEvent, forwardRef } from 'react';

import NumberFormat from 'react-number-format';
import { isAfter, isBefore, isValid } from 'date-fns';
import { useUpdateEffect } from 'react-use';

import { DefaultFormatStrings, formatDate } from '../../date';
import { FormElRef } from '../form-item';

const limitFn = (val: string, max: string): string => {
  const nextValue =
    val.length === 1
      ? (val[0] > max[0] && `0${val}.`) || val
      : (val > max && max) || (val < '01' && '01') || val;

  return nextValue;
};

const validateFn = (prev: string, current: string, index: number): string => {
  let nextValue = current;

  if (/\d/.test(current)) {
    if (index === 0) nextValue = limitFn(nextValue, '31');
    else if (index === 1) nextValue = limitFn(nextValue, '12');
  }

  return `${prev}${nextValue}`;
};

export interface BemDateInputProps {
  defaultValue?: Date;
  minDate?: Date;
  maxDate?: Date;
  name: string;
  disabled?: boolean;
  onChange?: (nextValue: Date | string | undefined) => void;
  onBlur?: (nextValue: Date | string | undefined) => void;
  value?: Date;
}

export const BemDateInput = forwardRef<FormElRef, BemDateInputProps>(
  (
    {
      defaultValue,
      onChange,
      onBlur,
      name,
      disabled,
      minDate = new Date('01/01/1900'),
      maxDate = new Date('01/01/2100'),
      value: externalValue,
    },
    ref,
  ) => {
    const [internalValue, setInternalValue] = useState<string>(
      (defaultValue && formatDate(defaultValue, DefaultFormatStrings.input)) ||
        '',
    );

    useUpdateEffect(() => {
      if (internalValue !== externalValue?.toString()) {
        const nextValue =
          (externalValue &&
            formatDate(externalValue, DefaultFormatStrings.input)) ||
          '';
        setInternalValue(nextValue);

        onBlur && onBlur(nextValue);
      }
    }, [externalValue]);

    const handleBlur = ({ target }: FocusEvent<HTMLInputElement>): void => {
      const [day, month, year] = target.value
        .split('/')
        .map((v) => parseInt(v, 10));

      const date = new Date(year, month - 1, day);

      let nextDate = date.getDate() === day && isValid(date) ? date : '';

      if (nextDate) {
        if (maxDate && isAfter(date, maxDate)) {
          nextDate = maxDate;
          setInternalValue(formatDate(nextDate, DefaultFormatStrings.input));
        } else if (minDate && isBefore(date, minDate)) {
          nextDate = minDate;
          setInternalValue(formatDate(nextDate, DefaultFormatStrings.input));
        }
      }

      !nextDate && setInternalValue('');
      onChange && onChange(nextDate);
      onBlur && onBlur(nextDate);
    };

    const handleInputChange = ({
      target,
    }: FocusEvent<HTMLInputElement>): void => {
      const nextValue = target.value
        .replace(/_/g, '')
        .split('/')
        .reduce(validateFn, '');

      setInternalValue(nextValue);
    };

    return (
      <NumberFormat
        name={name}
        disabled={disabled}
        value={internalValue}
        onBlur={handleBlur}
        onChange={handleInputChange}
        format="##/##/####"
        mask="_"
      />
    );
  },
);
