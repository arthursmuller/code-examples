module.exports = {
  presets: [
    '@react/cli-plugin-babel/preset',
    ['@babel/preset-env', {targets: {node: 'current'}}],
    // '@babel/preset-typescript',
  ],
  plugins: [
    'babel-plugin-root-import',
    {
      rootPathSuffix: 'src',
      rootPathPrefix: '~',
    }
  ]
}