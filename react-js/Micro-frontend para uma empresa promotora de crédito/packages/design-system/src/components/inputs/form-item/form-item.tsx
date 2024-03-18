import { ReactElement, useState, forwardRef, cloneElement } from 'react';

import { Message } from 'react-hook-form';
import {
  Box,
  Flex,
  FormControl,
  FormErrorMessage,
  FormHelperText,
  FormLabel,
  Icon,
  useUpdateEffect,
} from '@chakra-ui/react';
import { useMount } from 'react-use';

import { PasswordIcon } from '@pcf/design-system-icons';

import { topToBottom } from '../../../animations';
import textStyles from '../../../bem-chakra-theme/foundations/typography';
import { StatusCheckCircleIcon } from '../../validation-text';

const colors = {
  empty: 'grey.600',
  filled: 'grey.800',
  error: 'error.regular',
  valid: 'success.regular',
  active: 'primary.primary',
};

export interface FormItemProps {
  label?: string;
  errorMessage?: Message;
  info?: string;
  hasStatusIcon?: boolean;
  width?: string;
  disabled?: boolean;
  icon?: ReactElement;
  background?: string;
  overrideColor?: string;
  height?: string | number;
}

interface FormItemClearProps extends FormItemProps {
  children: ReactElement;
}

export type FormElRef =
  | HTMLInputElement
  | HTMLSelectElement
  | HTMLTextAreaElement
  | any // eslint-disable-line
  | null;

const DEFAULT_HEIGHT = '56px';

export const FormItem = forwardRef<FormElRef, FormItemClearProps>(
  (
    {
      label,
      errorMessage,
      info,
      hasStatusIcon = false,
      width = '100%',
      disabled,
      icon,
      children,
      background = 'transparent',
      overrideColor,
      height = DEFAULT_HEIGHT,
    },
    ref,
  ) => {
    const [hasValue, setHasValue] = useState<boolean>();

    const internalChildrenValue = children?.props?.control?.watchInternal(
      children.props.name,
    );

    useUpdateEffect(() => {
      if (!hasValue && !!internalChildrenValue) {
        setHasValue(internalChildrenValue);
      }
    }, [internalChildrenValue, hasValue]);

    const handleBlur = (event): void => {
      const check = event?.target?.value ?? event;
      setHasValue(!!check);
    };

    useMount(() => {
      if (hasValue === undefined) {
        setHasValue(!!children?.props?.defaultValue);
      }
    });

    const customBlur = (e): void => {
      handleBlur(e);
      children.props.onBlur && children.props.onBlur(e);
    };

    let outlineColor = !hasValue ? colors.empty : colors.filled;

    if (hasStatusIcon && !disabled) {
      outlineColor = colors.valid;
    }
    if (errorMessage) outlineColor = colors.error;

    return (
      <FormControl
        colorScheme="primary"
        isInvalid={!!errorMessage}
        isDisabled={disabled}
        width={width}
        display="flex"
        flexDirection="column"
        paddingX="8px"
        paddingY="4px"
        border="1px solid"
        borderRadius="4px"
        marginBottom="16px"
        height={height}
        color={overrideColor || colors.filled}
        borderColor={overrideColor || outlineColor}
        backgroundColor={disabled ? 'grey.200' : background}
        sx={{
          'input, textarea, select': {
            width: '100%',
            height: '24px',
            background,
            border: 0,
            marginTop: '20px',

            ...textStyles.bold16,

            opacity: hasValue ? 1 : 0,

            '&:focus': {
              outline: 'none',
              boxShadow: 'none',
              opacity: 1,
            },
          },
          textarea: {
            height: 'calc(100% - 20px)',
          },
          '&:focus-within': {
            borderColor: overrideColor || colors.active,
            color: overrideColor || colors.active,

            label: {
              top: '6px',

              ...textStyles.regular12,

              color: overrideColor || colors.active,
            },
          },
        }}
      >
        {label && (
          <FormLabel
            position="absolute"
            fontSize={hasValue ? '12px' : '16px'}
            top={hasValue ? '6px' : '16px'}
            transition="all 0.25s"
            pointerEvents="none"
            color={overrideColor || outlineColor}
            _invalid={{ color: overrideColor || colors.error }}
            _disabled={{
              color: overrideColor || hasValue ? colors.filled : outlineColor,
            }}
            whiteSpace="nowrap"
            overflow="hidden"
            textOverflow="ellipsis"
            width="calc(100% - 8px)"
          >
            {label}
          </FormLabel>
        )}

        <Flex height="100%">
          {cloneElement(
            children,
            { ...children.props, onBlur: customBlur, disabled },
            null,
          )}

          {(hasStatusIcon || icon) && (
            <Box margin="auto" h="fit-content" marginX="8px">
              {disabled ? (
                <Icon as={PasswordIcon} />
              ) : (
                icon || (
                  <StatusCheckCircleIcon
                    hasError={!!errorMessage}
                    size="24px"
                  />
                )
              )}
            </Box>
          )}
        </Flex>

        {info && !errorMessage && (
          <FormHelperText
            textStyle="regular12"
            position="absolute"
            top="50px"
            animation={`.25s ${topToBottom}`}
          >
            {info}
          </FormHelperText>
        )}

        {errorMessage && (
          <FormErrorMessage
            role="alert"
            textStyle="regular12"
            color="error.regular"
            position="absolute"
            top={height !== DEFAULT_HEIGHT ? height : '50px'}
            animation={`.25s ${topToBottom}`}
          >
            {errorMessage}
          </FormErrorMessage>
        )}
      </FormControl>
    );
  },
);
