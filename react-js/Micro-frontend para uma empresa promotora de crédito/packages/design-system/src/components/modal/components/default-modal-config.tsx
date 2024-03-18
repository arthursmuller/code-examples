import { DefaultModalHeaderProps } from './default-modal-header';

import { ColorSchemes } from '../../../bem-chakra-theme/foundations/colors';

export interface DefaultModalConfig
  extends Omit<
    DefaultModalHeaderProps,
    'bg' | 'icon' | 'color' | 'iconBg' | 'textStyle'
  > {
  information?: string;
  type?:
    | ColorSchemes.warning
    | ColorSchemes.error
    | ColorSchemes.success
    | `${ColorSchemes.warning}`
    | `${ColorSchemes.error}`
    | `${ColorSchemes.success}`;
  onClose?: () => void;
  closeText?: string;
  onConfirm?: () => void | Promise<void>;
  confirmText?: string;
  onCancel?: () => void;
}
