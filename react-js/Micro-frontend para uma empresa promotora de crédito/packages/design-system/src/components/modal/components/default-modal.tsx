import { HtmlHTMLAttributes, FC, useState, useEffect } from 'react';

import { Button, ButtonProps, Flex, Text } from '@chakra-ui/react';

import {
  LoadingfailIcon,
  CheckIcon,
  StatusCloseErrorIcon,
} from '@pcf/design-system-icons';

import { DefaultModalConfig } from './default-modal-config';
import {
  DefaultModalheader,
  DefaultModalHeaderProps,
} from './default-modal-header';
import {
  DefaultModalContainer,
  DefaultModalContainerProps,
} from './default-modal-container';

import { ColorSchemes } from '../../../bem-chakra-theme/foundations/colors';

const { success, warning, error } = ColorSchemes;

const typeColors = {
  content: {
    text: {
      [success]: 'white',
      [error]: 'white',
      [warning]: 'white',
    },
    bg: {
      [success]: 'success.regular',
      [error]: 'error.regular',
      [warning]: 'warning.regular',
    },
    iconBg: {
      [success]: 'success.dark',
      [error]: 'error.dark',
      [warning]: 'warning.washed',
    },
    icon: {
      [success]: CheckIcon,
      [error]: StatusCloseErrorIcon,
      [warning]: LoadingfailIcon,
    },
    iconColor: {
      [success]: 'white',
      [error]: 'white',
      [warning]: 'grey.700',
    },
  },
};

interface DefaultModalComposition {
  Header: FC<DefaultModalHeaderProps>;
  Container: FC<DefaultModalContainerProps>;
}

interface DefaultModalProps
  extends HtmlHTMLAttributes<HTMLElement>,
    DefaultModalConfig {}

const DefaultModal: FC<DefaultModalProps> & DefaultModalComposition = ({
  title = '',
  information,
  type = ColorSchemes.warning,
  onClose,
  onConfirm,
  onCancel,
  closeText = 'Fechar',
  confirmText = 'Confirmar',
}) => {
  const cancelButtonProps: ButtonProps = {};
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    return () => {
      setIsLoading(false);
    };
  }, []);

  if (onConfirm) {
    cancelButtonProps.color = 'grey.700';
    cancelButtonProps.bg = 'grey.200';
    cancelButtonProps.marginTop = [4, 4, 0];
    cancelButtonProps.marginLeft = [0, 0, 4];
    cancelButtonProps.colorScheme = 'grey';
  }

  return (
    <DefaultModalContainer>
      <DefaultModalheader
        icon={typeColors.content.icon[type]}
        color={typeColors.content.text[type]}
        iconColor={typeColors.content.iconColor[type]}
        iconBg={typeColors.content.iconBg[type]}
        bg={typeColors.content.bg[type]}
        title={title}
      />

      <>
        {information && (
          <Text textStyle="regular16" textAlign="center">
            {information}
          </Text>
        )}

        <Flex
          direction={['column', 'column', 'row']}
          justifyContent="center"
          mt="24px"
        >
          {onConfirm && (
            <Button
              colorScheme={type}
              isLoading={isLoading}
              onClick={async () => {
                if (
                  onConfirm &&
                  onConfirm.constructor.name === 'AsyncFunction'
                ) {
                  setIsLoading(true);
                }

                onConfirm && (await onConfirm());
                onClose && onClose();
              }}
            >
              {confirmText}
            </Button>
          )}

          {(onClose || onCancel) && (
            <Button
              onClick={() => {
                onCancel && onCancel();
                onClose && onClose();
              }}
              colorScheme={type}
              {...cancelButtonProps}
            >
              {closeText}
            </Button>
          )}
        </Flex>
      </>
    </DefaultModalContainer>
  );
};

DefaultModal.Header = DefaultModalheader;
DefaultModal.Container = DefaultModalContainer;

export { DefaultModal };
