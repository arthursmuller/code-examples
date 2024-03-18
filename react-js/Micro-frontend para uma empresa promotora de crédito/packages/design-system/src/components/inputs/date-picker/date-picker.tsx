import { FC, useEffect, useState } from 'react';

import { Flex } from '@chakra-ui/react';
import { css } from '@emotion/react';
// import DatePicker from 'react-date-picker';

const style = css`
  width: 100%;

  .bem-picker {
    width: 100%;

    .react-date-picker__wrapper {
      border: none;
    }
  }
`;

export interface BemDatePickerProps {
  name?: string;
  defaultValue?: Date;
  disabled?: boolean;
  minDate?: Date;
  maxDate?: Date;
  onChange?: (nextValue: Date | undefined) => void;
  onBlur?: (nextValue: Date | undefined) => void;
}

export const BemDatePicker: FC<BemDatePickerProps> = (
  { name, defaultValue, disabled, onChange, onBlur, minDate, maxDate },
  ref,
) => {
  const [value, setDate] = useState(defaultValue);

  useEffect(() => {
    onChange && onChange(value);
    onBlur && onBlur(value);
  }, [value, onChange, onBlur]);

  const handleChange = (date: Date | Date[]): void => {
    setDate(date as Date);
  };

  return (
    <Flex css={style}>
      {/* <DatePicker
        name={name}
        disabled={disabled}
        className="bem-picker"
        onChange={handleChange}
        value={value}
        dayPlaceholder="__"
        yearPlaceholder="__"
        monthPlaceholder="__"
        format="dd MM yyyy"
        locale="pt-BR"
        minDate={minDate}
        maxDate={maxDate}
      /> */}
    </Flex>
  );
};
