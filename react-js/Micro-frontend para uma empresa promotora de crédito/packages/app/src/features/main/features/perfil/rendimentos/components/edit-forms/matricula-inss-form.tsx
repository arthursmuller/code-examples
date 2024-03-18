import { FC } from 'react';

import { Divider, useBreakpointValue } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import { EditMatriculaBaseForm } from './edit-matricula-base-form';

import { MatriculaInssFormModel } from '../../models/matricula-inss-form.model';
import { FormGrid } from '../form-grid';
import {
  BankAccountForm,
  SpecieForm,
  ValueForm,
  InssRegisterForm,
  desktopValueGridTemplate,
  desktopBankAccountGridTemplate,
  desktopInssRegisterGridTemplate,
} from '../form-parts';
import { BaseEditMatriculaProps } from '../../models/base-edit-matricula-props';

interface MatriculaInssFormProps extends BaseEditMatriculaProps {
  initialData: MatriculaInssFormModel;
  matriculaId?: number;
}

export const MatriculaInssForm: FC<MatriculaInssFormProps> = ({
  initialData,
  matriculaId,
  onSuccess,
  useUnloadPrompt,
}) => {
  const {
    handleSubmit,
    control,
    formState: { errors, isDirty },
    reset,
  } = useForm<MatriculaInssFormModel>({ defaultValues: initialData });
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  return (
    <EditMatriculaBaseForm
      formHandle={handleSubmit}
      matricula={initialData}
      matriculaId={matriculaId}
      title="Atualização de Matrícula INSS"
      isDirty={isDirty}
      onSuccess={() => {
        reset({}, { keepValues: true });
        onSuccess && onSuccess();
      }}
      useUnloadPrompt={useUnloadPrompt}
    >
      <FormGrid
        gridTemplateAreas={
          !isMobile ? desktopInssRegisterGridTemplate : 'unset'
        }
      >
        <InssRegisterForm
          control={control}
          errors={errors}
          initialData={initialData}
          hasGridAreas={!isMobile}
        />
      </FormGrid>

      <Divider borderColor="grey.300" marginBottom="8px" />

      <FormGrid
        gridTemplateAreas={!isMobile ? desktopBankAccountGridTemplate : 'unset'}
      >
        <BankAccountForm
          control={control}
          errors={errors}
          initialData={initialData}
          hasGridAreas={!isMobile}
        />

        <SpecieForm
          control={control}
          errors={errors}
          initialData={initialData}
          hasGridAreas={!isMobile}
        />
      </FormGrid>

      <Divider borderColor="grey.300" marginBottom="8px" />

      <FormGrid
        gridTemplateAreas={!isMobile ? desktopValueGridTemplate : 'unset'}
      >
        <ValueForm
          control={control}
          errors={errors}
          initialData={initialData}
          hasGridAreas={!isMobile}
        />
      </FormGrid>
    </EditMatriculaBaseForm>
  );
};
