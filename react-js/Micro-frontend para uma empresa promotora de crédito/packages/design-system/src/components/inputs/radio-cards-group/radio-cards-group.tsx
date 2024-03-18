import React, { FC, ReactElement, Children } from 'react';

import {
  Grid,
  GridProps,
  useCheckboxGroup,
  UseCheckboxGroupProps,
  useRadioGroup,
  UseRadioGroupProps,
} from '@chakra-ui/react';

import { RadioCardProps } from './radio-card';

export interface RadioCardsGroupProps {
  name: string;
  defaultValue?: string | string[] | undefined;
  onChange?: (nextValue) => void;
  children: ReactElement<RadioCardProps>[];
  minWidth?: string;
  fitMode?: 'fit' | 'fill';
  chakraProps?: GridProps;
  isRadio?: boolean;
}

type RadioHookProps = { getRootProps; getRadioProps };
type CheckboxHookProps = { getCheckboxProps };

interface RadioCardsGroupRenderProps extends RadioCardsGroupProps {
  inputHookProps: RadioHookProps | CheckboxHookProps;
}

interface RadioCardsGroupWrapperProps extends RadioCardsGroupProps {
  renderComp: FC<RadioCardsGroupRenderProps>;
}

export const RadioCardsGroupRender: FC<RadioCardsGroupRenderProps> = ({
  inputHookProps,
  isRadio,
  children,
  minWidth = '250px',
  fitMode = 'fit',
  chakraProps,
}) => {
  const groupProps = isRadio
    ? (inputHookProps as RadioHookProps).getRootProps()
    : { name: 'contrato' };

  return (
    <Grid
      marginY={[4, 4, 6]}
      width="100%"
      gap="24px"
      gridTemplateColumns={[
        '1fr',
        `repeat(auto-${fitMode}, minmax(${minWidth}, 1fr))`,
      ]}
      {...chakraProps}
      {...groupProps}
    >
      {Children.map(children, (child) => {
        if (React.isValidElement(child)) {
          const inputProps = isRadio
            ? (inputHookProps as RadioHookProps).getRadioProps({
                value: child.props.value,
              })
            : (inputHookProps as CheckboxHookProps).getCheckboxProps({
                value: child.props.value,
              });

          return (
            <child.type {...child.props} {...inputProps} isRadio={isRadio}>
              {child}
            </child.type>
          );
        }
        return null;
      })}
    </Grid>
  );
};

const CheckboxGroupWrapper: FC<RadioCardsGroupWrapperProps> = ({
  renderComp,
  ...props
}) => {
  const inputHookProps = useCheckboxGroup(props as UseCheckboxGroupProps);

  return <>{renderComp({ inputHookProps, ...props })}</>;
};

const RadioGroupWrapper: FC<RadioCardsGroupWrapperProps> = ({
  renderComp,
  ...props
}) => {
  const inputHookProps = useRadioGroup(props as UseRadioGroupProps);

  return <>{renderComp({ inputHookProps, ...props })}</>;
};

export const RadioCardsGroup: FC<RadioCardsGroupProps> = ({
  isRadio = true,
  ...props
}) => {
  return isRadio ? (
    <RadioGroupWrapper
      {...props}
      isRadio={isRadio}
      renderComp={RadioCardsGroupRender}
    />
  ) : (
    <CheckboxGroupWrapper
      {...props}
      isRadio={isRadio}
      renderComp={RadioCardsGroupRender}
    />
  );
};
