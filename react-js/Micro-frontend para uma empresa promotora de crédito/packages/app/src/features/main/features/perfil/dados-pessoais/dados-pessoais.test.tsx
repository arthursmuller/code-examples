import { rest } from 'msw';
import { setupServer } from 'msw/node';
import {
  render,
  screen,
  waitFor,
  waitForElementToBeRemoved,
} from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { MemoryRouter as Router } from 'react-router-dom';

import {
  ClienteExibicaoModel,
  CLIENT_QUERY_ENDPOINT,
  GENDER_QUERY_ENDPOINT,
  CIVIL_STATE_QUERY_ENDPOINT,
  GRAU_INSTRUCAO_QUERY_ENDPOINT,
  STATES_QUERY_ENDPOINT,
  citiesQueryEndpointGen,
  CLIENT_MUTATION_ENDPOINT,
} from '@pcf/core';
import { ModalProvider } from '@pcf/design-system';
import { pcfApi, pcfApiResponse } from 'setupTests';

import { DadosPessoais } from '.';

const mockedOpts = {
  gender: [
    {
      descricao: 'MASCULINO',
      id: 2,
      sigla: 'M',
    },
    {
      descricao: 'FEMININO',
      id: 1,
      sigla: 'F',
    },
  ],
  civilState: [
    {
      descricao: 'VIUVO',
      id: 5,
      sigla: 'V',
    },
  ],
  levelofSchooling: [
    {
      descricao: 'SUPERIOR COMPLETO',
      id: 5,
    },
  ],
  states: [
    {
      id: 1,
      nome: 'Paraíba',
      sigla: 'PB',
    },
  ],
  cities: [
    {
      descricao: 'POA',
      id: 1,
      uf: {
        id: 1,
        nome: 'Paraíba',
        sigla: 'PB',
      },
    },
  ],
};

const mockedClient: ClienteExibicaoModel = {
  id: 1,
  nome: 'Unit, the Test',
  cpf: '562.556.334-97',
  dataNascimento: '2000-03-12T00:00:00',
  filiacao1: 'Billy, the Parent',
  filiacao2: 'Buba, the Parent',
  deficienteVisual: false,
  email: 'placeholder@todo.todo',
  genero: mockedOpts.gender[0],
  estadoCivil: mockedOpts.civilState[0],
  grauInstrucao: mockedOpts.levelofSchooling[0],
  cidadeNatal: mockedOpts.cities[0],
};

const spy = jest.fn();

const server = setupServer(
  rest.get<ClienteExibicaoModel>(
    pcfApi(GENDER_QUERY_ENDPOINT),
    pcfApiResponse(mockedOpts.gender),
  ),
  rest.get<ClienteExibicaoModel>(
    pcfApi(CIVIL_STATE_QUERY_ENDPOINT),
    pcfApiResponse(mockedOpts.civilState),
  ),
  rest.get<ClienteExibicaoModel>(
    pcfApi(GRAU_INSTRUCAO_QUERY_ENDPOINT),
    pcfApiResponse(mockedOpts.levelofSchooling),
  ),
  rest.get<ClienteExibicaoModel>(
    pcfApi(STATES_QUERY_ENDPOINT),
    pcfApiResponse(mockedOpts.states),
  ),
  rest.get<ClienteExibicaoModel>(
    pcfApi(citiesQueryEndpointGen('1')),
    pcfApiResponse(mockedOpts.cities),
  ),
  rest.get<ClienteExibicaoModel>(
    pcfApi(CLIENT_QUERY_ENDPOINT),
    pcfApiResponse(mockedClient),
  ),
  rest.put(pcfApi(CLIENT_MUTATION_ENDPOINT), (req, res, ctx) => {
    spy(req.body);
    return res(ctx.status(200), ctx.json({ retorno: mockedClient }));
  }),
);

beforeAll(() => server.listen());

afterEach(() => server.resetHandlers());

afterAll(() => {
  server.close();
  jest.clearAllMocks();
});

test('Should load information into fields', async () => {
  render(
    <Router>
      <DadosPessoais />
    </Router>,
  );

  await waitForElementToBeRemoved(() => screen.queryByTestId('loader'));

  const saveButton = screen.getByText(/salvar/i);
  expect(saveButton).toBeVisible();
  expect(saveButton).not.toBeDisabled();
});

test('Should persist changes', async () => {
  render(
    <ModalProvider>
      <Router>
        <DadosPessoais />
      </Router>
    </ModalProvider>,
  );

  const textTestField = await screen.findByLabelText(/Nome completo/i);
  const nextClientName = 'Zed, the Evolved Test';
  userEvent.type(textTestField, `{selectall}{del}${nextClientName}`);

  // TODO: fix select label ref and click

  // const selectTestField = screen.getByText(/Sexo/i);
  // userEvent.click(selectTestField);
  // const nextGender = mockedOpts.gender[1];
  // const opt = screen.getByText(nextGender.descricao);
  // userEvent.click(opt);

  userEvent.click(screen.getByText(/salvar/i));

  await waitFor(() => {
    expect(spy).toHaveBeenCalledWith({
      idGenero: mockedClient.genero?.id,
      idEstadoCivil: mockedClient.estadoCivil?.id,
      idGrauInstrucao: mockedClient.grauInstrucao?.id,
      idCidadeNatal: mockedClient.cidadeNatal?.id,
      nome: nextClientName,
      dataNascimento: '2000-03-12',
      filiacao1: mockedClient.filiacao1,
      filiacao2: mockedClient.filiacao2,
      deficienteVisual: mockedClient.deficienteVisual,
      email: mockedClient.email,
    });

    expect(screen.getByText(/Dados Atualizados/i)).toBeVisible();
  });
});

test('Should block user from leaving page with unsaved changes', async () => {
  const navigationConfirmSpy = jest.fn(() => false);

  render(
    <ModalProvider>
      <Router getUserConfirmation={navigationConfirmSpy}>
        <DadosPessoais />
      </Router>
    </ModalProvider>,
  );

  const field = await screen.findByLabelText(/Nome completo/i);

  userEvent.type(field, 'aaa');

  userEvent.click(screen.getByText(/voltar/i));

  await waitFor(() => {
    expect(navigationConfirmSpy).toBeCalledTimes(1);
  });

  userEvent.type(
    screen.getByLabelText(/Nome completo/i),
    '{backspace}{backspace}{backspace}',
  );

  userEvent.click(screen.getByText(/voltar/i));

  expect(navigationConfirmSpy).toBeCalledTimes(1);
});

// TODO: when able to get select fields:

// test if city clear when UF is changed, test if button is disabled, if fulfill, test if button is enabled
