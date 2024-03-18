import '@testing-library/jest-dom';
import '@testing-library/jest-dom/extend-expect';
import { MockedRequest, ResponseComposition, RestContext } from 'msw/lib/types';

jest.mock('print-build-info', () => ({
  __esModule: true,
  default: () => null,
}));

jest.mock('react-query/devtools', () => ({
  __esModule: true,
  default: () => null,
  ReactQueryDevtools: () => <div />,
}));

jest.mock('react-use/lib/useGeolocation', () => ({
  __esModule: true,
  default: () => {
    return { latitude: '0', longitude: '0' };
  },
}));

global.scrollTo = jest.fn();

export const pcfApi = (path: string): string => {
  return new URL(path, process.env.REACT_APP_PLATAFORMA_CLIENTE_API).toString();
};

export const pcfApiResponse =
  (response, spy: ((body) => void) | null = null) =>
  (req: MockedRequest, res: ResponseComposition, ctx: RestContext) => {
    spy && spy(req.body);
    return res(
      ctx.status(202, 'Mocked status'),
      ctx.json({ retorno: response }),
    );
  };

const noop = (): void => {};

Object.defineProperty(window, 'matchMedia', {
  writable: true,
  value: (query: any) => {
    return {
      matches: false,
      media: query,
      onchange: null,
      addListener: noop, // deprecated
      removeListener: noop, // deprecated
      addEventListener: noop,
      removeEventListener: noop,
      dispatchEvent: noop,
    };
  },
});
