import { authLoginSuccess, authLogout } from './auth';

const storageKey = 'store-datamob';

export const persister =
  ({ getState }) =>
  (next) =>
  (action) => {
    const result = next(action);

    if ([authLoginSuccess.type, authLogout.type].includes(action.type)) {
      const { auth } = getState();
      localStorage.setItem(storageKey, JSON.stringify({ auth }));
    }

    return result;
  };

export const rehydrate = () => {
  const persisted = localStorage.getItem(storageKey);
  return (persisted && JSON.parse(persisted)) || {};
};
