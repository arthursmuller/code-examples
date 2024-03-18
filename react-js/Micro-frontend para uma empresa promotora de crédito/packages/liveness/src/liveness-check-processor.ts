import { FaceTecSession } from 'sdk/FaceTecSession';

import { LivenessRequestModel, postLiveness, PostLivenessFn } from './requests/post-liveness';
import { FaceTecSDK } from './sdk/FaceTecSDK';
import {
  FaceTecFaceScanProcessor,
  FaceTecFaceScanResultCallback,
  FaceTecSessionResult,
} from './sdk/FaceTecPublicApi';

export interface LivenessCheckProcessorConfigs {
  onComplete?: (isSuccessfully: boolean, params?: LivenessRequestModel) => void;
  onCancel?: (isLockedOut: boolean) => void;
  deviceKeyIdentifier?: string;
  livenessRequestFn?: PostLivenessFn;
}

export class LivenessCheckProcessor implements FaceTecFaceScanProcessor {
  configs: LivenessCheckProcessorConfigs;

  session: FaceTecSession;

  constructor(
    sessionToken: string,
    configs: LivenessCheckProcessorConfigs = {},
  ) {
    this.configs = configs;
    this.session = new FaceTecSDK.FaceTecSession(this, sessionToken);
  }

  processSessionResultWhileFaceTecSDKWaits(
    sessionResult: FaceTecSessionResult,
    faceScanResultCallback: FaceTecFaceScanResultCallback,
  ): void {
    if (
      sessionResult.status !==
      FaceTecSDK.FaceTecSessionStatus.SessionCompletedSuccessfully
    ) {
      if (process.env.NODE_ENV === 'development') {
        console.log(
          `Session was not completed successfully, cancelling.  Status: ${sessionResult.status}`,
        );
      }

      faceScanResultCallback.cancel();
      this.configs.onCancel &&
        this.configs.onCancel(
          sessionResult.status === FaceTecSDK.FaceTecSessionStatus.LockedOut,
        );
      return;
    }

    const parameters = {
      faceScan: sessionResult.faceScan,
      auditTrailImage: sessionResult.auditTrail[0],
      lowQualityAuditTrailImage: sessionResult.lowQualityAuditTrail[0],
      sessionId: sessionResult.sessionId,
    };

    (this.configs.livenessRequestFn || postLiveness)(
      parameters,
      this.configs.deviceKeyIdentifier,
      sessionResult.sessionId,
    )
      .then((resp) => {
        if (resp?.success) {
          faceScanResultCallback.succeed();
          this.configs.onComplete && this.configs.onComplete(true, parameters);
        } else if (resp?.success === false) {
          console.log('User needs to retry, invoking retry.');
          faceScanResultCallback.retry();
        } else {
          console.log('Unexpected API response, cancelling out.');
          faceScanResultCallback.cancel();
          this.configs.onComplete && this.configs.onComplete(false, parameters);
        }
      })
      .catch((e) => {
        console.log('Error on the API validation request', e);
        faceScanResultCallback.cancel();
        this.configs.onComplete && this.configs.onComplete(false);
      });
  }

  onFaceTecSDKCompletelyDone(): void {
    if (process.env.NODE_ENV === 'development') {
      console.log('Liveness session closed.', this);
    }
  }
}
