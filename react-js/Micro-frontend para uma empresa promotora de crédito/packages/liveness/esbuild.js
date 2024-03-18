const esbuild = require('esbuild');
const path = require('path');
const globby = require('globby');
const rootPath = path.resolve(__dirname);
const fse = require('fs-extra');
const fs = require('fs');

// https://github.com/sindresorhus/globby/issues/179
async function _findEntryPoints() {
  const files = await globby([
    path.resolve(rootPath, 'src/**/*.ts'),
    // path.resolve(rootPath, 'src/**/sdk/**/*'),
  ]);

  return files.filter((file) => !file.includes('/sdk/'));
}

function _findEntryPointsWindows() {
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
    !file.includes('/sdk/'))
  }

function main() {
  const result = _findEntryPointsWindows();

  esbuild
    .build({
      entryPoints: result,
      bundle: false,
      // outfile: 'build/index.js',
      // minify: true,
      outdir: 'build',
      format: 'esm',
      sourcemap: 'external',
      metafile: true,
      inject: ['./react-shim.js'],
      loader: { '.data': 'binary' },
    })
    .then((result) => {
      require('fs').writeFileSync(
        'meta.json',
        JSON.stringify(result.metafile),
      );

      fse.copySync(
        'src/sdk',
        'build/sdk',
        { overwrite: true },
        function (err) {
          if (err) {
            console.error('Error copying SDK content', err);
          }
        },
      );
    })
    .catch((e) => {
      console.log('Error bundling Liveness package', e);
      process.exit(1);
    });
}

main();
