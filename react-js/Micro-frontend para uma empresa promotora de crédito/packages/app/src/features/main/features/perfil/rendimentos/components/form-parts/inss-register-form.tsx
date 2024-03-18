import { FC } from 'react';

import { GridItem } from '@chakra-ui/react';

import {
  FormItemControl,
  BemDateInput,
  BemTextInput,
} from '@pcf/design-system';

import { FormPartProps } from '.';

import {
  MatriculaInssFormModel,
  MatriculaInssFormModelKeys,
  MatriculaInssFormModelKeys as areas,
} from '../../models/matricula-inss-form.model';
import { SelectUnidadesFederativas } from '../../../components';

export const desktopInssRegisterGridTemplate = `'${areas.matricula} ${areas.matricula} ${areas.dataInscricaoBeneficio} ${areas.uf}'`;

export const InssRegisterForm: FC<FormPartProps<MatriculaInssFormModel>> = ({
  initialData,
  errors,
  control,
  hasGridAreas,
}) => (
  <>
    <GridItem gridArea={hasGridAreas ? areas.matricula : 'unset'}>
      <FormItemControl
        label="Matrícula"
        name={MatriculaInssFormModelKeys.matricula}
        defaultValue={initialData?.matricula}
        errorMessage={errors?.matricula?.message}
        control={control}
        as={BemTextInput}
        type="number"
        rules={{
          validate: {
            length(matricula: string) {
              if (matricula && matricula.length !== 10) {
                return 'Matrícula inválida';
              }
              return true;
            },
          },
        }}
        required
      />
    </GridItem>
    <GridItem gridArea={hasGridAreas ? areas.dataInscricaoBeneficio : 'unset'}>
      <FormItemControl
        label="Data Inscrição Benefício"
        name={MatriculaInssFormModelKeys.dataInscricaoBeneficio}
        defaultValue={initialData?.dataInscricaoBeneficio}
        errorMessage={errors?.dataInscricaoBeneficio?.message}
        control={control}
        as={BemDateInput}
        required
      />
    </GridItem>
    <GridItem gridArea={hasGridAreas ? areas.uf : 'unset'}>
      <FormItemControl
        label="UF"
        name={MatriculaInssFormModelKeys.uf}
        defaultValue={initialData?.uf}
        errorMessage={errors?.uf?.message}
        required
        control={control}
        as={SelectUnidadesFederativas}
      />
    </GridItem>
  </>
);
