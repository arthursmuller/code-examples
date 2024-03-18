import { Box } from '@chakra-ui/react';
import { useIntl } from 'react-intl';
import Swal, { SweetAlertOptions } from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';

import '../../assets/css/custom.css';
import Text from '../Text';

interface AlertProps extends SweetAlertOptions {
  onConfirm?: () => void;
}

const Alert = ({ onConfirm, ...rest }: AlertProps) => {
  withReactContent(Swal)
    .fire({
      icon: 'warning',
      showCancelButton: true,
      customClass: {
        confirmButton: 'button blue',
        cancelButton: 'button white',
      },
      iconColor: '#d7d7dc',
      ...rest,
    })
    .then((result) => {
      if (result.isConfirmed) {
        onConfirm();
      }
    });
};

export const AlertDelete = ({ onConfirm, ...rest }: AlertProps) => {
  const intl = useIntl();
  Alert({
    onConfirm: onConfirm,
    html: intl.formatMessage({ id: 'alert.delete.text' }),
    confirmButtonText: intl.formatMessage({ id: 'alert.button.remove' }),
    cancelButtonText: intl.formatMessage({ id: 'alert.button.cancel' }),
    ...rest,
  });
};

export const AlertHtml = (props) => {
  return (
    <Box d="flex" flexDirection="column" alignItems="center">
      <Text>{props.text}</Text>
      {props?.irreversible && (
        <Text fontWeight="normal" fontSize="sm" mt="10px" color="black.500">
          {props?.irreversible}
        </Text>
      )}
    </Box>
  );
};

export default Alert;
