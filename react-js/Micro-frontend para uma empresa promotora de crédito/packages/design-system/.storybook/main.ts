// @ts-ignore
const path = require('path');
const toPath = (_path) => path.join(process.cwd(), _path);

module.exports = {
  addons: ['@storybook/addon-essentials'],
  stories: ['../src/**/*.stories.tsx'],
  //https://github.com/styleguidist/react-docgen-typescript/issues/356
  typescript: {
    reactDocgen: 'none',
  },
  webpackFinal: async (config) => {
    const fileLoaderRule = config.module.rules.find(rule => rule.test.test('.svg'));
    // remove default loader
    fileLoaderRule.exclude = /\.svg$/;
    // add svgr to understand { ReactComponent as SVG }
    config.module.rules.push({
      test: /\.svg$/,
      use: ["@svgr/webpack", "url-loader"],
    });

    return {
      ...config,
      resolve: {
        ...config.resolve,
        modules: [
          ...(config.resolve.modules || []),
          path.resolve(__dirname, "../src"),
        ],
        alias: {
          ...config.resolve.alias,
          '@emotion/core': toPath('../../node_modules/@emotion/react'),
          'emotion-theming': toPath('../../node_modules/@emotion/react'),
        },
      },
      plugins: config.plugins.filter((plugin) => {
        if (plugin.constructor.name === 'ESLintWebpackPlugin') {
          return false;
        }
        return true;
      }),
    };
  },
};
