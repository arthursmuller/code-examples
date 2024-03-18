import { FC } from 'react';

const intl = new Intl.NumberFormat('pt-BR', {
  style: 'currency',
  currency: 'BRL',
});

export const formatCurrency = (value: number): string => intl.format(value);

export const CurrencyDisplay: FC<{ value: number }> = ({ value }) => (
  <>{formatCurrency(value)}</>
);
