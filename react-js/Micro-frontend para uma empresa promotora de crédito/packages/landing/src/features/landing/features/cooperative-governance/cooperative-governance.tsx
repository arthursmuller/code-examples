import { FC } from 'react';

import { Box, Flex } from '@chakra-ui/react';
// import axios from 'axios';
// import { useQuery } from 'react-query';

import { CustomHeading } from '@pcf/design-system';

// 20210512120010
// https://www.bempromotora.com.br/wp-json/wp/v2/pages/1704

const fakeData = {
  id: 1704,
  date: '2020-09-16T16:17:47',
  date_gmt: '2020-09-16T19:17:47',
  guid: {
    rendered: 'https://www.bempromotora.com.br/?page_id=1704',
  },
  modified: '2021-03-11T11:40:39',
  modified_gmt: '2021-03-11T14:40:39',
  slug: 'governanca-corporativa',
  status: 'publish',
  type: 'page',
  link: 'https://www.bempromotora.com.br/governanca-corporativa',
  title: {
    rendered: 'Governança Corporativa',
  },
  content: {
    rendered:
      '<p>&nbsp;</p>\n<p><strong>Estatuto e Código de Ética</strong></p>\n<ul>\n<li><a href="https://www.bempromotora.com.br/wp-content/uploads/2020/01/Documento-02.pdf" target="_blank" rel="noopener">Estatuto Social</a></li>\n<li><a href="https://www.bempromotora.com.br/wp-content/uploads/2020/01/Documento-03.pdf" target="_blank" rel="noopener">Código de Conduta e Ética</a></li>\n</ul>\n<p>&nbsp;</p>\n<p><strong>Demonstrações Financeiras</strong></p>\n<ul>\n<li><a href="https://www.bempromotora.com.br/wp-content/uploads/2021/03/DO-BEM-PROMOTORA-28-02-2020.pdf" target="_blank" rel="noopener">Exercício 2019</a></li>\n<li><a href="https://www.bempromotora.com.br/wp-content/uploads/2021/03/DO-BEM-PROMOTORA-05-03-2021-balanço.pdf" target="_blank" rel="noopener">Exercício 2020</a></li>\n</ul>\n<p>&nbsp;</p>\n<p><strong>Assembleias e Atas</strong></p>\n<p style="font-weight: 400; padding-left: 30px;"><span style="text-decoration: underline;">Assembleia Geral Ordinária</span></p>\n<ul>\n<li><a href="https://www.bempromotora.com.br/wp-content/uploads/2020/09/Documento-02.pdf" target="_blank" rel="noopener">2020</a></li>\n</ul>\n<p style="font-weight: 400; padding-left: 30px;"><span style="text-decoration: underline;">Assembleia Geral Extraordinária</span></p>\n<ul>\n<li><a href="https://www.bempromotora.com.br/wp-content/uploads/2020/09/Documento-03.pdf" target="_blank" rel="noopener">2020</a></li>\n</ul>\n<p>&nbsp;</p>\n',
    protected: false,
  },
  excerpt: {
    rendered:
      '<p>&nbsp; Estatuto e Código de Ética Estatuto Social Código de Conduta e Ética &nbsp; Demonstrações Financeiras Exercício 2019 Exercício 2020 &nbsp; Assembleias e Atas Assembleia Geral<span class="excerpt-hellip"> […]</span></p>\n',
    protected: false,
  },
  author: 10,
  featured_media: 0,
  parent: 0,
  menu_order: 0,
  comment_status: 'closed',
  ping_status: 'closed',
  template: '',
  meta: [],
  _links: {
    self: [
      {
        href: 'https://www.bempromotora.com.br/wp-json/wp/v2/pages/1704',
      },
    ],
    collection: [
      {
        href: 'https://www.bempromotora.com.br/wp-json/wp/v2/pages',
      },
    ],
    about: [
      {
        href: 'https://www.bempromotora.com.br/wp-json/wp/v2/types/page',
      },
    ],
    author: [
      {
        embeddable: true,
        href: 'https://www.bempromotora.com.br/wp-json/wp/v2/users/10',
      },
    ],
    replies: [
      {
        embeddable: true,
        href: 'https://www.bempromotora.com.br/wp-json/wp/v2/comments?post=1704',
      },
    ],
    'version-history': [
      {
        count: 5,
        href: 'https://www.bempromotora.com.br/wp-json/wp/v2/pages/1704/revisions',
      },
    ],
    'predecessor-version': [
      {
        id: 1807,
        href: 'https://www.bempromotora.com.br/wp-json/wp/v2/pages/1704/revisions/1807',
      },
    ],
    'wp:attachment': [
      {
        href: 'https://www.bempromotora.com.br/wp-json/wp/v2/media?parent=1704',
      },
    ],
    curies: [
      {
        name: 'wp',
        href: 'https://api.w.org/{rel}',
        templated: true,
      },
    ],
  },
};

export const CooperativeGovernane: FC = () => {
  // useQuery('governanca-corporativa', {
  //   queryFn: () => {
  //     return axios.get(
  //       'https://www.bempromotora.com.br/wp-json/wp/v2/pages/1704',
  //     );
  //   },
  //   onError: (error) => {
  //     console.log(error);
  //   },
  //   onSuccess: (data) => {
  //     console.log(data);
  //   },
  //   useErrorBoundary: false,
  // });

  return (
    <Flex
      w="100%"
      padding={10}
      flexDir="column"
      sx={{
        p: {
          marginBottom: '10px',
        },
        a: {
          color: 'primary.regular',
        },
        ul: {
          margin: '0 0 15px 30px',
        },
        'ul li': {
          marginBottom: '10px',
        },
      }}
    >
      <CustomHeading
        as="h3"
        textStyle="headline2"
        textAlign="start"
        color="secondary.regular"
      >
        {fakeData.title.rendered}
      </CustomHeading>
      <Box dangerouslySetInnerHTML={{ __html: fakeData.content.rendered }} />
    </Flex>
  );
};
