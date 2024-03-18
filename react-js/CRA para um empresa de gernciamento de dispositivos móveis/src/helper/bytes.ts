import { Bytes } from '../types/util';

type Size = 'Bytes' | 'KB' | 'MB' | 'GB' | 'TB';
type ConverterProps = {
  bytes: Bytes;
  decimals?: number;
  to: Size;
};
type FormatterProps = Omit<ConverterProps, 'to'> & { to?: Size | 'auto' };

const base = 1000;

export function convertBytesTo({
  bytes,
  decimals = 0,
  to,
}: ConverterProps): number {
  const dm = decimals >= 0 ? decimals : 0;
  const converters = {
    Bytes: (b: number) => parseFloat(b.toFixed(dm)),
    KB: (b: number) => parseFloat((b / Math.pow(base, 1)).toFixed(dm)),
    MB: (b: number) => parseFloat((b / Math.pow(base, 2)).toFixed(dm)),
    GB: (b: number) => parseFloat((b / Math.pow(base, 3)).toFixed(dm)),
    TB: (b: number) => parseFloat((b / Math.pow(base, 4)).toFixed(dm)),
  };
  return converters[to](bytes);
}

export function formatBytesTo({
  bytes,
  decimals = 0,
  to = 'auto',
}: FormatterProps) {
  if (bytes == 0) return '0 Bytes';
  if (!bytes) return '';

  const sizes: Size[] = ['Bytes', 'KB', 'MB', 'GB', 'TB'];

  const i = Math.floor(Math.log(bytes) / Math.log(base));
  const size = to === 'auto' ? sizes[i] : to;

  const value = convertBytesTo({ bytes, decimals, to: size });
  return `${value} ${size}`;
}
