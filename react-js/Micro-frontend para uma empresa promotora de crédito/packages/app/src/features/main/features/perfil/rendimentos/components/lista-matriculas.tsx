import { FC } from 'react';

import { Flex } from '@chakra-ui/react';
import { Controller } from 'react-hook-form';

import {
  getRendimentosQueryConfig,
  RendimentoResponseModel,
  Resource,
} from '@pcf/core';
import {
  BemErrorBoundary,
  Loader,
  NoDataDisplay,
  RadioCardsGroup,
} from '@pcf/design-system';
import { MatriculaCard } from 'features/main/components/matricula';

interface ListaMatriculasProps {
  control: any;
  onEdit: (matriculaId: string | number) => void;
  isSimulationFlow?: boolean;
}

export const ListaMatriculas: FC<ListaMatriculasProps> = ({
  control,
  onEdit,
  isSimulationFlow = false,
}) => {
  return (
    <BemErrorBoundary>
      <Resource<RendimentoResponseModel[]>
        path={getRendimentosQueryConfig().queryKey ?? ''}
        noDataComponent={<NoDataDisplay entityName="matrícula" />}
        loaderComponent={<Loader />}
        render={({ data: matriculas }) => (
          <Flex direction="column">
            <Controller
              control={control}
              name="matricula"
              defaultValue=""
              rules={{ required: true }}
              render={({ field: { onChange, value } }) => (
                <RadioCardsGroup
                  name="matricula"
                  onChange={onChange}
                  defaultValue={value || ''}
                  minWidth="100%"
                  fitMode="fill"
                >
                  {matriculas.map((matricula) => (
                    <MatriculaCard
                      key={matricula.id}
                      value={matricula.id.toString()}
                      matricula={matricula}
                      edit={onEdit}
                      isSimulationFlow={isSimulationFlow}
                    />
                  ))}
                </RadioCardsGroup>
              )}
            />
          </Flex>
        )}
      />
    </BemErrorBoundary>
  );
};
