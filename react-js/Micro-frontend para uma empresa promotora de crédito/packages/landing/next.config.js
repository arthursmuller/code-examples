const withBundleAnalyzer = require('@next/bundle-analyzer')({
  enabled: process.env.ANALYZE === 'true',
})


module.exports = withBundleAnalyzer({
  react: {
    useSuspense: false,
    wait: true
  },

  eslint: {
    ignoreDuringBuilds: true,
  },

  optimizeFonts: false,

  async redirects() {
    return [
      {
        source: '/app',
        destination: process.env.PLATAFORMA_CLIENTE_APP,
        permanent: true,
      },
      {
        source: '/recuperacao-senha',
        destination: `${process.env.PLATAFORMA_CLIENTE_APP}/recuperacao-senha`,
        permanent: true,
      },
      {
        source: '/cadastro',
        destination: `${process.env.PLATAFORMA_CLIENTE_APP}/signup`,
        permanent: true,
      },
      {
        source: '/signup',
        destination: `${process.env.PLATAFORMA_CLIENTE_APP}/signup`,
        permanent: true,
      },
      {
        source: '/encontre-uma-loja-bem',
        destination: `/atendimento`,
        permanent: true,
      }
    ]
  },

  async rewrites() {
    return [
      {
        source: '/',
        destination: '/home/landing',
      },
      {
        source: '/404',
        destination: '/home/landing',
      },
      {
        source: '/:path*',
        destination: '/home/:path*',
      },
    ];
  },

  images: {
    deviceSizes: [480, 768, 992, 1080, 1280, 1920, 2048, 3840],
  },
  reactStrictMode: true,
})
