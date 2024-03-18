import { useEffect, useState } from 'react';

import { FaceTecSDK } from './sdk/FaceTecSDK';
import { Config } from './config';
import { TEXTS } from './texts';
import {
  LivenessCheckProcessor,
  LivenessCheckProcessorConfigs,
} from './liveness-check-processor';
import { FaceTecSDKStatus } from './sdk/FaceTecPublicApi';
import { getSessionToken, GetSesstionTokenFn } from './requests/get-session';

const ASSETS_PATH = '../liveness-sdk/assets';
const RESOURCES_PATH = '../liveness-sdk/resources';

export interface LivenessProps extends LivenessCheckProcessorConfigs {
  productionKey?: string;
  publicFaceScanEncryptionKey?: string;
  sessionRequestFn?: GetSesstionTokenFn;
}

export const useLiveness = ({
  deviceKeyIdentifier,
  publicFaceScanEncryptionKey,
  productionKey,
  onComplete,
  onCancel,

  sessionRequestFn,
  livenessRequestFn,
}: LivenessProps = {}): {
  loaded: boolean;
  loading: boolean;
  hasError: boolean;
  start: () => void;
  getStatus: () => [FaceTecSDKStatus, string];
  load: () => void;
} => {
  const [loaded, setLoaded] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(false);
  const [hasError, setHasError] = useState<boolean>(false);

  function getStatus(): [FaceTecSDKStatus, string] {
    const status = FaceTecSDK.getStatus();
    return [
      status,
      FaceTecSDK.getFriendlyDescriptionForFaceTecSDKStatus(status),
    ];
  }

  function load(): void {
    setLoading(true);

    // Already loaded
    if (getStatus()[0] === 1) {
      setLoaded(true);
      setHasError(false);
      setLoading(false);
      return;
    }

    // Locked out
    if (getStatus()[0] === 7) {
      setLoaded(false);
      setLoading(false);
      setHasError(true);
    }

    FaceTecSDK.setResourceDirectory(RESOURCES_PATH);
    FaceTecSDK.setImagesDirectory(ASSETS_PATH);

    FaceTecSDK.setCustomization(
      Config.retrieveConfigurationWizardCustomization(FaceTecSDK, ASSETS_PATH),
    );

    const sdkProps = {
      id: deviceKeyIdentifier || Config.DeviceKeyIdentifier,
      key: publicFaceScanEncryptionKey || Config.PublicFaceScanEncryptionKey,
      cb: (initializedSuccessfully: boolean) => {
        setLoaded(initializedSuccessfully);
        setHasError(!initializedSuccessfully);
        setLoading(false);

        if (!initializedSuccessfully) {
          console.log(getStatus());
        } else {
          FaceTecSDK.configureLocalization(TEXTS);
        }
      },
    };

    if (process.env.NODE_ENV === 'production') {
      if (!productionKey) {
        throw new Error('Missing productionKey from production environment.');
      } else {
        FaceTecSDK.initializeInProductionMode(
          productionKey,
          sdkProps.id,
          sdkProps.key,
          sdkProps.cb,
        );
      }
    } else {
      FaceTecSDK.initializeInDevelopmentMode(
        sdkProps.id,
        sdkProps.key,
        sdkProps.cb,
      );
    }
  }

  useEffect(load, []);

  async function start(): Promise<void> {
    if (loaded) {
      const sessionToken = (
        await (sessionRequestFn || getSessionToken)(deviceKeyIdentifier)
      )?.sessionToken;

      if (sessionToken) {
        const processor = new LivenessCheckProcessor(sessionToken, {
          onComplete,
          onCancel,
          deviceKeyIdentifier,
          livenessRequestFn,
        });

        if (process.env.NODE_ENV !== 'production') console.log(processor);
      } else {
        setHasError(true);
        throw new Error('Failed to get a session token.');
      }
    }
  }

  return {
    loaded,
    loading,
    hasError,
    start,
    load,
    getStatus,
  };
};
