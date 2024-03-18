import { rest } from 'msw';
import { setupServer } from 'msw/node';
import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { RendimentoResponseModel, RENDIMENTOS_QUERY_ENDPOINT ,
  ContratoModel,
  CONTRATOS_QUERY_ENDPOINT,
  InformacaoProdutoOperacao,
  REFIN_QUERY_ENDPOINT,
  SimulacaoNovoRefinRequisicaoModel,
  SimulacaoNovoRetornoModel,
  TIPO_OPERACAO_QUERY_ENDPOINT_REFIN,
,
  IntencaoOperacaoRequisicaoModel,
  INTENCAO_OPERACAO_ENDPOINT,
} from '@pcf/core';

import { ModalProvider } from '@pcf/design-system';


import { RefinanciamentoProvided as Refin } from '.';

import { pcfApi, pcfApiResponse } from 'setupTests';

jest.mock('app/app-providers', (): any => {
  const actual = jest.requireActual('app/app-providers');

  return {
    ...actual,
    queryCacheConfig: {
      ...actual.queryCacheConfig,
      prefetchQuery: jest.fn(),
    },
  };
});

const matriculaMock: RendimentoResponseModel = {
  id: 1,
  agencia: '1234',
  banco: {
    id: 1,
    nome: 'Banco test',
    codigo: '1',
  },
  conta: '12345',
  convenio: {
    id: 1,
    nome: 'INSS',
    descricao: 'INSS',
    codigo: '1',
  },
  convenioOrgao: undefined,
  dataAdmissao: new Date(),
  dataInscricaoBeneficio: new Date(),
  siapeTipoFuncional: undefined,
  inssEspecieBeneficio: {
    id: 1,
    descricao: 'Test beneficio',
    codigo: '1',
  },
  matricula: '123456789',
  matriculaInstituidor: '',
  possuiRepresentacaoPorProcurador: false,
  tipoConta: {
    id: 1,
    nome: 'Conta corrente',
    sigla: 'C',
  },
  uf: {
    id: 1,
    nome: 'GO',
    sigla: 'Goiás',
  },
  valorRendimento: 12345,
  nomeInstituidor: 'Unit, the Test',
};

const contractsMocks: ContratoModel[] = [
  {
    matricula: '123456789',
    contrato: '******1234',
    prestacao: 100,
    qtdParcelas: 5,
    qtdParcelasPagas: 1,
    taxa: 1.1,
    saldoTotal: 400,
  },
  {
    matricula: '123456789',
    contrato: '******5678',
    prestacao: 150,
    qtdParcelas: 5,
    qtdParcelasPagas: 1,
    taxa: 1.1,
    saldoTotal: 600,
  },
];

const resultsMock: SimulacaoNovoRetornoModel[] = [
  {
    prazo: 12,
    prestacao: 88,
    valorFinanciado: 1100,
    valorTotal: 1000,
    valorAF: 100,
    taxaMes: 1.01,
    taxaAno: 1.1,
    primeiroVcto: '2021-03-30T18:17:10.799Z',
  },
];

const spy = jest.fn();
const spyPost = jest.fn();

const server = setupServer(
  rest.get<RendimentoResponseModel[]>(
    pcfApi(RENDIMENTOS_QUERY_ENDPOINT),
    pcfApiResponse([matriculaMock]),
  ),
  rest.get<ContratoModel[]>(
    pcfApi(CONTRATOS_QUERY_ENDPOINT),
    pcfApiResponse(contractsMocks),
  ),
  rest.get<InformacaoProdutoOperacao>(
    pcfApi(TIPO_OPERACAO_QUERY_ENDPOINT_REFIN),
    pcfApiResponse({ idProduto: 1, idTipoOperacao: 1 }),
  ),

  rest.post<SimulacaoNovoRefinRequisicaoModel[]>(
    pcfApi(REFIN_QUERY_ENDPOINT),
    (req, res, ctx) => {
      spy(req.body);
      return pcfApiResponse(resultsMock)(req, res, ctx);
    },
  ),
  rest.post<IntencaoOperacaoRequisicaoModel>(
    pcfApi(INTENCAO_OPERACAO_ENDPOINT),
    (req, res, ctx) => {
      spyPost(req.body);
      return pcfApiResponse({})(req, res, ctx);
    },
  ),
);

beforeAll(() => server.listen());
afterEach(() => server.resetHandlers());

afterAll(() => {
  server.close();
  jest.clearAllMocks();
});

test('Should persist Refin OP', async () => {
  render(
    <ModalProvider>
      <Refin />
    </ModalProvider>,
  );

  // Matricula step
  const matriculaOpt = await screen.findByRole('checkbox');
  userEvent.click(matriculaOpt);
  const nextButton = screen.getByRole('button', { name: /continuar/i });

  await waitFor(() => {
    expect(nextButton).not.toBeDisabled();
  });
  userEvent.click(nextButton);

  // Contracts step
  await screen.findByRole('heading', {
    name: /matrícula #123456789 \(inss\)/i,
  });
  const contractOpts = await screen.findAllByRole('checkbox');
  expect(contractOpts.length).toBe(2);
  expect(screen.getByText(/0\/3/)).toBeVisible();

  userEvent.click(contractOpts[0]);
  userEvent.click(contractOpts[1]);
  expect(await screen.findByText(/2\/3/)).toBeVisible();

  const refinButton = screen.getByRole('button', { name: /refinanciar/i });
  userEvent.click(refinButton);

  const skipFilterButton = await screen.findByRole('button', {
    name: /continuar/i,
  });
  userEvent.click(skipFilterButton);

  // Results step
  await screen.findByRole('heading', {
    name: /Resultados dos prazos disponíveis para você/i,
  });
  const resultOpt = await screen.findByRole('checkbox');
  userEvent.click(resultOpt);

  expect(spy).toHaveBeenCalledWith({
    contratosRefinanciamento: [
      { contrato: '******1234' },
      { contrato: '******5678' },
    ],
    idConvenio: 1,
    prazos: [84, 96],
    prestacao: 250,
    retornarSomenteOperacoesViaveis: true,
  });

  const saveButton = screen.getByRole('button', { name: /continuar/i });
  await waitFor(() => {
    expect(saveButton).not.toBeDisabled();
  });
  userEvent.click(saveButton);

  const finishButton = await screen.findByRole('button', { name: /fechar/i });
  expect(finishButton).toBeVisible();

  expect(spyPost).toHaveBeenCalledWith({
    contratos: [{ contrato: '******1234' }, { contrato: '******5678' }],
    idProduto: 1,
    idTipoOperacao: 1,
    idRendimentoCliente: 1,
    idUsuario: -1,
    prazo: resultsMock[0].prazo,
    prestacao: resultsMock[0].prestacao,
    taxaAno: resultsMock[0].taxaAno,
    taxaMes: resultsMock[0].taxaMes,
    valorAuxilioFinanceiro: resultsMock[0].valorAF,
    valorFinanciado: resultsMock[0].valorFinanciado,
    primeiroVencimento: resultsMock[0].primeiroVcto,
  });
});

// TODO: breaking matriculas load / parallel tests; currency input label selector does not work
xtest('Should filter by custom interval', async () => {
  render(
    <ModalProvider>
      <Refin />
    </ModalProvider>,
  );

  // Matricula step
  const matriculaOpt = await screen.findByRole('checkbox');
  userEvent.click(matriculaOpt);
  const nextButton = screen.getByRole('button', { name: /continuar/i });

  await waitFor(() => {
    expect(nextButton).not.toBeDisabled();
  });
  userEvent.click(nextButton);

  // Contracts step
  await screen.findByRole('heading', {
    name: /matrícula #123456789 \(inss\)/i,
  });
  const contractOpts = await screen.findAllByRole('checkbox');
  userEvent.click(contractOpts[0]);
  expect(await screen.findByText(/1\/3/)).toBeVisible();

  const refinButton = screen.getByRole('button', { name: /refinanciar/i });
  userEvent.click(refinButton);

  const filterButton = await screen.findByRole('button', {
    name: /personalizar valor de parcela/i,
  });
  userEvent.click(filterButton);

  // Filter
  await screen.findByRole('heading', {
    name: /Qual o valor da prestação que você deseja pagar?/i,
  });
  // Filter: default
  userEvent.type(screen.getByLabelText(/Valor da prestação/i), '12345');
  userEvent.click(screen.getByText('12 meses'));
  // Filter: custom
  userEvent.click(screen.getByText(/personalizado/i));
  userEvent.type(await screen.findByLabelText(/Prazo em meses/i), '100');
  // TODO: Filter: range

  const finishFilterButton = screen.getByRole('button', { name: /continuar/i });
  userEvent.click(finishFilterButton);

  // Results step
  await screen.findByRole('heading', {
    name: /Resultados dos prazos disponíveis para você/i,
  });

  expect(spy).toHaveBeenCalledWith({
    contratosRefinanciamento: [
      { contrato: '******1234' },
      { contrato: '******5678' },
    ],
    idConvenio: 1,
    prazos: [12, 100],
    prestacao: 12345,
    retornarSomenteOperacoesViaveis: true,
  });
});
