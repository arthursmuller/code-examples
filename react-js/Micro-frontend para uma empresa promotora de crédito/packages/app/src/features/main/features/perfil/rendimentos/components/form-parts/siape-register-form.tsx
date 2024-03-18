import { FC } from 'react';

import { GridItem } from '@chakra-ui/react';

import {
  BemDateInput,
  BemTextInput,
  FormItemControl,
} from '@pcf/design-system';

import { FormPartProps } from '.';

import { SelectUnidadesFederativas } from '../../../components';
import {
  MatriculaSiapeFormModel,
  MatriculaSiapeFormModelKeys,
  MatriculaSiapeFormModelKeys as areas,
} from '../../models/matricula-siape-form.model';
import { SelectOrgao } from '../selects';

export const desktopSiapeRegisterGridTemplate = `'${areas.matricula} ${areas.matricula} ${areas.dataAdmissao} ${areas.uf}' 
                                                 '${areas.orgao} ${areas.orgao} ${areas.orgao} ${areas.orgao}'`;

type SiapeRegisterFormProps = FormPartProps<MatriculaSiapeFormModel>;

export const SiapeRegisterForm: FC<SiapeRegisterFormProps> = ({
  initialData,
  errors,
  control,
  hasGridAreas,
}) => (
  <>
    <GridItem gridArea={hasGridAreas ? areas.matricula : 'unset'}>
      <FormItemControl
        label="Matrícula"
        name={MatriculaSiapeFormModelKeys.matricula}
        defaultValue={initialData?.matricula}
        errorMessage={errors?.matricula?.message}
        control={control}
        as={BemTextInput}
        type="number"
        rules={{
          validate: {
            length(matricula: string) {
              if (
                matricula &&
                (matricula.length < 7 || matricula.length > 10)
              ) {
                return 'Matrícula inválida';
              }
              return true;
            },
          },
        }}
        required
      />
    </GridItem>
    <GridItem gridArea={hasGridAreas ? areas.dataAdmissao : 'unset'}>
      <FormItemControl
        label="Data da Admissão"
        name={MatriculaSiapeFormModelKeys.dataAdmissao}
        defaultValue={initialData?.dataAdmissao}
        errorMessage={errors?.dataAdmissao?.message}
        control={control}
        as={BemDateInput}
        required
      />
    </GridItem>
    <GridItem gridArea={hasGridAreas ? areas.uf : 'unset'}>
      <FormItemControl
        label="UF"
        name={MatriculaSiapeFormModelKeys.uf}
        defaultValue={initialData?.uf}
        errorMessage={errors?.uf?.message}
        required
        control={control}
        as={SelectUnidadesFederativas}
      />
    </GridItem>
    <GridItem gridArea={hasGridAreas ? areas.orgao : 'unset'}>
      <FormItemControl
        label="Orgão"
        name={MatriculaSiapeFormModelKeys.orgao}
        defaultValue={initialData?.orgao?.toString()}
        errorMessage={errors?.orgao?.message}
        required
        control={control}
        as={SelectOrgao}
      />
    </GridItem>
  </>
);
