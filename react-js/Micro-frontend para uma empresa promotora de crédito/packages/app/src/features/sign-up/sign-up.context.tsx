import { createContext, FC, useContext, useState } from 'react';

import { useStepState, useStateCallback } from '@pcf/design-system';

import { UserData, UserInfo, UserSecurityInfo } from './models/user-info.model';

interface SignUpContextProps {
  onPrevious?: () => void;
}

type SignUpContextData = {
  currentStep: number;
  onBack(): void;
  onForward(): void;
  setCanGoForward(next: boolean): void;
  canGoForward: boolean;
  setFormData(userData: Partial<UserData>): void;
  socialEmail: string | null;
  setSocialEmail(email: string | null): void;

  setSubmit(formResponse): void;
  formData: UserData;
};

const defaultData: UserData = {
  name: '',
  surname: '',
  cpf: '',
  phone: '',
  email: '',
  password: '',
  repeatPassword: '',
};

const SignUpContext = createContext<SignUpContextData>({} as SignUpContextData);

const SignUpContextProvider: FC<SignUpContextProps> = ({
  children,
  onPrevious,
}) => {
  const { stepNumber, previousStep, nextStep } = useStepState(
    undefined,
    undefined,
    onPrevious,
  );
  const [socialEmail, setSocialEmail] = useState<string | null>();

  const { state: formData, setStateCallback: setFormData } =
    useStateCallback<UserData>(defaultData);
  const [canGoForward, setCanGoForward] = useState<boolean>(false);
  const [submit, setSubmit] = useState<() => void>(() => null);

  function updateFormData(data: UserSecurityInfo | UserInfo): void {
    setFormData({ ...formData, ...data }, nextStep);
  }

  function setOnForward(formResponse): void {
    setSubmit(() => formResponse && formResponse(updateFormData));
  }

  return (
    <SignUpContext.Provider
      value={{
        formData,
        currentStep: stepNumber,
        onBack: previousStep,
        canGoForward,
        setCanGoForward,
        onForward: submit,
        setSubmit: setOnForward,
        setFormData,
        socialEmail,
        setSocialEmail,
      }}
    >
      {children}
    </SignUpContext.Provider>
  );
};

function useSignUpContext(): SignUpContextData {
  const context = useContext(SignUpContext);
  if (!context)
    throw new Error(
      'useSignUpContext must be used within SignUpContextProvider',
    );
  return context;
}

export { SignUpContextProvider, useSignUpContext };
