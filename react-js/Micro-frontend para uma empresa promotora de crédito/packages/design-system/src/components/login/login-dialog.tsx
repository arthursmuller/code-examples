import { FC } from 'react';

import { ClockIcon } from '@pcf/design-system-icons';

import { EmailLoginForm, EmailLoginFormProps } from './components/email';

import { DefaultModal } from '../modal/components';

export interface LoginDialogProps extends EmailLoginFormProps {
  icon: FC;
  title: string;
}

const LoginDialog: FC<LoginDialogProps> = ({ icon, title, ...rest }) => (
  <DefaultModal.Container maxWidth="392px" maxHeight="auto">
    <DefaultModal.Header
      icon={ClockIcon}
      iconBg="secondary.regular"
      bg="secondary.regular"
      title={title}
      color="white"
      textStyle="bold20"
    />

    <EmailLoginForm {...rest} />
  </DefaultModal.Container>
);

const InactivityLoginDialog: FC<
  Omit<EmailLoginFormProps, 'navigateToRecoverPassword'>
> = (props) => (
  <LoginDialog
    title="Devido à inatividade no seu perfil, faça login novamente para continuar"
    icon={ClockIcon}
    navigateToRecoverPassword={null}
    {...props}
  />
);

export { LoginDialog, InactivityLoginDialog };
