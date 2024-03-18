import { forwardRef } from 'react';

import { Button, Flex } from '@chakra-ui/react';
import useSelect from 'react-select-search/dist/cjs/useSelect';

import { FormElRef } from '../../form-item';
import { BemSelectProps } from '../select.model';
import { ModalProvider, useModal } from '../../../modal';
import { SelectActionIcons } from './select-action-icons';
import { useSelectInternalState } from './use-select-internal-state';
import { SelectMobileDialog } from './select-mobile-dialog';

const SelectMobileItself = forwardRef<FormElRef, BemSelectProps>(
  (
    {
      defaultValue,
      options,
      value: externalValue,
      hasError,
      isLoading,
      disabled,
      label,
      searchQuery,
      retry,
      onBlur,
      onChange,
    },
    ref,
  ) => {
    const { showModal, hideModal } = useModal();

    const [value, , handleChange] = useSelectInternalState(
      defaultValue,
      externalValue,
      onBlur,
      onChange,
    );

    const [snapshot, valueProps, optionProps] = useSelect({
      options,
      value,
      disabled,
      allowEmpty: false,
      onChange: (selected, opt) => {
        handleChange(selected as any, opt);
        hideModal();
      },
    });

    const openModal = (): void => {
      showModal({
        closeOnClickOverlay: false,
        modal: (
          <SelectMobileDialog
            value={snapshot.value}
            options={snapshot.options}
            onClose={hideModal}
            label={label}
            optionProps={optionProps}
          />
        ),
      });
    };

    return (
      <>
        <Flex flex={1} justifyContent="center" alignItems="center" ref={ref}>
          <Button
            variant="ghost"
            onClick={openModal}
            flex={1}
            disabled={isLoading || hasError}
            colorScheme="grey"
            padding={0}
            marginRight={2}
            color="inherit"
            outline="none"
            ref={valueProps.ref}
            _hover={{
              background: 'none',
            }}
            sx={{
              background: 'none !important',
            }}
          >
            <input
              style={{ outline: 'none' }}
              value={snapshot.displayValue}
              readOnly
            />
          </Button>

          <SelectActionIcons
            hasError={hasError}
            isLoading={isLoading}
            hasValue={!!value}
            disabled={disabled}
            isMenuOpen={false}
            hasSearch={!!searchQuery}
            onClear={() => handleChange('')}
            onRetry={() => retry && retry()}
          />
        </Flex>
      </>
    );
  },
);

export const SelectMobile = forwardRef<FormElRef, BemSelectProps>(
  (props, ref) => (
    <ModalProvider>
      <SelectMobileItself {...props} key={props.options.length + 1} ref={ref} />
    </ModalProvider>
  ),
);
