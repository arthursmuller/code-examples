import React from 'react';

import Alert, { AlertHtml } from '../../../../components/Alert';
import { newPasswordDevice } from '../../../../store/device';

const alertNewPassword = ( intl, dispatch, idDevice: number ) => {
  Alert({
    onConfirm: () => dispatch(newPasswordDevice(idDevice)),
    html: (
      <AlertHtml
        irreversible={intl.formatMessage({ id: 'devices.alert.irreversible' })}
        text={intl.formatMessage({ id: 'devices.alert.newpassword.text' })}
      />
    ),
    confirmButtonText: intl.formatMessage({
      id: 'devices.alert.newpassword.button',
    }),
    cancelButtonText: intl.formatMessage({ id: 'devices.alert.cancel' }),
  });
};

export default alertNewPassword;
