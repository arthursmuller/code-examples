import { FC } from 'react';
import { Icon } from '@chakra-ui/react';
import {
  ArrowUpIcon,
  StatusCloseErrorIcon,
  StatusReloadIcon,
  SearchIcon,
} from '@pcf/design-system-icons';

import { Loader } from '../../../loader';

interface SelectActionIconsProps {
  hasError: boolean;
  isLoading: boolean;
  hasValue: boolean;
  disabled: boolean;
  hasSearch: boolean;
  isMenuOpen: boolean;
  onClear: () => void;
  onRetry: () => void;
}

export const SelectActionIcons: FC<SelectActionIconsProps> = ({
  hasError,
  isLoading,
  hasValue,
  disabled,
  hasSearch,
  isMenuOpen,
  onClear,
  onRetry,
}) => {
  return !hasError ? (
    <>
      {isLoading && <Loader height="100%" width="40px" />}

      {!isLoading && hasValue && (
        <Icon
          as={StatusCloseErrorIcon}
          width="10px"
          height="100%"
          marginRight="8px"
          cursor={!disabled ? 'pointer' : 'default'}
          pointerEvents={!disabled ? 'inherit' : 'none'}
          onClick={onClear}
        />
      )}

      {!hasValue && !isLoading && (
        <>
          {!hasSearch ? (
            <Icon
              as={ArrowUpIcon}
              width="10px"
              height="100%"
              marginRight="8px"
              transform={isMenuOpen ? 'rotate(0deg)' : 'rotate(180deg)'}
              transition="transform .25s"
              pointerEvents="none"
              position="absolute"
              right="8px"
              top="0"
            />
          ) : (
            <Icon
              as={SearchIcon}
              width="12px"
              height="100%"
              marginRight="8px"
              pointerEvents="none"
              position="absolute"
              right="8px"
              top="0"
              color="grey.400"
            />
          )}
        </>
      )}
    </>
  ) : (
    <Icon
      as={StatusReloadIcon}
      height="100%"
      marginRight="8px"
      cursor="pointer"
      onClick={onRetry}
    />
  );
};
