<template>
  <div class="hello">
    <card>
      <template #title>
        <h3>{{ msg }}</h3>
      </template>
      <template #content>
        <p>
          For a guide and recipes on how to configure / customize this project,<br />
          check out the
          <a href="https://cli.vuejs.org" target="_blank" rel="noopener">vue-cli documentation</a>.
        </p>
      </template>
    </card>
    <card class="mt-4">
      <template #title> Installed CLI Plugins </template>
      <template #content>
        <ul>
          <li>
            <a
              href="https://github.com/vuejs/vue-cli/tree/dev/packages/%40vue/cli-plugin-babel"
              target="_blank"
              rel="noopener"
              >babel</a
            >
          </li>
          <li>
            <a
              href="https://github.com/vuejs/vue-cli/tree/dev/packages/%40vue/cli-plugin-router"
              target="_blank"
              rel="noopener"
              >router</a
            >
          </li>
          <li>
            <a
              href="https://github.com/vuejs/vue-cli/tree/dev/packages/%40vue/cli-plugin-eslint"
              target="_blank"
              rel="noopener"
              >eslint</a
            >
          </li>
          <li>
            <a
              href="https://github.com/vuejs/vue-cli/tree/dev/packages/%40vue/cli-plugin-unit-jest"
              target="_blank"
              rel="noopener"
              >unit-jest</a
            >
          </li>
          <li>
            <a
              href="https://github.com/vuejs/vue-cli/tree/dev/packages/%40vue/cli-plugin-e2e-cypress"
              target="_blank"
              rel="noopener"
              >e2e-cypress</a
            >
          </li>
          <li>
            <a
              href="https://github.com/vuejs/vue-cli/tree/dev/packages/%40vue/cli-plugin-typescript"
              target="_blank"
              rel="noopener"
              >typescript</a
            >
          </li>
        </ul>
      </template>
    </card>
    <card class="mt-4">
      <template #title>Essential Links</template>
      <template #content>
        <ul>
          <li><a href="https://vuejs.org" target="_blank" rel="noopener">Core Docs</a></li>
          <li><a href="https://forum.vuejs.org" target="_blank" rel="noopener">Forum</a></li>
          <li>
            <a href="https://chat.vuejs.org" target="_blank" rel="noopener">Community Chat</a>
          </li>
          <li><a href="https://twitter.com/vuejs" target="_blank" rel="noopener">Twitter</a></li>
          <li><a href="https://news.vuejs.org" target="_blank" rel="noopener">News</a></li>
        </ul>
      </template>
    </card>
    <card class="mt-4">
      <template #title>Ecosystem</template>
      <template #content>
        <ul>
          <li><a href="https://router.vuejs.org" target="_blank" rel="noopener">vue-router</a></li>
          <li><a href="https://vuex.vuejs.org" target="_blank" rel="noopener">vuex</a></li>
          <li>
            <a
              href="https://github.com/vuejs/vue-devtools#vue-devtools"
              target="_blank"
              rel="noopener"
              >vue-devtools</a
            >
          </li>
          <li>
            <a href="https://vue-loader.vuejs.org" target="_blank" rel="noopener">vue-loader</a>
          </li>
          <li>
            <a href="https://github.com/vuejs/awesome-vue" target="_blank" rel="noopener"
              >awesome-vue</a
            >
          </li>
        </ul>
      </template>
    </card>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'HelloWorld',
  props: {
    msg: String,
  },
});
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
