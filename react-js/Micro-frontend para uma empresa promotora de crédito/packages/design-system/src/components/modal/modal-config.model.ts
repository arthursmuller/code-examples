import { FunctionComponent, ReactElement } from 'react';

import { DefaultModalConfig } from './components/default-modal-config';

export interface ModalConfig extends DefaultModalConfig {
  modal?: ReactElement | FunctionComponent | null;

  closeOnClickOverlay?: boolean;
  customOffset?: {
    top?: number;
    bottom?: number;
    left?: number;
    right?: number;
  };
}
