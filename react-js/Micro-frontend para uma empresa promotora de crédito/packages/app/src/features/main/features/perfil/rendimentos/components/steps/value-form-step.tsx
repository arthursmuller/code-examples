import { FC } from 'react';

import {
  BemErrorBoundary,
  ModalProvider,
  useStepsContainerContext,
} from '@pcf/design-system';

import { MatriculaBaseStep } from './matricula-base-step';

import { MatriculaSiapeFormModel } from '../../models/matricula-siape-form.model';
import { ValueForm } from '../form-parts';
import { usePersistMatricula } from '../use-persist-matricula';
import { MatriculaInssFormModel } from '../../models/matricula-inss-form.model';

const ValueFormStepContent: FC = () => {
  const { data, finish } = useStepsContainerContext<
    MatriculaSiapeFormModel | MatriculaInssFormModel
  >();

  const { onSubmit, isSubmiting } = usePersistMatricula(
    { tipoConvenio: data?.tipoConvenio },
    undefined,
    undefined,
    finish,
  );

  return (
    <BemErrorBoundary>
      <MatriculaBaseStep<MatriculaSiapeFormModel | MatriculaSiapeFormModel>
        renderForm={(props) => <ValueForm {...props} isCreating />}
        onSubmit={onSubmit}
        isSubmiting={isSubmiting}
      />
    </BemErrorBoundary>
  );
};

export const ValueFormStep: FC = () => (
  <ModalProvider>
    <ValueFormStepContent />
  </ModalProvider>
);
