import { FC } from 'react';

import { Button } from '@chakra-ui/react';

import { InactivityLoginDialog, LoginDialogProps } from './login-dialog';
import { LoginProps, LoginStepper } from './login-stepper';
import { LoginPopover } from '.';

import { ModalProvider, useModal } from '../modal';

export default {
  title: 'Login',
  component: LoginStepper,
};

const Default: FC<LoginProps> = (props) => (
  <LoginStepper
    onGoogleLoginSuccess={() => {
      console.log('sucesso');
    }}
    onSubmit={(data) => console.log(data)}
    {...props}
  />
);

export const Steps = Default.bind({});
Steps.args = {
  onPrevious: () => console.log('went back'),
};

const Dialog: FC<LoginDialogProps> = (props) => {
  const { showModal } = useModal();

  const onClick = (): void => {
    showModal({
      modal: () => (
        <InactivityLoginDialog
          onSubmit={(data) => alert(`${data.email} - ${data.password}`)}
          {...props}
        />
      ),
    });
  };

  return <Button onClick={onClick}>Open login</Button>;
};

export const AsDialog: FC<LoginDialogProps> = (props) => (
  <ModalProvider>
    <Dialog {...props} />
  </ModalProvider>
);

export const AsPopover: FC<LoginDialogProps> = () => (
  <LoginPopover
    onCreateAccountClick={() => {}}
    showSocialMediaLoginButton
    showCreateAccountButton
    onGoogleLoginSuccess={() => {}}
    onFacebookLoginSuccess={() => {}}
    onAppleLoginSuccess={() => {}}
    onSubmit={() => {
      alert('submit');
    }}
  />
);
