const removeStrokeAndFill = {
  name: 'removeAttrs',
  params: { attrs: '(stroke|fill)' },
};

const addFillCurrentColor = {
  name: 'addAttributesToSVGElement',
  params: { attributes: [{ fill: 'currentColor' }] },
};

const plugins = [
  'cleanupAttrs',
  'removeDoctype',
  'removeXMLProcInst',
  'removeComments',
  'removeMetadata',
  'removeTitle',
  'removeDesc',
  'removeUselessDefs',
  'removeEditorsNSData',
  'removeEmptyAttrs',
  'removeHiddenElems',
  'removeEmptyText',
  'removeEmptyContainers',
  // 'removeViewBox: false,
  'cleanupEnableBackground',
  'convertStyleToAttrs',
  'convertColors',
  'convertPathData',
  'convertTransform',
  'removeUnknownsAndDefaults',
  'removeNonInheritableGroupAttrs',
  'removeUselessStrokeAndFill',
  'removeUnusedNS',
  'cleanupIDs',
  'cleanupNumericValues',
  'moveElemsAttrsToGroup',
  'moveGroupAttrsToElems',
  'collapseGroups',
  // 'removeRasterImages: false,
  'mergePaths',
  'convertShapeToPath',
  'sortAttrs',
  'removeDimensions',
];

const svgoConfig = {
  svgWithColors: { plugins },
  svgWithoutColors: {
    plugins: [...plugins, removeStrokeAndFill, addFillCurrentColor],
  },
};

module.exports = svgoConfig;
