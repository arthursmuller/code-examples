import { IntlShape } from "react-intl";

export const validatorDefaultMessages = (intl: IntlShape) => ({
  required: intl.formatMessage({ id: 'form.field_required' }),
  email: intl.formatMessage({ id: 'form.field_email' }),
  alpha_num_dash_space: intl.formatMessage({ id: 'form.field_alpha_num_dash_space' }),
  default: intl.formatMessage({ id: 'form.field_default' })
});
