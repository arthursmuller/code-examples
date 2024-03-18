import {
  createContext,
  HTMLAttributes,
  useState,
  useContext,
  FC,
  useMemo,
  useCallback,
  useRef,
} from 'react';

import { Box, Center, useBreakpointValue } from '@chakra-ui/react';

import { Overlay, DefaultModal } from './components';
import { ModalConfig } from './modal-config.model';

import { ColorSchemes } from '../../bem-chakra-theme/foundations/colors';

export const MODAL_IDENTIFIER_CLASS = 'modal-content';

interface ModalContextData {
  showModal(ModalConfig: ModalConfig): void;
  hideModal(): void;
}

export const getDefaultErrorModalConfig = (
  configOverrides?: ModalConfig,
): ModalConfig => {
  return {
    title: 'Ocorreu um erro!',
    information: '',
    type: ColorSchemes.error,
    closeOnClickOverlay: true,
    closeText: 'Ok',
    onClose: () => {},
    ...(configOverrides || {}),
  };
};

const ModalContext = createContext<ModalContextData>({} as ModalContextData);

const ModalProvider: FC<HTMLAttributes<HTMLElement>> = ({ children }) => {
  const [visible, setIsVisible] = useState<boolean>(false);
  const [configs, setConfig] = useState<ModalConfig>();
  const onClose = useRef<() => void>();
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  const hideModal = useCallback((): void => {
    !!onClose.current && onClose.current();

    setIsVisible(false);
    setConfig(undefined);
  }, []);

  const showModal = useCallback(
    ({
      title = 'Title',
      type = ColorSchemes.success,
      closeOnClickOverlay = true,
      ...props
    }: ModalConfig): void => {
      setConfig({ title, type, closeOnClickOverlay, ...props });
      onClose.current = props.onClose;

      setIsVisible(true);
    },
    [],
  );

  const memoValue = useMemo(
    () => ({
      showModal,
      hideModal,
    }),
    [showModal, hideModal],
  );

  const { customOffset } = configs || {};

  return (
    <ModalContext.Provider value={memoValue}>
      {children}
      {visible && (
        <Overlay
          closeOnClick={configs?.closeOnClickOverlay || false}
          hideModal={hideModal}
        >
          <Center
            className={MODAL_IDENTIFIER_CLASS}
            {...(customOffset && !isMobile
              ? { ...customOffset, position: 'absolute' }
              : { height: '100%' })}
          >
            {configs?.modal ? (
              (typeof configs.modal === 'function' && (
                <Box as={configs.modal as FC} />
              )) ||
              configs.modal
            ) : (
              <DefaultModal {...configs} onClose={hideModal} />
            )}
          </Center>
        </Overlay>
      )}
    </ModalContext.Provider>
  );
};

function useModal(): ModalContextData {
  const context = useContext(ModalContext);

  if (!context) {
    throw new Error('useModal must be used within an ModalProvider');
  }

  return context;
}

export { ModalProvider, useModal };
