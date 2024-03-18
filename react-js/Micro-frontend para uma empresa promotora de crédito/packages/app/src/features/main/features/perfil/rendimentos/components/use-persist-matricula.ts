import { useCallback } from 'react';

import {
  useRendimentoMutation,
  extractReadableErrorMessage,
} from '@pcf/core';
import { useModal, getDefaultErrorModalConfig } from '@pcf/design-system';

import { toRequestModel } from '../utils/matricula-to-model';
import { MatriculaSiapeFormModel } from '../models/matricula-siape-form.model';
import { MatriculaInssFormModel } from '../models/matricula-inss-form.model';

export const usePersistMatricula = (
  extraData: Partial<MatriculaSiapeFormModel | MatriculaInssFormModel> = {},
  matriculaId?: number,
  onSuccess?: () => void,
  onClose?: () => void,
): {
  onSubmit: (
    formData: MatriculaSiapeFormModel | MatriculaInssFormModel,
  ) => void;
  isSubmiting: boolean;
} => {
  const { showModal } = useModal();

  const { mutate, isLoading: isSubmiting } = useRendimentoMutation(matriculaId);

  const onSubmit = useCallback(
    (formData: MatriculaSiapeFormModel | MatriculaInssFormModel): void => {
      const request = toRequestModel({
        ...formData,
        ...extraData,
      });

      mutate(request, {
        onSuccess() {
          onSuccess && onSuccess();

          showModal({
            title: 'Matrícula salva com sucesso!',
            closeOnClickOverlay: true,
            closeText: 'Voltar para lista',
            onClose: () => onClose && onClose(),
          });
        },
        onError(error) {
          showModal(
            getDefaultErrorModalConfig({
              information: extractReadableErrorMessage(error),
            }),
          );
        },
      });
    },
    [extraData, mutate, onClose], // eslint-disable-line
  );

  return { onSubmit, isSubmiting };
};
