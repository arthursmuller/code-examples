import { BemErrorFallbackMessageModel } from './bem-error-message.model';

export const loadFailMessages: BemErrorFallbackMessageModel = {
  title: 'Ops! Não conseguimos carregar esses dados',
  description:
    'Logo corrigiremos esse erro. Enquanto isso, recarregue a página ou tente novamente mais tarde.',
  buttonLabel: 'Recarregar',
};

export const alertMessages: BemErrorFallbackMessageModel = {
  title: 'Informação',
  description: '',
  buttonLabel: '',
};

export const requestFailMessages: BemErrorFallbackMessageModel = {
  title: 'Ops! Houve um erro inesperado',
  description:
    'Nossa equipe já recebeu esse erro e logo será corrigido. Por favor, tente novamente ou, se problema persistir, tente mais tarde.',
  buttonLabel: 'Tentar novamente',
};
