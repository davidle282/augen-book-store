<template>
  <div class="container">
    <input
      class="form-control mb-4 mt-3"
      type="search"
      placeholder="Search by book title or author"
      name="searchTerm"
      v-on:change="handleTextChange"
    />

    <div class="row">
      <div
        v-for="(book, index) in books"
        v-bind:item="book"
        v-bind:index="index"
        v-bind:key="book.ID"
        class="col-sm-3"
      >
        <div class="card-list mb-4">
          <img class="card-img-top pl-5 pr-5 pt-3" v-bind:src="book.image" />

          <div class="card-body">
            <h5 class="card-title">
              {{ book.title }}
            </h5>
            <p class="card-text">
              {{ book.author }}
            </p>
            <p class="text-secondary">{{ book.publishedDate }}</p>
            <button
              class="btn btn-primary"
              data-toggle="modal"
              data-target="#bookDetailModal"
              v-on:click="selectBook(book.id)"
            >
              Select
            </button>
          </div>
        </div>
      </div>
    </div>
    <book-detail
      v-if="selectedBookId"
      v-bind:bookId="selectedBookId"
    ></book-detail>
  </div>
</template>

<script>
import axios from "axios";
import BookDetail from "./BookDetail.vue";

export default {
  name: "BookList",
  components: {
    BookDetail,
  },
  data() {
    return {
      books: [],
      selectedBookId: "",
    };
  },
  mounted: function mounted() {
    this.getBooks();
  },
  methods: {
    getBooks: function getBooks() {
      axios.get("https://localhost:44302/api/book").then((res) => {
        this.books = res.data;
        console.log(res.data);
      });
    },
    searchBook: function searchBook(books, searchTerm) {
      const result = books.filter(
        (books) =>
          books.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
          books.author.toLowerCase().includes(searchTerm.toLowerCase())
      );
      console.log("~~~search result: ", result);
      this.books = result;
    },
    handleTextChange: function handleTextChange(e) {
      console.log(e.target.value);
      const searchTerm = e.target.value;
      axios.get("https://localhost:44302/api/book").then((res) => {
        this.searchBook(res.data, searchTerm);
      });
    },

    selectBook: function selectBook(id) {
      this.selectedBookId = id;
      console.log(this.selectedBookId);
    },
  },
};
</script>
