import { getFeatureFlags } from 'app';
import { GetStaticProps } from 'next';
import { getAboutQueryConfig } from '@pcf/core';

export const getStaticPropsDefault: GetStaticProps = async () => {
  const [about, flags] = await Promise.all([
    getAboutQueryConfig().queryFn(null),
    getFeatureFlags(),
  ])

  return {
    props: {
      ...flags,
      about,
    },
    revalidate: 300,
  };
};
