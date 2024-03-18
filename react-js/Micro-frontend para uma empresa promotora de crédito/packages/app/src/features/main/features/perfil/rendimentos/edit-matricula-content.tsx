import { FC, useState } from 'react';

import { useHistory } from 'react-router-dom';
import { useMount } from 'react-use';

import { useQuickToast } from '@pcf/design-system';
import { RendimentoResponseModel } from '@pcf/core';

import { MatriculaInssForm, MatriculaSiapeForm } from './components/edit-forms';
import { toInssFormModal, toSiapeFormModel } from './utils';
import { BaseEditMatriculaProps } from './models/base-edit-matricula-props';

import { PerfilRoutesPaths } from '../perfil.routes.enum';

export interface EditMatriculaContentProps extends BaseEditMatriculaProps {
  rendimentos: RendimentoResponseModel[];
  rendimentoId: number;
}

export const EditMatriculaContent: FC<EditMatriculaContentProps> = ({
  rendimentos,
  rendimentoId,
  ...rest
}) => {
  const history = useHistory();
  const toast = useQuickToast();
  const [Form, setForm] = useState<FC>();

  useMount(() => {
    const data = rendimentos?.find((r) => r.id === rendimentoId);

    if (!data) {
      toast(
        'Endereço inválido',
        'Sem problema!, redirecionandos você para sua lista de matrículas.',
      );
      history.push(PerfilRoutesPaths.rendimentos);
    } else {
      const isSiape = data?.convenio?.nome?.split(' ')[0] === 'SIAPE';

      if (isSiape) {
        const siapeFormData = toSiapeFormModel(data);
        setForm(() => () => (
          <MatriculaSiapeForm
            initialData={siapeFormData}
            matriculaId={data.id}
            {...rest}
          />
        ));
      } else {
        const inssFormData = toInssFormModal(data);
        setForm(() => () => (
          <MatriculaInssForm
            initialData={inssFormData}
            matriculaId={data.id}
            {...rest}
          />
        ));
      }
    }
  });

  return Form ? <Form /> : <div />;
};
