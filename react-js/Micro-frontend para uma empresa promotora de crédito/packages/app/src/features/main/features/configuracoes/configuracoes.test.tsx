import { render, screen, waitFor } from '@testing-library/react';
import userEvent from '@testing-library/user-event';

import { NavBar } from 'features/main/components';

export const configNavigation = (
  routeLabel: string | RegExp | null = null,
): void => {
  const configsButton = screen.getByLabelText('configs');
  userEvent.click(configsButton);

  if (routeLabel) {
    const menuButton = screen.getByRole('button', { name: routeLabel });
    userEvent.click(menuButton);
  }
};

jest.mock('app/auth/auth.context', () => ({
  __esModule: true,
  useAuthContext: () => ({ onLogout: jest.fn() }),
}));

xtest('Open configs drawer', async () => {
  const { getByText } = render(<NavBar />);

  configNavigation();

  const configsTitle = getByText(/Configurações/i);
  const optionsSample = getByText(/redefinir minha senha/i);

  expect(configsTitle).toBeInTheDocument();
  expect(optionsSample).toBeInTheDocument();

  const closeButton = screen.getByLabelText('close');
  userEvent.click(closeButton);

  await waitFor(() => {
    expect(configsTitle).not.toBeInTheDocument();
    expect(optionsSample).not.toBeInTheDocument();
  });
});
