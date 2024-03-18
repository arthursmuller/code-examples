import { FunctionComponent, ReactElement } from 'react';

import { Button, Flex, Text } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';

import { GenericData, useStepsContainerContext } from '@pcf/design-system';

import { FormGrid, FormGridProps } from '../form-grid';

interface FormProps<DataType> {
  initialData: DataType;
  control;
  errors;
  watch;
  setValue;
}

interface MatriculaBaseStepProps<DataType> extends FormGridProps {
  renderForm?: ({
    initialData,
    control,
    errors,
    watch,
    setValue,
  }: FormProps<DataType>) => ReactElement;
  as?: FunctionComponent<FormProps<DataType>>;
  customTitle?: string;
  onSubmit?: (data: DataType) => void;
  isSubmiting?: boolean;
}
export function MatriculaBaseStep<DataType extends GenericData>({
  renderForm,
  as: FormComponentFC,
  gridTemplateColumns,
  customTitle,
  onSubmit,
  isSubmiting,
  gridTemplateAreas,
}: MatriculaBaseStepProps<DataType>): ReactElement {
  const { nextStep, data } = useStepsContainerContext<DataType>();

  const {
    handleSubmit,
    control,
    watch,
    formState: { isValid, errors },
    setValue,
    trigger,
    unregister,
  } = useForm<DataType>({
    mode: 'onChange',
  });

  const formProps = {
    errors,
    control,
    watch,
    initialData: data,
    setValue,
    trigger,
    unregister,
  };

  const type = data.tipoConvenio?.nome?.split(' ')[0];

  const submit = (stepData: DataType): void => {
    if (onSubmit) {
      onSubmit({ ...stepData, ...data });
    } else {
      nextStep(stepData);
    }
  };

  return (
    <Flex direction="column" marginY={4}>
      <Text
        as="h2"
        textStyle="bold24"
        color="secondary.mid-dark"
        textAlign="center"
        flex="1"
      >
        {customTitle || `Cadastro de Rendimento ${type}`}
      </Text>

      <FormGrid
        gridTemplateColumns={gridTemplateColumns || '1fr'}
        gridTemplateAreas={gridTemplateAreas}
      >
        {FormComponentFC && <FormComponentFC {...formProps} />}
        {renderForm && renderForm(formProps)}
      </FormGrid>

      <Button
        disabled={!isValid}
        onClick={handleSubmit(submit as any)}
        isLoading={isSubmiting}
      >
        Continuar
      </Button>
    </Flex>
  );
}

MatriculaBaseStep.defaultProps = {
  renderForm: null,
  as: null,
  isSubmiting: false,
  onSubmit: null,
  customTitle: null,
};
