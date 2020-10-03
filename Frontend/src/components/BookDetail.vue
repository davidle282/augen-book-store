<template>
  <div class="container">
    <p>Dt</p>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "BookDetail",
  data() {
    return {
      book: [],
    };
  },
  mounted: function mounted() {
    this.getBook();
  },
  methods: {
    getBook: function getBook() {
      //   axios.get("https://localhost:44302/api/book").then((res) => {
      //     this.books = res.data;
      //     console.log(res.data);
      //   });
      console.log("detail");
    },
  },
};
</script>
