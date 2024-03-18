import { FC } from 'react';

import { Controller } from 'react-hook-form';

import {
  RadioCard,
  RadioCardsGroup,
  BemErrorBoundary,
  Loader,
} from '@pcf/design-system';
import { getConveniosQueryConfig, ConvenioModel, Resource } from '@pcf/core';

interface TipoConsignadoFormProps {
  initialData?: {
    tipoConvenio: ConvenioModel;
  };
  control;
  trigger?;
}

export const TipoConsignadoForm: FC<TipoConsignadoFormProps> = ({
  control,
  initialData,
  trigger,
}) => {
  return (
    <BemErrorBoundary>
      <Resource<ConvenioModel[]>
        path={getConveniosQueryConfig().queryKey ?? ''}
        loadCallback={trigger}
        loaderComponent={<Loader />}
        render={({ data: convenios }) => (
          <Controller
            control={control}
            name="tipoConvenio"
            defaultValue={initialData?.tipoConvenio || ''}
            rules={{ required: true }}
            render={({ field: { onChange, value } }) => (
              <RadioCardsGroup
                minWidth="100%"
                name="tipoConvenio"
                onChange={(id) =>
                  onChange(convenios.find((c) => c.id === parseInt(id, 10)))
                }
                defaultValue={value?.id?.toString() || ''}
              >
                {convenios.map((opt) => (
                  <RadioCard
                    key={opt.id}
                    value={opt.id.toString()}
                    title={opt.nome}
                    information={opt.descricao}
                  />
                ))}
              </RadioCardsGroup>
            )}
          />
        )}
      />
    </BemErrorBoundary>
  );
};
