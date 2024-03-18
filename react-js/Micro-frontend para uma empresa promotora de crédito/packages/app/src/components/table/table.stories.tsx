import { Story } from '@storybook/react';
import { Link } from '@chakra-ui/react';

import { ValidationText } from 'components/validation-text';

import { BemTable, TableProps } from './table';

type DataSourceData = {
  key: string;
  proposta: string;
  data: string;
  conveniada: string;
  valorLiberado: string;
  situacao: string;
};

const columns = [
  {
    title: 'Nº da Proposta',
    dataIndex: 'proposta',
    key: 'proposta',
  },
  {
    title: 'Data',
    dataIndex: 'data',
    key: 'data',
  },
  {
    title: 'Descrição',
    dataIndex: 'conveniada',
    key: 'conveniada',
  },
  {
    title: 'Valor liberado (R$)',
    dataIndex: 'valorLiberado',
    key: 'valorLiberado',
  },
  {
    title: 'Situação',
    dataIndex: 'situacao',
    key: 'situacao',
    render: (situacao) => {
      return <ValidationText hasError>{situacao}</ValidationText>;
    },
  },
  {
    title: '',
    render: () => {
      return (
        <Link textStyle="link" color="secondary.mid-dark" to="/#">
          Ver mais
        </Link>
      );
    },
  },
];

export default {
  title: 'Table',
  component: BemTable,
};

const Template: Story<TableProps<DataSourceData>> = (args) => {
  return <BemTable {...args} />;
};

export const Simple = Template.bind({});
Simple.args = {
  dataSource: [
    {
      key: '1',
      proposta: '#0000000000',
      data: '21/10/2020',
      conveniada: 'Conveniada INSSDATAPREV',
      valorLiberado: 'R$ 15.000,00',
      situacao: 'Em análise',
    },
    {
      key: '2',
      proposta: '#0000000000',
      data: '21/10/2020',
      conveniada: 'Conveniada INSSDATAPREV',
      valorLiberado: 'R$ 15.000,00',
      situacao: 'Em análise',
    },
    {
      key: '3',
      proposta: '#0000000000',
      data: '21/10/2020',
      conveniada: 'Conveniada INSSDATAPREV',
      valorLiberado: 'R$ 15.000,00',
      situacao: 'Em análise',
    },
    {
      key: '4',
      proposta: '#0000000000',
      data: '21/10/2020',
      conveniada: 'Conveniada INSSDATAPREV',
      valorLiberado: 'R$ 15.000,00',
      situacao: 'Em análise',
    },
    {
      key: '5',
      proposta: '#0000000000',
      data: '21/10/2020',
      conveniada: 'Conveniada INSSDATAPREV',
      valorLiberado: 'R$ 15.000,00',
      situacao: 'Em análise',
    },
    {
      key: '6',
      proposta: '#0000000000',
      data: '21/10/2020',
      conveniada: 'Conveniada INSSDATAPREV',
      valorLiberado: 'R$ 15.000,00',
      situacao: 'Em análise',
    },
  ],
  columns,
  pagination: { defaultPageSize: 5 },
};

export const Loading = Template.bind({});
Loading.args = {
  loading: true,
  columns,
};

export const Empty = Template.bind({});
Empty.args = {
  dataSource: [],
  columns,
};
