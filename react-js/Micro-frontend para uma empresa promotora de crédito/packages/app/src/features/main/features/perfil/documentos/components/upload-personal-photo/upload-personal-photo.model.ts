import { UploadDocumentForm } from '../shared-steps/models/upload-document-form.model';

export interface UploadPersonalPhotoModel extends UploadDocumentForm {
  uploadType: 'webcam' | 'upload';
}
