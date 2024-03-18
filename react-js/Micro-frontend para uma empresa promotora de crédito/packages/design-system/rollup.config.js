import commonjs from '@rollup/plugin-commonjs';
import resolve from '@rollup/plugin-node-resolve';
import peerDepsExternal from 'rollup-plugin-peer-deps-external';
import typescript from 'rollup-plugin-typescript2';
import image from '@rollup/plugin-image';
import json from '@rollup/plugin-json';
import css from 'rollup-plugin-css-only';
import svgr from '@svgr/rollup';
// import { terser } from 'rollup-plugin-terser';
// import analyze from 'rollup-plugin-analyzer';

import packageJson from './package.json';

export default {
  input: './src/index.ts',
  output: [
    {
      file: packageJson.main,
      format: 'cjs',
      sourcemap: true,
      plugins: [
        // terser(),
        // analyze()
        //
      ],
    },
    {
      file: packageJson.module,
      format: 'esm',
      sourcemap: true,
      plugins: [
        // terser(),
        // analyze()
      ],
    },
  ],
  plugins: [
    peerDepsExternal(),
    resolve({ preferBuiltins: true }),
    commonjs({ sourceMap: false }),
    css({ output: 'bundle.css' }),
    typescript(),
    image(),
    json(),
    svgr(),
  ],
  external: [/node_modules/],
};
