import { IdentityType } from '../identity-types.enum';

export interface UploadIdentityForm {
  files: File[];
  documentType: IdentityType;
}
