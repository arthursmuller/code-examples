import { FC } from 'react';

import {
  format as DateFnsFormat,
  formatDistance as DateFnsFormatRelative,
} from 'date-fns';
import { ptBR } from 'date-fns/locale';
import { Text, TextProps } from '@chakra-ui/react';

const opts = {
  locale: ptBR,
  addSuffix: true,
};

export enum DefaultFormatStrings {
  input = 'dd/MM/yyyy',
  api = 'yyyy-MM-dd',
  short = 'PP',
  extended = 'PPPP',
}

export const formatDate = (
  date: Date,
  formatString: string = DefaultFormatStrings.short,
): string => DateFnsFormat(date, formatString, opts);

export const formatRelative = (date: Date, baseDate = new Date()): string =>
  DateFnsFormatRelative(date, baseDate, opts);

export interface DateDisplayProps extends TextProps {
  date?: Date;
  formatStr?: string;
  baseDate?: Date;
  relative?: boolean;
}

export const DateDisplay: FC<DateDisplayProps> = ({
  date = new Date(),
  formatStr,
  baseDate,
  relative,
  ...textProps
}) => (
  <Text
    {...textProps}
    textTransform="lowercase"
    sx={{ ':first-letter': { textTransform: 'capitalize' } }}
  >
    {relative ? formatRelative(date, baseDate) : formatDate(date, formatStr)}
  </Text>
);
