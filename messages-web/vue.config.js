const { defineConfig } = require('@vue/cli-service');
const autoImport = require('unplugin-vue-components/webpack');
const { PrimeVueResolver } = require('unplugin-vue-components/resolvers');

module.exports = defineConfig({
  transpileDependencies: true,
  devServer: {
    client: {
      overlay: {
        warnings: false,
        errors: true,
      },
    },
  },
  configureWebpack: {
    plugins: [
      autoImport({
        dirs: ['src/vue'],
        resolvers: [PrimeVueResolver({})],
      }),
    ],
  },
});
