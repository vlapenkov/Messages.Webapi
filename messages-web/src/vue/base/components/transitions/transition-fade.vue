<template>
  <transition
    enter-active-class="fadein animation-duration-200"
    leave-active-class="fadeout animation-duration-100"
    mode="out-in"
    appear
  >
    <slot></slot>
  </transition>
</template>
