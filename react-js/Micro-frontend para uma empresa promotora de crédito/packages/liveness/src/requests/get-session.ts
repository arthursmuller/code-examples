import { Config } from '../config';
import { FaceTecSDK } from '../sdk/FaceTecSDK';

export type GetSesstionTokenFn = (
  deviceKeyIdentifier?: string,
) => Promise<{ sessionToken: string }>;

export const getSessionToken: GetSesstionTokenFn = async (
  deviceKeyIdentifier?: string,
) => {
  const myHeaders = new Headers();

  myHeaders.append('Content-Type', 'application/json');
  myHeaders.append(
    'X-Device-Key',
    deviceKeyIdentifier || Config.DeviceKeyIdentifier,
  );
  myHeaders.append(
    'X-User-Agent',
    FaceTecSDK.createFaceTecAPIUserAgentString(''),
  );

  const result = await fetch(`${Config.BaseURL}/session-token`, {
    method: 'GET',
    headers: myHeaders,
  });

  return JSON.parse(await result.text());
};
