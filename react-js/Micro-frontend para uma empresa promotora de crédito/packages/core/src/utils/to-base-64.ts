export const toBase64 = async (
  file: File,
  trimFormat = true,
): Promise<string> =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onerror = (error) => reject(error);
    reader.onload = () => {
      let result = reader.result as string;

      if (trimFormat) {
        [, result] = result.split('base64,');
      }

      resolve(result);
    };
  });

export const base64WithoutContentType = (fileString: string): string => {
  let file: string = fileString;
  const base64Sep = 'base64,';

  if (fileString.indexOf(base64Sep) > -1) {
    file = fileString.split(base64Sep).pop() || '';
  }

  return file;
};
