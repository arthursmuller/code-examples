import { FC, useEffect } from 'react';

import { GridItem } from '@chakra-ui/react';

import { FormItemControl, BemSelect, BemTextInput } from '@pcf/design-system';
import { useSiapeTiposFuncionais } from '@pcf/core';

import { FormPartProps } from '.';

import {
  MatriculaSiapeFormModel,
  MatriculaSiapeFormModelKeys,
  MatriculaSiapeFormModelKeys as areas,
} from '../../models/matricula-siape-form.model';
import { SelectSiapeTipoFuncional } from '../selects';

const CODIGO_SERVIDOR = 'S';

export const desktopInstitutingGridTemplate = `'${areas.siapeTipoFuncional} ${areas.siapeTipoFuncional} ${areas.possuiRepresentacaoPorProcurador} ${areas.possuiRepresentacaoPorProcurador}' 
                                               '${areas.nomeInstituidor} ${areas.nomeInstituidor} ${areas.matriculaInstituidor} ${areas.matriculaInstituidor}'`;

interface InstitutingForm extends FormPartProps<MatriculaSiapeFormModel> {
  watch;
}

export const InstitutingForm: FC<InstitutingForm> = ({
  initialData,
  errors,
  control,
  watch,
  hasGridAreas,
  unregister,
}) => {
  const { data: tiposFuncionais } = useSiapeTiposFuncionais();

  const selectedTipoFuncional = watch(
    MatriculaSiapeFormModelKeys.siapeTipoFuncional,
    initialData.siapeTipoFuncional,
  )?.toString();

  const isServidor =
    !!selectedTipoFuncional &&
    tiposFuncionais?.find((t) => t.id === parseInt(selectedTipoFuncional, 10))
      ?.codigo === CODIGO_SERVIDOR;

  useEffect(() => {
    if (selectedTipoFuncional) {
      if (isServidor) {
        unregister([
          MatriculaSiapeFormModelKeys.nomeInstituidor,
          MatriculaSiapeFormModelKeys.matriculaInstituidor,
          MatriculaSiapeFormModelKeys.possuiRepresentacaoPorProcurador,
        ]);
      }
    }
  }, [isServidor]);

  return (
    <>
      <GridItem gridArea={hasGridAreas ? areas.siapeTipoFuncional : 'unset'}>
        <FormItemControl
          label="Espécie"
          name={MatriculaSiapeFormModelKeys.siapeTipoFuncional}
          defaultValue={initialData?.siapeTipoFuncional}
          errorMessage={errors?.siapeTipoFuncional?.message}
          control={control}
          as={SelectSiapeTipoFuncional}
          required
        />
      </GridItem>
      {selectedTipoFuncional && !isServidor && (
        <>
          <GridItem gridArea={hasGridAreas ? areas.nomeInstituidor : 'unset'}>
            <FormItemControl
              label="Nome do(a) Instituidor(a)"
              name={MatriculaSiapeFormModelKeys.nomeInstituidor}
              defaultValue={initialData?.nomeInstituidor}
              errorMessage={errors?.nomeInstituidor?.message}
              control={control}
              as={BemTextInput}
              required
            />
          </GridItem>
          <GridItem
            gridArea={
              hasGridAreas
                ? MatriculaSiapeFormModelKeys.matriculaInstituidor
                : 'unset'
            }
          >
            <FormItemControl
              label="Matrícula do(a) Instituidor(a)"
              name={MatriculaSiapeFormModelKeys.matriculaInstituidor}
              defaultValue={initialData?.matriculaInstituidor}
              errorMessage={errors?.matriculaInstituidor?.message}
              control={control}
              as={BemTextInput}
              type="number"
              required
            />
          </GridItem>
          <GridItem
            gridArea={
              hasGridAreas ? areas.possuiRepresentacaoPorProcurador : 'unset'
            }
          >
            <FormItemControl
              label="Representado(a) por Procurador(a)?"
              name={
                MatriculaSiapeFormModelKeys.possuiRepresentacaoPorProcurador
              }
              defaultValue={initialData?.possuiRepresentacaoPorProcurador}
              errorMessage={errors?.possuiRepresentacaoPorProcurador?.message}
              control={control}
              as={BemSelect}
              options={[
                { name: 'Sim', value: '1' },
                { name: 'Não', value: '0' },
              ]}
              required
            />
          </GridItem>
        </>
      )}
    </>
  );
};
