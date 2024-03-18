module.exports = {
  siteUrl: process.env.PLATAFORMA_CLIENTE_LANDING,
  generateRobotsTxt: true,
  transform: async (config, path) => {
    const pathConfig = {
      loc: path.replace('home/', ''),
      changefreq: config.changefreq,
      priority: config.priority,
      lastmod: config.autoLastmod ? new Date().toISOString() : undefined,
      alternateRefs: config.alternateRefs || [],
    }

    if (path.includes('landing')) {
      return {
        ...pathConfig,
        loc: process.env.PLATAFORMA_CLIENTE_LANDING,
        priority: 1,
      }
    }

    return pathConfig;
  },
}