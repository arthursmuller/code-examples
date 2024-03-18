import { FC } from 'react';

import { BemErrorBoundary } from '@pcf/design-system';

import { MatriculaBaseStep } from '.';

import { MatriculaSiapeFormModel } from '../../models/matricula-siape-form.model';
import { InstitutingForm } from '../form-parts';

export const InstitutingFormStep: FC = () => (
  <BemErrorBoundary>
    <MatriculaBaseStep<MatriculaSiapeFormModel> as={InstitutingForm} />
  </BemErrorBoundary>
);
