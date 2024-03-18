import { FC } from 'react';

import { ColumnsType } from 'antd/lib/table';

import { BemErrorBoundary, formatCurrency } from '@pcf/design-system';
import {
  ContratoModel,
  extractReadableErrorMessage,
  useContratosQuery,
} from '@pcf/core';
import { BemTable } from 'components/table';

const columns: ColumnsType<ContratoModel> = [
  {
    title: 'Matrícula',
    dataIndex: 'matricula',
    render: (_, { matricula }) => `#${matricula}`,
  },
  {
    title: 'Contrato',
    dataIndex: 'contrato',
  },
  {
    title: 'Parcelas Pagas',
    render: (_, contrato) => {
      return `${contrato.qtdParcelasPagas} de ${contrato.qtdParcelas}`;
    },
  },
  {
    title: 'Valor Parcela',
    dataIndex: 'prestacao',
    render: (_, { prestacao }) => {
      return prestacao ? formatCurrency(prestacao) : '';
    },
  },
];

export const ContractsTableContent: FC = () => {
  const { data, isLoading } = useContratosQuery();

  return (
    <BemTable<ContratoModel>
      dataSource={data}
      pagination={{ defaultPageSize: 5, style: { marginRight: '16px' } }}
      columns={columns}
      rowKey={(contrato) => contrato.contrato}
      loading={isLoading}
    />
  );
};

export const ContractsTable: FC = (props) => (
  <BemErrorBoundary errorMessage={extractReadableErrorMessage}>
    <ContractsTableContent {...props} />
  </BemErrorBoundary>
);
