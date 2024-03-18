import { render, screen } from '@testing-library/react';
import { rest } from 'msw';
import { setupServer } from 'msw/node';
import { MemoryRouter, Route } from 'react-router-dom';
import { ChakraProvider } from '@chakra-ui/react';

import {
  intencaoOperacaoByIdEndpointGen,
  IntencaoOperacaoModel,
} from '@pcf/core';
import bemTheme from '@pcf/design-system';
import { mainRoutePaths } from 'features/main/routes';
import { pcfApi, pcfApiResponse } from 'setupTests';

import { ResumoSolicitacao } from './resumo-solicitacao';

const mockedData: IntencaoOperacaoModel = {
  id: 1,
  prestacao: 200,
  valorAuxilioFinanceiro: 200,
  taxaMes: 1,
  taxaAno: 1,
  valorFinanciado: 2000,
  dataAtualizacao: '2000-03-12T00:00:00',
  dataInclusao: '2000-03-12T00:00:00',
  primeiroVencimento: '2000-03-12T00:00:00',
  prazo: 1,

  tipoOperacao: { id: 1, nome: 'Novo', sigla: 'N' },
  produto: {
    id: 1,
    nome: 'Consignado',
    sigla: 'Q',
    requerConvenio: false,
  },
  lead: undefined,
  loja: {
    id: 2,
    proximidade: 0,
    nome: 'Loja Teste',
    latitude: 0,
    longitude: 0,
    endereco: 'Rua Teste',
    cidade: 'Porto Alegre',
    estado: 'RS',
    bairro: 'Centro',
    cep: '90000233',
    mensagemApresentacao: 'Hehe',
    telefones: undefined,
  },
  passosProduto: [
    {
      id: 0,
      titulo: 'Solicitação',
      descricao: 'Solicitação et lorem ipsum dolor sit amet',
      completo: true,
      excepcional: false,
      pendenciaUsuario: false,
    },
    {
      id: 0,
      titulo: 'Análise',
      descricao: 'Análise et lorem ipsum dolor sit amet',
      completo: true,
      excepcional: false,
      pendenciaUsuario: false,
    },
    {
      id: 0,
      titulo: 'Assinatura Digital',
      descricao: 'Assinatura digital et ipsum dolor sit amet',
      completo: false,
      excepcional: false,
      pendenciaUsuario: false,
    },
    {
      id: 0,
      titulo: 'Aprovada',
      descricao: 'Aprovada et lorem ipsum dolor sit amet',
      completo: false,
      excepcional: false,
      pendenciaUsuario: false,
    },
  ],

  rendimento: {
    id: 2,
    convenio: {
      id: 1,
      nome: 'INSS DATAPREV',
      codigo: '000020',
      descricao:
        'Disponível para Aposentados e pensionistas do INSS. O valor do empréstimo é descontado diretamente do benefício.',
    },
    convenioOrgao: null,
    uf: null,
    banco: null,
    tipoConta: null,
    agencia: '1234',
    conta: '1234',
    valorRendimento: 1232.22,
    inssEspecieBeneficio: null,
    siapeTipoFuncional: null,
    matricula: '123456772',
    dataInscricaoBeneficio: '1990-10-12T00:00:00',
    dataAdmissao: '0001-01-01T00:00:00',
    matriculaInstituidor: null,
    nomeInstituidor: null,
    possuiRepresentacaoPorProcurador: false,
  },
  usuario: {
    id: 1,
    nome: 'G L',
    email: 'unit.the.test@test.com',
    cpf: '123.456.789-00',
    administrador: false,
    loja: null,
    cliente: null,
  },
};

const server = setupServer(
  rest.get<IntencaoOperacaoModel>(
    pcfApi(intencaoOperacaoByIdEndpointGen({ id: '1' })[0]),
    pcfApiResponse(mockedData),
  ),
);

beforeAll(() => {
  server.listen({ onUnhandledRequest: 'warn' });
});

afterAll(() => {
  server.close();
  jest.clearAllMocks();
});

test('Resumo Solicitacao renders and fill info', async () => {
  render(
    <ChakraProvider resetCSS theme={bemTheme}>
      <MemoryRouter initialEntries={[`${mainRoutePaths.RESUMO_SOLICITACAO}/1`]}>
        <Route
          exact
          path={`${mainRoutePaths.RESUMO_SOLICITACAO}/:intencaoOperacaoId`}
          component={ResumoSolicitacao}
        />
      </MemoryRouter>
    </ChakraProvider>,
  );

  expect(
    await screen.findByRole('button', { name: /dados da solicitação/i }),
  ).toBeVisible();
  expect(screen.getByText(/123.456.789-00/i)).toBeVisible();

  expect(
    screen.getByText(/Análise et lorem ipsum dolor sit amet/i),
  ).toBeVisible();
  expect(
    screen.queryByText(/Solicitação et lorem ipsum dolor sit amett/i),
  ).not.toBeInTheDocument();
});
