import { useEffect } from 'react';

import { useSessionStorage } from 'react-use';
import webPCheck from 'supports-webp';

export enum WebpSupport {
  CHECKING = 0,
  OK = 1,
  NOT_SUPPORTED = 2,
}

const WEBP_SUPPORT_KEY = '@plataformacliente/supports-webp';

export function useSupportWebp(): WebpSupport {
  const [supportsWebP, setWebPSupport] = useSessionStorage<WebpSupport>(
    WEBP_SUPPORT_KEY,
    WebpSupport.CHECKING,
  );

  useEffect(() => {
    const checkForSupport = async (): Promise<void> => {
      const browserSupportsWebP = await webPCheck;

      setWebPSupport(
        browserSupportsWebP ? WebpSupport.OK : WebpSupport.NOT_SUPPORTED,
      );
    };

    if (supportsWebP === WebpSupport.CHECKING) {
      checkForSupport();
    }
  }, [webPCheck]); // eslint-disable-line react-hooks/exhaustive-deps

  return supportsWebP;
}
