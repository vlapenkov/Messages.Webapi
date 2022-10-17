import Harlem from '@harlem/core';
import createDevtoolsPlugin from '@harlem/plugin-devtools';
import { Plugin } from 'vue';

export const harlemState: Plugin = {
  install(app) {
    const plugins = [];

    if (process.env.NODE_ENV === 'development') {
      plugins.push(
        createDevtoolsPlugin({
          label: 'State',
        }),
      );
    }

    app.use(Harlem, { plugins });
  },
};
