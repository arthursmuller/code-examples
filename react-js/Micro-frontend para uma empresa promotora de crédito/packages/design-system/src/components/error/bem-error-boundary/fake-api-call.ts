let count = 0;

export const fakeApiCall = async (): Promise<string> => {
  await new Promise((r) => setTimeout(r, 1000));

  if (count < 3) {
    count += 1;
    throw new Error('Error message');
  }

  return 'Ok!';
};
