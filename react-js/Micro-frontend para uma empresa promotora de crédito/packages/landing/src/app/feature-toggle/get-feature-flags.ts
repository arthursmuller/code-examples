import { flattenFlags, getFeatureFlagsQueryConfig } from '@pcf/core';

import { FeatureFlags, FEATURES } from './feature-toggle';

export const getFeatureFlags = async (): Promise<{ flags: FeatureFlags }> => {
  try {
    const backendFlags = await getFeatureFlagsQueryConfig().queryFn(null);

    let featureToggle: FeatureFlags = {
      ...flattenFlags(backendFlags || []),
    };

    if (process.env.NODE_ENV === 'development') {
      featureToggle = {
        ...featureToggle,
        ...FEATURES,
      };
    }

    return {
      flags: featureToggle,
    };
  } catch (e) {
    console.log('getFeatureFlags:', e.message); // eslint-disable-line

    return {
      flags: {},
    };
  }
};
