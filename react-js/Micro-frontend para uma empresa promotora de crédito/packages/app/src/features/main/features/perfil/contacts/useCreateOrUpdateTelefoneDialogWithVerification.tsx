import React, { FC } from 'react';

import { isValidMobilePhone } from '@brazilian-utils/brazilian-utils';

import {
  StepsActionDialogComp,
  StepsContainerProvider,
  TelefoneClienteExibicaoModel,
  useModal,
  useStepsContainerContext,
} from '@pcf/design-system';
import { FeatureToggleContextProvider } from 'app';

import {
  PhoneForm,
  TelefoneFormData,
} from './components/phone-verification-steps/phone-form';
import { PhoneTypeIdentification } from './components/phone-verification-steps/phone-type-identification';
import { PhoneVerificationOptions } from './components/phone-verification-steps/phone-verification-options';
import { PhoneVerificationCode } from './components/phone-verification-steps/phone-verification-code';

interface FormModalTelefoneProps {
  phone?: TelefoneClienteExibicaoModel;
}

const StepsTelefone: FC<FormModalTelefoneProps> = ({ phone }) => {
  const { data } = useStepsContainerContext<TelefoneFormData>();

  return (
    <StepsActionDialogComp>
      <PhoneForm phone={phone} />
      <PhoneTypeIdentification />
      {isValidMobilePhone(data.phone) && <PhoneVerificationOptions />}
      <PhoneVerificationCode />
    </StepsActionDialogComp>
  );
};

export const useCreateOrUpdateTelefoneDialogWithVerification = (): {
  open: (phone?: TelefoneClienteExibicaoModel) => void;
} => {
  const { showModal, hideModal } = useModal();

  function open(phone?: TelefoneClienteExibicaoModel): void {
    showModal({
      closeOnClickOverlay: false,
      modal: (
        <FeatureToggleContextProvider>
          <StepsContainerProvider onCloseCb={hideModal}>
            <StepsTelefone phone={phone} />
          </StepsContainerProvider>
        </FeatureToggleContextProvider>
      ),
    });
  }

  return { open };
};
