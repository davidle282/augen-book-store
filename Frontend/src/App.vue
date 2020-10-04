<template>
  <div id="app">
    <Header />
    <book-list />
  </div>
</template>

<script>
import BookList from "./components/BookList.vue";
import Header from "./components/Header";
export default {
  name: "App",
  components: {
    BookList,
    Header,
  },
};
</script>

<style lang="scss">
@import "./assets/scss/main.scss";
</style>
