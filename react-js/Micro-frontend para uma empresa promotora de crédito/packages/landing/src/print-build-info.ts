function print(title: string | undefined, info: string | undefined): void {
  // eslint-disable-next-line
  console.log(
    `%c ${title} %c ${info} %c`,
    `background: #E15100; padding: 1px; border-radius: 3px 0 0 3px;  color: #fff`,
    `background: #2B3EA1; padding: 1px; border-radius: 0 3px 3px 0;  color: #fff`,
    'background:transparent',
  );
}

export const printBuildInfo = (): void => {
  print('Version', process.env.NEXT_PUBLIC_VERSION);
  print('Branch', process.env.NEXT_PUBLIC_GIT_BRANCH);
  print('Commit', process.env.NEXT_PUBLIC_GIT_COMMIT);
  print('Build Time', process.env.NEXT_PUBLIC_BUILD_TIME);
};

export const printAPIInfo = (apiInfo): void => {
  if (process.env.NEXT_PUBLIC_IS_PRODUCTION !== 'true') {
    console.info(process.env.NEXT_PUBLIC_PLATAFORMA_CLIENTE_API);
    console.info(apiInfo);
  }
};
