<template>
  <div>
    <h1>This is an about page</h1>
    <card class="mt-3 shadow-8" v-for="i in 40" :key="i">
      <template #content>
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Itaque repudiandae quae distinctio
        non ipsa. Earum vel a eum debitis fuga enim accusantium omnis ad voluptatem repudiandae
        unde, atque et ex, saepe sint incidunt. Eaque, quae animi. Sapiente dolorum sit non laborum
        provident, itaque laboriosam iusto est suscipit cum voluptates in?
      </template>
    </card>
  </div>
</template>
