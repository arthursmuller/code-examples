import { FC, ReactElement } from 'react';

import { Flex, useCheckbox, useRadio } from '@chakra-ui/react';

export interface RadioCardLogicProps {
  value: string;
}

interface InnerProps extends RadioCardLogicProps {
  render: (props) => ReactElement;
  isRadio?: boolean;
}

interface InnerPostWrapperProps {
  render: (props) => ReactElement;
  state;
  getInputProps;
  getCheckboxProps;
  isRadio: boolean;
}
interface InnerWrapperProps extends InnerProps {
  renderComp: FC<InnerPostWrapperProps>;
}

const RadioCardRenderLogic: FC<InnerPostWrapperProps> = ({
  render,
  state,
  getInputProps,
  getCheckboxProps,
  isRadio,
}) => {
  const input = getInputProps();
  const checkbox = getCheckboxProps();

  const { isChecked } = state;

  return (
    <Flex
      as="label"
      layerStyle="card"
      padding="0"
      flexGrow={1}
      direction="column"
      role="checkbox"
    >
      <input {...input} style={{ display: 'none' }} />
      <Flex
        {...checkbox}
        _focus={{
          boxShadow: 'outline',
        }}
        cursor="pointer"
        flex={1}
      >
        {render && render({ isChecked, isRadio })}
      </Flex>
    </Flex>
  );
};

const CheckboxWrapper: FC<InnerWrapperProps> = ({ renderComp, ...props }) => {
  const hookData = useRadio(props);

  return <>{renderComp({ ...hookData, ...props, isRadio: true })}</>;
};

const RadioWrapper: FC<InnerWrapperProps> = ({ renderComp, ...props }) => {
  const hookData = useCheckbox(props);

  return <>{renderComp({ ...hookData, ...props, isRadio: false })}</>;
};

export const RadioCardLogic: FC<InnerProps> = ({
  isRadio = true,
  ...props
}) => {
  return isRadio ? (
    <CheckboxWrapper {...props} renderComp={RadioCardRenderLogic} />
  ) : (
    <RadioWrapper {...props} renderComp={RadioCardRenderLogic} />
  );
};
