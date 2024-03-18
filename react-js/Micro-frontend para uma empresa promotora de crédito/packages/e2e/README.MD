# E2E with Playwright

### Setup

- ``yarn`` to install dependencies;
- Raise an app that will be tested; or change ``.env`` file URLs;
- ``npx playwright install`` to install Playwrights's Browsers versions.

### Running

##### general: 

- ``yarn test [[fragment of test description name]]``.

##### for development:

- Create a spec.ts file;
- Add a ``await page.pause()``;
- Run by ``yarn test:debug [[fragment of test description name]]``
- Press record and capture the steps;
- ????
- Profit.
