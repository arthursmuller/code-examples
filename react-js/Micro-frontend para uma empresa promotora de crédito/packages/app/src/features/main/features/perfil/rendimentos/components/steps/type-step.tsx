import { FC } from 'react';

import { TipoConsignadoForm } from 'features/main/components';

import { MatriculaBaseStep } from './matricula-base-step';

import { MatriculaSiapeFormModel } from '../../models/matricula-siape-form.model';

export const TypeFormStep: FC = () => (
  <MatriculaBaseStep<MatriculaSiapeFormModel | MatriculaSiapeFormModel>
    renderForm={(props) => <TipoConsignadoForm {...props} />}
    customTitle="Cadastrar rendimento"
  />
);
