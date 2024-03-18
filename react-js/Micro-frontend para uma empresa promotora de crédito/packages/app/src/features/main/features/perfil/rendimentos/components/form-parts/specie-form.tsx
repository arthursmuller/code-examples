import { FC } from 'react';

import { GridItem } from '@chakra-ui/react';

import { FormItemControl } from '@pcf/design-system';

import { FormPartProps } from '.';

import {
  MatriculaInssFormModel,
  MatriculaInssFormModelKeys,
  MatriculaInssFormModelKeys as areas,
} from '../../models/matricula-inss-form.model';
import { SelectEspecieInss } from '../selects';

export const SpecieForm: FC<FormPartProps<MatriculaInssFormModel>> = ({
  initialData,
  errors,
  control,
  hasGridAreas,
}) => (
  <GridItem gridArea={hasGridAreas ? areas.inssEspecieBeneficio : 'unset'}>
    <FormItemControl
      label="Espécie"
      name={MatriculaInssFormModelKeys.inssEspecieBeneficio}
      defaultValue={initialData?.inssEspecieBeneficio}
      errorMessage={errors?.inssEspecieBeneficio?.message}
      control={control}
      as={SelectEspecieInss}
      required
    />
  </GridItem>
);
