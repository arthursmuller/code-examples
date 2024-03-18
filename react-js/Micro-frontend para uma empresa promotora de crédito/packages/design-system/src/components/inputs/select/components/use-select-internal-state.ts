import { useState } from "react";

import { useUpdateEffect } from "react-use";

export const useSelectInternalState = (
  defaultValue: string | string[],
  externalValue: string | string[],
  onBlur,
  onChange,
): [string|string[], string, (nextValue: string, opt?) => void] => {
  const [value, setValue] = useState(defaultValue);
  const [textValue, setTextValue] = useState<string>();

  const handleChange = (nextValue, opt?): void => {
    opt && setTextValue(opt?.name);
    setValue(nextValue);
    onBlur && onBlur(nextValue);
    onChange && onChange(nextValue);
  };

  useUpdateEffect(() => {
    if (value !== externalValue) {
      handleChange(externalValue || '');
    }
  }, [externalValue]);

  return [value, textValue, handleChange];
};