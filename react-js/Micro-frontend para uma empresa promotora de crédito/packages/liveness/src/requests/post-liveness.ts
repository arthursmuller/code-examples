import { Config } from '../config';
import { FaceTecSDK } from '../sdk/FaceTecSDK';

export interface LivenessRequestModel {
  faceScan: string;
  auditTrailImage: string;
  lowQualityAuditTrailImage: string;
  sessionId: string;
}

export type PostLivenessFn = (
  params: LivenessRequestModel,
  deviceKeyIdentifier?: string,
  sessionId?: string,
) => Promise<{ success: boolean }>;

export const postLiveness: PostLivenessFn = async (
  params: LivenessRequestModel,
  deviceKeyIdentifier?: string,
  sessionId?: string,
) => {
  const myHeaders = new Headers();

  myHeaders.append('Content-Type', 'application/json');
  myHeaders.append(
    'X-Device-Key',
    deviceKeyIdentifier || Config.DeviceKeyIdentifier,
  );
  myHeaders.append(
    'X-User-Agent',
    FaceTecSDK.createFaceTecAPIUserAgentString(sessionId),
  );

  const result = await fetch(`${Config.BaseURL}/liveness-3d`, {
    method: 'POST',
    headers: myHeaders,
    body: JSON.stringify(params),
  });

  const resp = JSON.parse(await result.text());

  return resp;
};
