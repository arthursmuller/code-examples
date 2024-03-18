export const enumKeyFor = (
  enumType: { [Key: string]: string | number },
  value: string | number,
): string | undefined =>
  Object.keys(enumType).find((key) => enumType[key] === value);
