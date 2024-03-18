//https://github.com/floffah/esbuild-plugin-d.ts

//https://jamesthom.as/2021/05/setting-up-esbuild-for-typescript-libraries/
// declarationonly não faz typecheking

// https://github.com/evanw/esbuild/issues/1010
// target es5

// var pkg = require('./package.json');
// && tsc --emitDeclarationOnly --outDir build
// const { dtsPlugin } = require("esbuild-plugin-d.ts");

var svgrPlugin = require('esbuild-plugin-svgr');
var esbuild = require('esbuild');
var path = require('path');
// var globby = require('globby');
var rootPath = path.resolve(__dirname);
const fs = require('fs');


// https://github.com/sindresorhus/globby/issues/179

// async function _findEntryPointsGlobby() {
//   const files = await globby([
//     path.resolve(rootPath, 'src/**/*.ts'),
//     path.resolve(rootPath, 'src/**/*.tsx'),
//   ]);
//   return files.filter((file) => {
//     return (
//       !file.includes('__fixtures__') &&
//       !file.endsWith('.workshop.tsx') &&
//       !file.endsWith('.test.ts') &&
//       !file.endsWith('.stories.tsx') &&
//       !file.endsWith('.test.tsx')
//     );
//   });
// }


function _findEntryPoints() {
  const isDirectory = dirPath => fs.statSync(dirPath).isDirectory();
  const getDirectories = dirPath =>
    fs.readdirSync(dirPath).map(name => path.join(dirPath, name)).filter(isDirectory);

  const isFile = dirPath => fs.statSync(dirPath).isFile();
  const getFiles = dirPath =>
    fs.readdirSync(dirPath).map(name => path.join(dirPath, name)).filter(isFile);

  const getFilesRecursive = (dirPath) => {
    let dirs = getDirectories(dirPath);
    let files = dirs
        .map(dir => getFilesRecursive(dir))
        .reduce((a,b) => a.concat(b), []);
    return files.concat(getFiles(dirPath));
  };

  return getFilesRecursive(`${rootPath}/src`).filter(file =>
    (!file.includes('__fixtures__') &&
    !file.endsWith('.workshop.tsx') &&
    !file.endsWith('.test.ts') &&
    !file.endsWith('.stories.tsx') &&
    !file.endsWith('.json') &&
    !file.endsWith('.test.tsx')));
  }

// minify: true,
// metafile: true,
// preserveSymlinks: true,
// platform: 'browser',

function main() {
  const entryPoints =_findEntryPoints();

   esbuild
      .build({
        entryPoints: entryPoints,
        bundle: false,
        // outfile: 'build/index.js',
        // minify: true,
        outdir: 'build',
        format: 'esm',
        sourcemap: 'external',
        //https://bundle-buddy.com/bundle
        metafile: true,
        // external: [
        //   ...Object.keys(pkg.dependencies),
        //   ...Object.keys(pkg.peerDependencies || {}),
        //   // remove json of bundle
        //   // "*.json"
        // ],
        inject: ['./react-shim.js'],
        plugins: [
          // dtsPlugin(),
          svgrPlugin(),
        ],
      })
      .then((entryPoints) => {
        require('fs').writeFileSync(
          'meta.json',
          JSON.stringify(entryPoints.metafile),
        );
      })
      .catch(() => process.exit(1));
}

main();
