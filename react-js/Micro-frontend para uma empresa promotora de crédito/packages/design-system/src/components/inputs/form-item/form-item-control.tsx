import { ElementType, ReactElement } from 'react';

import { PropsOf } from '@chakra-ui/react';
import { Controller } from 'react-hook-form';

import { FormItem, FormItemProps } from './form-item';

interface CustomControllerRenderProps {
  onChange: (...event) => void;
  onBlur: (e) => void;
  value;
  name: string;
  label?: string;
  disabled: boolean;
}

export interface CustomControllerProps<AsType> {
  name: string;
  label?: string;
  control;
  defaultValue?;
  rules?;

  render?: (data: CustomControllerRenderProps) => ReactElement;
  as?: AsType;
}

interface CustomControllerInnerProps<AsType>
  extends CustomControllerProps<AsType> {
  onBlur?: (e) => void;
  disabled?: boolean;
}

type CustomControllerFC = <Tag extends ElementType>(
  props: CustomControllerInnerProps<Tag> & PropsOf<Tag>,
) => JSX.Element;

export const CustomController: CustomControllerFC = ({
  onBlur,
  onChange,
  disabled,

  name,
  label,
  control,
  defaultValue,
  rules,

  render,
  as: InputComp,

  ...tagProps
}) => {
  const customRender = ({ field }): ReactElement => {
    const nextFieldProps = {
      ...field,

      label,
      defaultValue,
      value: field.value,
      disabled,
      onBlur: (e) => {
        onBlur && onBlur(e);
        field.onBlur(e);
      },
      onChange: (e) => {
        onChange && onChange(e);
        field.onChange(e);
      },
    };

    if (render) {
      return render(nextFieldProps);
    } else {
      return <InputComp {...nextFieldProps} {...tagProps} />;
    }
  };

  return (
    <Controller
      name={name}
      control={control}
      defaultValue={defaultValue || ''}
      rules={rules}
      render={customRender}
    />
  );
};

export interface FormItemControlProps<AsType>
  extends CustomControllerProps<AsType>,
    FormItemProps {
  /** Adds default rule required for `React Hooks Form's Controller` */
  required?: boolean;
}

type FormItemControlFC = <Tag extends ElementType>(
  props: FormItemControlProps<Tag> & PropsOf<Tag>,
) => JSX.Element;

const requiredRule = { required: 'Campo Obrigatório' };

export const FormItemControl: FormItemControlFC = ({
  name,
  control,
  defaultValue,
  rules,
  render,
  as,
  required,

  label,
  errorMessage,
  info,
  hasStatusIcon,
  width,
  disabled,
  icon,
  background,
  height,

  ...tagProps
}) => {
  const nextRules = required ? { ...rules, ...requiredRule } : rules;

  return (
    <FormItem
      label={label}
      errorMessage={errorMessage}
      info={info}
      hasStatusIcon={hasStatusIcon}
      width={width}
      disabled={disabled}
      icon={icon}
      background={background}
      height={height}
    >
      <CustomController
        name={name}
        defaultValue={defaultValue}
        rules={nextRules}
        control={control}
        render={render}
        as={as}
        disabled={disabled}
        label={label}
        {...tagProps}
      />
    </FormItem>
  );
};
