import shell from 'shelljs';

import pkg from './package.json';

import { getCurrentBranch, getCurrentCommit, getDate } from '../../build-utils';

const args = process.argv.slice(2);
const isDev = args.includes('--dev');

(async () => {
  try {
    if (isDev) {
      const branch = await getCurrentBranch();
      const commit = await getCurrentCommit();
      
      shell.env.REACT_APP_GIT_BRANCH = branch;
      shell.env.REACT_APP_GIT_COMMIT = commit;
    } else {
      //... populate from process.env 
    }

    const buildTime = getDate();
    shell.env.REACT_APP_BUILD_TIME = buildTime;
    shell.env.REACT_APP_VERSION = pkg.version;

    if (process.argv.some((arg) => arg.includes('mirage'))) {
      shell.env.REACT_APP_START_MIRAGEJS = 'true';
    }

    if (isDev) {
      shell.exec('yarn react-scripts start');
    } else {
      shell.exec('yarn react-scripts build');
    }
  } catch (err) {
    shell.echo('Failed to gather build info', err);
    shell.exit(1);
  }
})();
