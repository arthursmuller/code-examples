import path from 'path';
import simpleGit from 'simple-git';

const workingDir = path.resolve();
const git = simpleGit(workingDir);

export const getDate = (): string => {
  const options = {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
    hour: 'numeric',
    minute: 'numeric',
    second: 'numeric',
    hour12: false,
    timeZone: 'America/Sao_Paulo',
  };
  const date = `${Intl.DateTimeFormat('pt-BR', options).format(new Date())}`;

  const [month, day, ...rest] = date.split('/');

  return `${day}/${month}/${rest}`;
};

export const getCurrentBranch = async (): Promise<string> => {
  const branch = await git.branch();

  return branch.current;
};

export const getCurrentCommit = (): Promise<string> => {
  return new Promise((resolve, reject) => {
    git.revparse(['HEAD'], (err, hash = '') => {
      if (err) {
        reject(err);
        return;
      }
      resolve(hash.slice(0, 7));
    });
  });
};
