import Script from 'next/script';

import Campanha from 'features/campanha';

const CampanhaPage = () => (
  <>
    <Script
      strategy="lazyOnload"
      src="https://www.googletagmanager.com/gtag/js?id=AW-956697505"
    />

    <Script
      strategy="lazyOnload"
      dangerouslySetInnerHTML={{
        __html: `
            window.dataLayer = window.dataLayer || [];
            function gtag(){dataLayer.push(arguments);}
            gtag('js', new Date());
            gtag('config', 'AW-956697505');
          `,
      }}
    />
    <Campanha />
  </>
);

export default CampanhaPage;
