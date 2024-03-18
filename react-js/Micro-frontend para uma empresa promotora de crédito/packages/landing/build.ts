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

      shell.env.NEXT_PUBLIC_GIT_BRANCH = branch;
      shell.env.NEXT_PUBLIC_GIT_COMMIT = commit;
    } else {
      //... populate from process.env 
    }

    const buildTime = getDate();
    shell.env.NEXT_PUBLIC_VERSION = pkg.version;
    shell.env.NEXT_PUBLIC_BUILD_TIME = buildTime;

    if (isDev) {
      shell.exec('next dev -p 3001');
    } else {
      shell.exec('next build');
    }
  } catch (err) {
    shell.echo('Failed to gather build info', err);
    shell.exit(1);
  }
})();
