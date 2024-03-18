import { rest } from 'msw';
import { setupServer } from 'msw/node';
import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';

import { ModalProvider } from '@pcf/design-system';
import {
  Anexo,
  ANEXOS_QUERY_ENDPOINT,
  ANEXO_ENDPOINT,
  DocumentTypeCode,
} from '@pcf/core';
import { pcfApi, pcfApiResponse } from 'setupTests';

import { Documentos } from './documentos';

jest.mock('react-webcam', () => {
  const React = require('react');

  const MockCam = React.forwardRef((props, ref) => {
    const getScreenshot = (): string => {
      return 'data:image/jpeg;base64,R0lGODlhAQABAAAAACwAAAAAAQABAAA=';
    };

    ref.current = { getScreenshot }; // eslint-disable-line
    return <div id="mock-cam" />;
  });

  return MockCam;
});

const spyPost = jest.fn();
const spyFetch = jest.fn();

const mockedAttachment: Anexo = {
  dataCadastro: '2021-03-29T15:19:06.3414807',
  id: 28,
  linkAnexo:
    'https://sthmlbrzapp.blob.core.windows.net/plataformaclientefinal/plataformaclientefinal%2F2021%2F03%2F29%2F15%2F19%2F4dc1eae5-bafe-490b-9b89-00b90eb7610b.webp',
  tipoDocumento: {
    id: 8,
    nome: 'PASSAPORTE BRASILEIRO',
    codigo: DocumentTypeCode.PassaporteBrasileiro,
  },
  extensao: '.webp'
};

const server = setupServer(
  rest.get<Anexo[]>(
    pcfApi(ANEXOS_QUERY_ENDPOINT),
    pcfApiResponse([mockedAttachment], spyFetch),
  ),
  rest.post(pcfApi(ANEXO_ENDPOINT), pcfApiResponse(null, spyPost)),
);

beforeAll(() => server.listen());

afterEach(() => server.resetHandlers());

afterAll(() => {
  server.close();
  jest.clearAllMocks();
});

xtest('List and delete document', async () => {
  render(
    <ModalProvider>
      <Documentos />
    </ModalProvider>,
  );

  await screen.findByRole('heading', { name: /documento de identificação/i });

  expect(screen.getByText(/aprovado/i)).toBeVisible();
  expect(screen.getByText('29/03/2021')).toBeVisible();
  expect(screen.getByRole('link', { name: /visualizar/i })).toBeVisible();

  const deleteButton = screen.getByRole('button', { name: /excluir/i });
  userEvent.click(deleteButton);

  const proceedButton = await screen.findByRole('button', {
    name: /sim, quero excluir/i,
  });
  userEvent.click(proceedButton);

  await waitFor(() => {
    expect(spyFetch).toHaveBeenCalledTimes(2);
  });
});

test('Persist document', async () => {
  render(
    <ModalProvider>
      <Documentos />
    </ModalProvider>,
  );

  const attachButton = (
    await screen.findAllByRole('button', { name: /anexar documento/i })
  )[0];
  userEvent.click(attachButton);

  let nextButton = await screen.findByRole('button', { name: /entendi/i });
  userEvent.click(nextButton);

  await screen.findByText(/Etapa 2 de 4/i);
  nextButton = await screen.findByRole('button', { name: /entendi/i });
  userEvent.click(nextButton);

  nextButton = await screen.findByRole('button', { name: /tirar foto/i });
  userEvent.click(nextButton);

  await screen.findByText(/Tirar foto do Comprovante/i);
  const [, secondButton] = await screen.findAllByRole('button', {
    name: /tirar foto/i,
  });
  nextButton = secondButton;
  userEvent.click(nextButton);

  nextButton = await screen.findByRole('button', { name: /cadastrar/i });
  userEvent.click(nextButton);

  waitFor(async () => {
    nextButton = await screen.findByRole('button', { name: /fechar/i });
    userEvent.click(nextButton);
  });

  waitFor(async () => {
    expect(spyPost).toHaveBeenCalledWith({
      codigoTipo: DocumentTypeCode.ComprovanteDeResidencia,
      idUsuario: -1,
      anexoBase64: 'R0lGODlhAQABAAAAACwAAAAAAQABAAA=',
      extensao: 'image/jpeg',
    });
  });
});
