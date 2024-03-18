export const openLink = (url: string, newTab?: boolean): void => {
  const tab = document.createElement('a');
  tab.setAttribute('href', url);

  if (newTab) tab.setAttribute('target', '_blank');

  tab.click();
};
