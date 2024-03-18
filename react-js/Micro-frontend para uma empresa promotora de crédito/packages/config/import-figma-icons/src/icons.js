const path = require('path');
const fs = require('fs');
const { exec } = require('child_process');
const { pascalCase } = require('change-case');
const Promise = require('bluebird');
const svgo = require('svgo');

const { api, svgoConfig } = require('./utils');
const { getNodeChildren } = require('./utils/api');

const IconsDir =
  path.resolve(process.cwd()) + process.env.REACT_APP_ICONS_DIRECTORY;

const getIconFolderPath = (name) => path.resolve(IconsDir, 'svgs');

const writeFile = Promise.promisify(fs.writeFile);

/**
 * clear icons dir except index.tsx, icons.stories.tsx, README.md files
 *
 */
const clearIconsDir = () => {
  exec(`rm -rf ${IconsDir} -v !(index.tsx) !(icons.stories.tsx) !(README.md)`);
  exec(`rm -rf ${IconsDir}/svgs`);
};

/**
 * generate icon component
 * [iconName].jsx and [iconName].svg  files
 *
 * @param iconNode
 * @return {Promise<void>}
 */
const generateIcon = async (iconNode) => {
  const iconName = iconNode.name;
  const iconFolderPath = getIconFolderPath(iconName);

  if (
    process.argv.includes('--clear') ||
    !fs.existsSync(path.resolve(iconFolderPath, `${iconName}.svg`))
  ) {
    const iconUrl = await api.getSvgImageUrl(iconNode.id);

    if (!fs.existsSync(iconFolderPath)) {
      fs.mkdirSync(iconFolderPath);
    }

    const { data: iconContent } = await api.getImageContent(iconUrl);

    const isLogo = iconNode.name.toLowerCase().includes('logo');

    const { data: optimizedIconContent } = await svgo.optimize(
      iconContent,
      isLogo ? svgoConfig.svgWithColors : svgoConfig.svgWithoutColors,
    );

    await Promise.all([
      writeFile(
        path.resolve(iconFolderPath, `${iconName}.svg`),
        optimizedIconContent,
        { encoding: 'utf8' },
      ),
    ]);

    console.log(`${iconName} was written success!`);
  }
};

/**
 * generate icons components
 *
 * @param {[Object]} iconNodesArr - array of icon nodes from frame
 * @return {Promise<void>}
 */
const generateIcons = async (iconNodesArr) => {
  await Promise.map(iconNodesArr, generateIcon, {
    concurrency: Number.parseInt(10),
  });
};

/**
 * generate index.js with imports
 *
 * @param iconNodesArr - array of icon nodes from frame
 */
const generateImports = (iconNodesArr) => {
  const fileWithImportsPath = path.resolve(IconsDir, 'index.tsx');

  const importsContent = iconNodesArr
    .filter((iconNode) => !!iconNode)
    .map((iconNode) => {
      const iconName = iconNode.name;

      return `export { ReactComponent as ${pascalCase(
        iconName.replace('icon-', ''),
      )}Icon } from './svgs/${iconName}.svg';`;
    })
    .join('\n');

  fs.writeFileSync(fileWithImportsPath, importsContent.concat('\n'), {
    encoding: 'utf8',
  });

  console.log(`imports was written success!`);
};

const main = async () => {
  if (process.argv.includes('--clear')) {
    clearIconsDir();
  }

  const iconNodesArr = await api.getNodeChildren(
    process.env.REACT_APP_FRAME_WITH_ICONS_ID,
  );

  const svgs = iconNodesArr.map((node) => {
    let visibleIcon = node.children.find((c) => {
      if (c.visible === undefined) {
        return true;
      }
      return false;
    });

    if (visibleIcon.children && visibleIcon.children.length) {
      if (visibleIcon.children[0].name === 'Vector') {
        return visibleIcon;
      } else {
        return visibleIcon.children[0];
      }
    } else {
      return visibleIcon;
    }
  });

  await Promise.all([generateIcons(svgs), generateImports(svgs)]);
};

module.exports = main;
