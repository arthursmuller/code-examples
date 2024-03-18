import { MatriculaInssFormModel } from '../../models/matricula-inss-form.model';
import { MatriculaSiapeFormModel } from '../../models/matricula-siape-form.model';

export interface FormPartProps<
  T = MatriculaInssFormModel | MatriculaSiapeFormModel,
> {
  initialData: T;
  errors;
  control;
  trigger?;
  unregister?;
  hasGridAreas?: boolean;
}
