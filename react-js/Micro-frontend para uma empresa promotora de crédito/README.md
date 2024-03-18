*This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).*

<img width="300" src="https://www.bempromotora.com.br/wp-content/uploads/2019/12/logo-bem.png" alt="Bem Promotora Logo" />

<hr/>

## Plataforma Cliente Final - FrontEnd

É a solução que faz a apresentação das telas e a conexão com projeto de Backend
pelo projeto e as demais integrações necessárias.

Para maiores documentações, acesse a área de [conhecimento da Bem](https://conhecimento.bempromotora.com.br/display/Pj/Projeto+Plataforma+Cliente+Final).

## Ambientes

- [PROD](https://clientefinalfront.azurewebsites.net/) - Ambiente de Produção -
- [DEV](https://clientefinalfront-develop.azurewebsites.net/) - Ambiente de Testes - [![Build Status](https://dev.azure.com/tecbem/Plataforma%20Cliente%20Final%20-%20Front/_apis/build/status/clientefinalfront%20-%20develop%20-%20CI?branchName=dev)](https://dev.azure.com/tecbem/Plataforma%20Cliente%20Final%20-%20Front/_build/latest?definitionId=10&branchName=dev)
- [HOMOLOG](https://clientefinalfront-homolog.azurewebsites.net/): Ambiente de Homologação - [![Build Status](https://dev.azure.com/tecbem/Plataforma%20Cliente%20Final%20-%20Front/_apis/build/status/clientefinalfront%20-%20feature%20-%20CI?branchName=dev)](https://dev.azure.com/tecbem/Plataforma%20Cliente%20Final%20-%20Front/_build/latest?definitionId=13&branchName=dev)


## Scripts :robot:

- Executar: `yarn start`
- React testing lib: `yarn test`, `yarn test:watch`
- ESLint: `yarn lint`
- Webpack source map analyzer: `yarn analyze`
- Storybook: `yarn storybook`

## Composition :hammer:

- **Core**: CRA + Typescript + React Hot Loader
- **Quality**: Husky + lint staged + eslint + testing lib, source-map-analyzer
- **Visual**: Chakra, Storybook
- **API**: React Query + Axios + QS
- **Utility**: React Hooks Forms + React Use

## Contribution and structure :book:

####**Branches and commits**:

-   New features / stories:
    -   Master (Prod like) < dev < feature/PCF-[number]
    -   Master (Prod like) < dev < feature/PCF-[number] < dev/[slice-of-change]
-   Hotfix:
    -   Master (Prod like) < dev < hotfix/[change]
    -   Master (Prod like) < dev < hotfix/PCF-[bug-number]

-   Commit message: max 90 chars, add body for more

####**Linting and Testing**:

1. Add tests; (*.test.tsx)
2. If involves new visual components, add a Story for Storybook; (*.stories.tsx)
3. **Husky runs automatically those actions before commits: eslint:fix and test.**

####**Folder structure conventions**:

File Naming:
- kebab-casing: ```kebab-casing.tsx```
- all components as .tsx: ```kebab-casing.tsx```
- all configs as .ts: ```casing-config.ts```
- context aware naming with '.': ```casing.context.ts | casing.tests.ts```
- folders with index.ts exporting all to have clean import paths;
- no logics inside index.ts files.

Folder strucutre:
```
    /.container                              // Nginx configs
    /.internals                              // CRA rewired
    /.storybook                              // Storybook setup
    /public...
    /src
    ├── /api                                  // Requests
    │   ├── /[api-name]
    │   │   ├── /[endpoint-name]
    │   │   ...    [query-name-[type].tsx]
    │   │          [interface.d.ts]
    │   │          index.ts
    │   ├── /common
    │   │   ├── [shared-interface.t.ts]
    │   │   ...
    │   │
    │   └── index.ts
    │
    ├── /app                                // Root level/App specific files
    │   ├── /routes
    │   │   ├──  private.tsx
    │   │   ├──  private.tsx
    │   │   └──  index.tsx
    │   │
    │   ├── app.context.tsx
    │   └── index.tsx
    │
    ├── /common                             // Components shared with all the app
    │   ├── /animations
    │   │   ├──/[animation-name.ts]
    │   │   └── index.tsx
    │   │
    │   ├── /components
    │   │   ├──/[component-name]
    │   │   │   ├── [component-name.tsx]
    │   │   │   ├── [component-name.test.tsx]
    │   │   │   ├── [component-name.stories.tsx]
    │   │   │   ├── [component-props.d.ts]
    │   │   │   └── index.tsx
    │   │   ...
    │   ├── /hocs ...
    │   ├── /hooks ...
    │   ├── /theme
    │       ├──/components
    │       │   ├── [component-name]
    │       │   │    ├── [component-name.stories.tsx]
    │       │   │    ├── [component-props.d.ts]
    │       │   │    └── index.tsx
    │       │   ├── index.tsx
    │       │
    │       ...
    │
    ├── /features
    │   ├── /[feature-name]
    │   │    ├── /components
    │   │    │   ├── [simple-component.tsx]           // Simple Components shared only within the feature
    │   │    │   ├── [another-simple-component.tsx]
    │   │    │   ├── [super-simple-component.tsx]
    │   │    │   ├── ...
    │   │    │   ├── [component-name]           //  Complex Components shared only within the feature
    │   │    │        ├── [component-name.tsx]
    │   │    │        ├── [component-name.test.tsx]
    │   │    │        ├── [component-name.stories.tsx]
    │   │    │        ├── [component-props.d.ts]
    │   │    │        └── index.tsx
    │   │    │   ├── ...
    │   │    │   ├── index.tsx
    │   │    │
    │   │    ├── /utils
    │   │    │   ├── [feature-name.utils.ts]
    │   │    │
    │   │    ├── [feature-entry-component.tsx]
    │   │    ├── [route-one.tsx]             // nested routes
    │   │    ├── [route-two.tsx]
    │   │    ├── [route-three.tsx]
    │   │    ├── [feature.context.tsx]
    │   │    ├── [feature.test.tsx]
    │   │    ├──index.tsx
    │   ...  └── /features                      // Second level: sub features for given feature if necessary
    │            ├── [sub-feature-name]
    │            ...
    │
    └── other setup files...
```

Example for a component:
```
        ├── /menu
        ... ├── components
            │   ├── menu-item.tsx
            │   ├── menu-item-mobile.tsx
            │   └── menu.tsx
            │
            ├── menu.context.tsx
            ├── menu.stories.tsx
            ├── menu.test.tsx
            └── index.tsx
```

####**Imagens**:

1. Exportar em 1x no figma
2. Google jpg compress (https://compressjpeg.com/)
3. Pegar a compressed e jpg to webp (https://cloudconvert.com/jpeg-to-webp)
