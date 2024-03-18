import { FC } from 'react';

import {
  HStack,
  Icon,
  Link,
  StackDivider,
  Text,
  Tooltip,
} from '@chakra-ui/react';
import { Link as ReactRouterDomLink } from 'react-router-dom';
import { ColumnsType } from 'antd/lib/table';

import {
  formatCurrency,
  formatDate,
  DefaultFormatStrings,
} from '@pcf/design-system';
import { IntencaoOperacaoModel, useIntencoesOperacao } from '@pcf/core';
import {
  TabNovoInativaIcon,
  TabPortabInativaIcon,
  TabRefinInativaIcon,
} from '@pcf/design-system-icons';
import { mainRoutePaths } from 'features/main/routes';
import { BemTable } from 'components/table';

import { SituationProposalText } from './situation-proposal-text';

const ProductIconMap = {
  N: TabNovoInativaIcon,
  P: TabPortabInativaIcon,
  R: TabRefinInativaIcon,
};

const columns: ColumnsType<IntencaoOperacaoModel> = [
  {
    title: 'Número Proposta',
    dataIndex: 'id',
    render: (_, { id, tipoOperacao }) => {
      return (
        <HStack spacing={3} divider={<StackDivider borderColor="grey.600" />}>
          <Tooltip
            hasArrow
            placement="top"
            label={tipoOperacao.nome}
            aria-label={`ícone ${tipoOperacao.nome}`}
          >
            <span>
              <Icon boxSize={6} as={ProductIconMap[tipoOperacao.sigla]} />
            </span>
          </Tooltip>
          <Text textStyle="regular16">{id}</Text>
        </HStack>
      );
    },
  },
  {
    title: 'Data Solicitação',
    dataIndex: 'dataInclusao',
    render: (_, { dataInclusao }) => {
      return formatDate(new Date(dataInclusao), DefaultFormatStrings.input);
    },
  },
  {
    title: 'Matrícula',
    dataIndex: 'rendimento',
    render: (_, { rendimento }) =>
      rendimento?.matricula ? `#${rendimento?.matricula}` : '',
  },
  {
    title: 'Valor Parcela',
    dataIndex: 'prestacao',
    render: (_, { prestacao }) => {
      return prestacao ? formatCurrency(prestacao) : '';
    },
  },
  {
    title: 'Parcelas',
    dataIndex: 'prazo',
  },
  {
    title: 'Situação',
    render: (_, { passosProduto }) => {
      return <SituationProposalText passosProduto={passosProduto} />;
    },
  },
  {
    render: (_, { id }) => {
      return (
        <Link
          color="secondary.mid-dark"
          textStyle="bold14"
          as={ReactRouterDomLink}
          to={`${mainRoutePaths.RESUMO_SOLICITACAO}/${id}`}
        >
          Ver mais
        </Link>
      );
    },
  },
];

export const ProposalsTable: FC = () => {
  const { data, isLoading } = useIntencoesOperacao();

  return (
    <BemTable<IntencaoOperacaoModel>
      dataSource={data}
      pagination={{ defaultPageSize: 5, style: { marginRight: '16px' } }}
      columns={columns}
      rowKey={(intencaoOperacao) => intencaoOperacao.id}
      loading={isLoading}
    />
  );
};
