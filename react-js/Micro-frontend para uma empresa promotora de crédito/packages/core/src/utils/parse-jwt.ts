export interface DecodedJwtData {
  unique_name: string;  //eslint-disable-line
  nameid: string;
  nbf: number;
  exp: number;
  iat: number;
}

export interface DecodedJwtGoogleData {
  email: string;
  family_name: string; //eslint-disable-line
  given_name: string; //eslint-disable-line
  picture: string;
}

export const parseJwt = (
  jwt: string,
): DecodedJwtData | DecodedJwtGoogleData => {
  const [, base64Url] = jwt.split('.');
  const base64 = base64Url.replace('-', '+').replace('_', '/');

  return JSON.parse(window.atob(base64));
};
