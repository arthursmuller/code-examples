import { FC } from 'react';

import { Divider, useBreakpointValue } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import { EditMatriculaBaseForm } from './edit-matricula-base-form';

import { MatriculaSiapeFormModel } from '../../models/matricula-siape-form.model';
import { FormGrid } from '../form-grid';
import {
  BankAccountForm,
  desktopBankAccountGridTemplate,
  desktopInstitutingGridTemplate,
  desktopSiapeRegisterGridTemplate,
  desktopValueGridTemplate,
  InstitutingForm,
  SiapeRegisterForm,
  ValueForm,
} from '../form-parts';
import { BaseEditMatriculaProps } from '../../models/base-edit-matricula-props';

interface MatriculaSiapeFormProps extends BaseEditMatriculaProps {
  initialData: MatriculaSiapeFormModel;
  matriculaId?: number;
}

export const MatriculaSiapeForm: FC<MatriculaSiapeFormProps> = ({
  initialData,
  matriculaId,
  onSuccess,
  useUnloadPrompt,
}) => {
  const {
    handleSubmit,
    control,
    watch,
    formState: { errors, isDirty },
    reset,
    unregister,
  } = useForm<MatriculaSiapeFormModel>({ defaultValues: initialData });
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  return (
    <EditMatriculaBaseForm
      formHandle={handleSubmit}
      matricula={initialData}
      matriculaId={matriculaId}
      title="Atualização de Matrícula SIAPE"
      isDirty={isDirty}
      onSuccess={() => {
        reset({}, { keepValues: true });
        onSuccess && onSuccess();
      }}
      useUnloadPrompt={useUnloadPrompt}
    >
      <FormGrid
        gridTemplateAreas={
          !isMobile ? desktopSiapeRegisterGridTemplate : 'unset'
        }
      >
        <SiapeRegisterForm
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
      </FormGrid>

      <Divider borderColor="grey.300" marginBottom="8px" />

      <FormGrid
        gridTemplateAreas={!isMobile ? desktopInstitutingGridTemplate : 'unset'}
      >
        <InstitutingForm
          control={control}
          errors={errors}
          watch={watch}
          initialData={initialData}
          hasGridAreas={!isMobile}
          unregister={unregister}
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
