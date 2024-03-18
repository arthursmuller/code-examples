import { FC } from 'react';

import { BemErrorBoundary } from '@pcf/design-system';

import { MatriculaBaseStep } from './matricula-base-step';

import { MatriculaSiapeFormModel } from '../../models/matricula-siape-form.model';
import { InssRegisterForm, SiapeRegisterForm } from '../form-parts';
import { MatriculaInssFormModel } from '../../models/matricula-inss-form.model';

export const RegisterSiapeFormStep: FC = () => (
  <BemErrorBoundary>
    <MatriculaBaseStep<MatriculaSiapeFormModel> as={SiapeRegisterForm} />
  </BemErrorBoundary>
);

export const RegisterInssFormStep: FC = () => (
  <BemErrorBoundary>
    <MatriculaBaseStep<MatriculaInssFormModel> as={InssRegisterForm} />
  </BemErrorBoundary>
);
