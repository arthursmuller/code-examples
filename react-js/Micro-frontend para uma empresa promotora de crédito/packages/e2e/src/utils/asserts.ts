import { expect, Request } from '@playwright/test';

export const assertRequestPayload = (
  req: Request,
  expectedPayload: any,
): void => {
  expect(expectedPayload).toStrictEqual(req.postDataJSON());
};

export const assertResponse = async (
  req: Request,
  expectedResponse: any,
): Promise<void> => {
  const response = await (await req.response()).json();
  expect(expectedResponse).toStrictEqual(response);
};
