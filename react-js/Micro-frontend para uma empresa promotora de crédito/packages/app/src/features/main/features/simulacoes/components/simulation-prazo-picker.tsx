import { FC, useRef, useState } from 'react';

import {
  Flex,
  Grid,
  Radio,
  RadioGroup,
  Checkbox,
  CheckboxGroup,
} from '@chakra-ui/react';
import { Control, Controller, DeepMap, FieldError } from 'react-hook-form';

import {
  BemTextInput,
  ExpandableSection,
  FormItemControl,
} from '@pcf/design-system';

const periods = [
  { id: '1', value: 96, description: '96 meses' },
  { id: '2', value: 72, description: '72 meses' },
  { id: '3', value: 62, description: '62 meses' },
  { id: '4', value: 48, description: '48 meses' },
  { id: '5', value: 36, description: '36 meses' },
  { id: '6', value: 24, description: '24 meses' },
  { id: '7', value: 12, description: '12 meses' },
];

const rangePeriods = [
  { id: '8', value: [73, 95], description: '95 - 73 meses' },
  { id: '9', value: [63, 71], description: '71 - 63 meses' },
  { id: '10', value: [49, 61], description: '61 - 49 meses' },
  { id: '11', value: [37, 47], description: '47 - 37 meses' },
  { id: '12', value: [25, 35], description: '35 - 25 meses' },
];

export type Prazo = number | number[] | undefined;

export interface SimulationPrazoPickerData {
  selectedOpt: string | string[];
  customOpt: string;
}

function findPrazoOptFor(
  value: string | number,
  key = 'id',
): { id: string; value: number | number[]; description: string } | undefined {
  return (
    periods.find((opt) => opt[key] === value) ||
    rangePeriods.find((opt) => opt[key] === value || opt[key][0] === value)
  );
}

export function getNextPrazoOpt({
  selectedOpt,
  customOpt,
}: SimulationPrazoPickerData): Prazo {
  return selectedOpt === '-1'
    ? parseInt(customOpt, 10) || 0
    : findPrazoOptFor(selectedOpt as string)?.value;
}

export function getNextPrazoOpts({
  selectedOpt,
  customOpt,
}: SimulationPrazoPickerData): Prazo[] {
  return (selectedOpt as string[]).map(
    (opt) => findPrazoOptFor(opt, 'id')?.value || +customOpt,
  );
}

interface SimulationPrazoPickerProps {
  prazo: Prazo | Prazo[];
  control: Control<SimulationPrazoPickerData>;
  errors: DeepMap<SimulationPrazoPickerData, FieldError>;
}

export const SimulationPrazoMultiPicker: FC<SimulationPrazoPickerProps> = ({
  prazo = [],
  control,
  errors,
}) => {
  const defaultCustomSelect = useRef<number | undefined>();

  const [defaultSelect] = useState<string[]>(
    (prazo as number[]).map((opt) => {
      let optId = findPrazoOptFor(opt, 'value')?.id;

      if (!optId) {
        defaultCustomSelect.current = opt;
        optId = '-1';
      }

      return optId;
    }),
  );

  return (
    <Controller
      control={control}
      name="selectedOpt"
      defaultValue={defaultSelect}
      render={({ field: { value: selectedValues, onChange } }) => {
        const hasCustomInput = (selectedValues as string[]).find(
          (opt) => opt === '-1',
        );

        return (
          <Flex width={['100%', '100%', 'fit-content']} direction="column">
            <CheckboxGroup
              onChange={onChange}
              defaultValue={selectedValues as string[]}
            >
              <PickerLayout
                OptionComponent={Checkbox}
                control={control}
                errors={errors}
                hasCustomInput={!!hasCustomInput}
                defaultCustomSelect={defaultCustomSelect.current}
              />
            </CheckboxGroup>
          </Flex>
        );
      }}
    />
  );
};

export const SimulationPrazoPicker: FC<SimulationPrazoPickerProps> = ({
  prazo,
  control,
  errors,
}) => {
  const [defaultRadioSelect] = useState<string | undefined>(
    (prazo && (findPrazoOptFor(prazo[0] || prazo, 'value')?.id || '-1')) ||
      undefined,
  );

  const [defaultCustomSelect] = useState<number | undefined>(
    (defaultRadioSelect === '-1' && (prazo as number)) || undefined,
  );

  return (
    <Controller
      control={control}
      name="selectedOpt"
      defaultValue={defaultRadioSelect || null}
      rules={{ required: true }}
      render={({ field: { value: selectedValue, onChange, name } }) => {
        const hasCustomInput = selectedValue === '-1';

        return (
          <RadioGroup
            onChange={onChange}
            defaultValue={`${defaultRadioSelect}`}
            name={name}
            width={['100%', '100%', 'fit-content']}
          >
            <PickerLayout
              OptionComponent={Radio}
              control={control}
              errors={errors}
              hasCustomInput={hasCustomInput}
              defaultCustomSelect={defaultCustomSelect}
            />
          </RadioGroup>
        );
      }}
    />
  );
};

interface PickerLayout {
  control: Control<SimulationPrazoPickerData>;
  errors: DeepMap<SimulationPrazoPickerData, FieldError>;
  OptionComponent: FC<any>;
  hasCustomInput: boolean;
  defaultCustomSelect: number | undefined;
}

const PickerLayout: FC<PickerLayout> = ({
  OptionComponent,
  hasCustomInput,
  defaultCustomSelect,
  control,
  errors,
}) => (
  <>
    <Grid
      marginTop="24px"
      gap="16px"
      gridTemplateColumns={['repeat(2, 1fr)', 'repeat(4, 1fr)']}
    >
      {periods?.map(({ id, description }) => (
        <OptionComponent key={id} value={id}>
          {description}
        </OptionComponent>
      ))}

      <OptionComponent value="-1">Personalizado</OptionComponent>
    </Grid>

    <Grid
      opacity={hasCustomInput ? 1 : 0}
      height={hasCustomInput ? 'fit-content' : 0}
      transition="all .25s"
      paddingTop={hasCustomInput ? 10 : 0}
      gridTemplateColumns={['1fr', '1fr', 'repeat(4, 1fr)']}
    >
      <FormItemControl
        label="Prazo em meses"
        name="customOpt"
        defaultValue={defaultCustomSelect}
        errorMessage={errors?.customOpt?.message}
        control={control}
        as={BemTextInput}
        type="number"
      />
    </Grid>

    <ExpandableSection title="Mostrar mais opções de prazos">
      <Grid
        gap="16px"
        gridTemplateColumns={['repeat(2, 1fr)', 'repeat(4, 1fr)']}
        width="100%"
      >
        {rangePeriods?.map(({ id, description }) => (
          <OptionComponent key={id} value={id}>
            {description}
          </OptionComponent>
        ))}
      </Grid>
    </ExpandableSection>
  </>
);
