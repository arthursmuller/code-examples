import { FC, useState } from 'react';

import { Redirect, useHistory, useParams } from 'react-router-dom';

import { Loader, StepsContainer, useStepState } from '@pcf/design-system';
import { useRecuperarSenhaQuery, AutenticacaoModel } from '@pcf/core';
import { PublicRoutes as PublicRoutesEnum } from 'app/routes/public/public-routes.enum';

import { NewPasswordSuccessStep } from './components/new-password-success-step';
import { NewPasswordStep } from './components/new-password-step';

const NewPassword: FC = () => {
  const [autenticacaoModel, setAutenticaoModel] = useState<AutenticacaoModel>();
  const { stepNumber, nextStep } = useStepState();
  const history = useHistory();
  const { token } = useParams<{ token: string }>();
  const { isLoading, data, isError } = useRecuperarSenhaQuery(token);

  const handleNextStep = (model: AutenticacaoModel): void => {
    nextStep();
    setAutenticaoModel(model);
  };

  if (isLoading) {
    return <Loader fullHeight />;
  }

  if ((!isLoading && !data) || isError) {
    return <Redirect to={PublicRoutesEnum.PasswordRecovery} />;
  }

  return (
    <StepsContainer
      onBack={() => history.goBack()}
      showBackButton={stepNumber === 1}
      stepNumber={stepNumber}
    >
      <NewPasswordStep onNextStep={handleNextStep} />
      <NewPasswordSuccessStep autenticacaoModel={autenticacaoModel} />
    </StepsContainer>
  );
};

export default NewPassword;
