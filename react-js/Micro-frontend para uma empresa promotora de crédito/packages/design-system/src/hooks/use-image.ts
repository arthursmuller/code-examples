import { useMemo } from 'react';

import { useSupportWebp, WebpSupport } from './use-support-webp';

export function useImage(webp: string, alternative: string): string | null {
  const supportsWebP = useSupportWebp();

  const src = useMemo(() => {
    if (supportsWebP === WebpSupport.CHECKING) return null;

    return supportsWebP === WebpSupport.NOT_SUPPORTED ? alternative : webp;
  }, [supportsWebP, webp, alternative]);

  return src;
}
