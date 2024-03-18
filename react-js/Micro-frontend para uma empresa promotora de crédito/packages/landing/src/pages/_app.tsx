import { useMount } from 'react-use';
import Head from 'next/head';
import { AppProps } from 'next/app';
import { hotjar } from 'react-hotjar';
import Script from 'next/script';
import { printBuildInfo, printAPIInfo } from 'print-build-info';
import { AppProvider } from 'app';
import { FeatureToggleContextProvider } from 'app/feature-toggle/feature-toggle.context';

import { setApiBaseUrl } from '@pcf/core';

setApiBaseUrl(process.env.NEXT_PUBLIC_PLATAFORMA_CLIENTE_API);

const App = ({ Component, pageProps: { flags, about, ...pageProps } }: AppProps) => {
  useMount(() => {
    printBuildInfo();
    printAPIInfo(about);

    if (process.env.NEXT_PUBLIC_HOTJAR_ID)
      hotjar.initialize(+process.env.NEXT_PUBLIC_HOTJAR_ID, undefined);
  });

  return (
    <>
      <Script
        strategy="lazyOnload"
        dangerouslySetInnerHTML={{
          __html: `
          ua2 = window.navigator.userAgent;
          msie = ua2.indexOf('MSIE ');
          trident = ua2.indexOf('Trident/');

          // IE 10 or older || IE 11
          if (msie > 0 || trident > 0) {
            window.location.href = window.location.href + 'ie-not-supported.html';
          }
        `,
        }}
      />

      <Script
        strategy="lazyOnload"
        crossOrigin="anonymous"
        src="//script.crazyegg.com/pages/scripts/0069/6630.js"
      />

      <Script
        strategy="lazyOnload"
        crossOrigin="anonymous"
        src="https://www.googletagmanager.com/gtag/js?id=G-ZRYF3MHFBB"
      />

      <Script
        strategy="lazyOnload"
        dangerouslySetInnerHTML={{
          __html: `
              window.dataLayer = window.dataLayer || [];
              function gtag(){dataLayer.push(arguments);}
              gtag('js', new Date());
              gtag('config', 'G-ZRYF3MHFBB');
            `,
        }}
      />

      <style global jsx>{`
        html,
        body,
        body > div:first-child,
        div#__next,
        div#__next > div {
          height: 100%;
        }
      `}</style>

      <Head>
        <title>Simulação de Empréstimo Consignado - Bem Promotora</title>
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <meta
          property="og:title"
          content="Simulação de Empréstimo Consignado - Bem Promotora"
          key="title"
        />
        <meta
          property="og:description"
          content="O Crédito Consignado é uma opção para quem precisa de dinheiro extra de forma rápida e sem burocracia. Conheça a Bem Promotora e Faça sua Simulação Agora!"
          key="description"
        />
      </Head>
      <FeatureToggleContextProvider flags={flags}>
        <AppProvider>
          <Component {...pageProps} />
        </AppProvider>
      </FeatureToggleContextProvider>
    </>
  );
};

export default App;
