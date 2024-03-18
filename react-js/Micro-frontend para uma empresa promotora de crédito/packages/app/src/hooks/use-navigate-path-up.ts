import { useHistory } from 'react-router-dom';

export const useNavigatePathUp = (): (() => void) => {
  const history = useHistory();

  const defaultOnClick = (): void => {
    const index = history.location.pathname.lastIndexOf('/');
    const nextRoute = history.location.pathname.slice(0, index);
    history.push(nextRoute);
  };

  return defaultOnClick;
};
