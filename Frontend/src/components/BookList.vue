<template>
  <div class="container">
    <input
      class="form-control mb-2 mt-3"
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
        <div class="card">
          <img
            style="max-height: 220px"
            class="card-img-top pl-5 pr-5 pt-3"
            v-bind:src="book.image"
          />

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
              data-target="#exampleModal"
              v-on:click="selectBook(book.id)"
            >
              Select
            </button>
          </div>
        </div>
      </div>
    </div>
    <div
      class="modal fade"
      id="exampleModal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-lg">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
            <button
              type="button"
              class="close"
              data-dismiss="modal"
              aria-label="Close"
            >
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div class="card mb-3">
              <div class="row no-gutters">
                <div class="col-md-4">
                  <img v-bind:src="bookDetail.image" class="card-img p-3" />

                  <h5 class="card-title">{{ bookDetail.title }}</h5>
                  <p class="card-text">
                    {{ bookDetail.author }}
                  </p>
                  <p class="card-text">
                    <small class="text-muted">Published Date</small>
                  </p>
                </div>
                <div class="col-md-8">
                  <div class="card-body">
                    <p class="text-justify">
                      <small> {{ bookDetail.description }}</small>
                    </p>
                  </div>
                </div>
              </div>
            </div>
            <p class="text-left">Ship via</p>
            <hr />
            <div class="row">
              <div
                class="col-sm-4"
                v-for="(service, index) in deliveryServices"
                v-bind:item="service"
                v-bind:index="index"
                v-bind:key="index"
              >
                <button
                  v-if="service.deliveryService === 'Motobike'"
                  class="btn btn-success"
                  v-on:click="setService(service.deliveryService)"
                >
                  {{ service.deliveryService }}(${{ service.deliveryCost }})
                </button>
                <button
                  v-else-if="service.deliveryService === 'Train'"
                  class="btn btn-primary"
                  v-on:click="setService(service.deliveryService)"
                >
                  {{ service.deliveryService }}(${{ service.deliveryCost }})
                </button>
                <button
                  v-else
                  class="btn btn-warning"
                  v-on:click="setService(service.deliveryService)"
                >
                  {{ service.deliveryService }}(${{ service.deliveryCost }})
                </button>
              </div>
            </div>
          </div>

          <p>{{ selectedService && selectedService + " service seleted" }}</p>

          <div class="modal-footer">
            <button type="button" class="btn btn-danger" data-dismiss="modal">
              BUY
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "BookList",

  data() {
    return {
      books: [],
      bookDetail: {},
      deliveryServices: [],
      selectedService: "",
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

    getDeliveryServices: function getDeliveryServices() {
      axios.get("https://localhost:44302/api/buybook").then((res) => {
        this.deliveryServices = res.data;
        console.log(res.data);
      });
    },
    selectBook: function selectBook(id) {
      axios.get(`https://localhost:44302/api/book/${id}`).then((res) => {
        this.bookDetail = res.data;
        this.getDeliveryServices();
        console.log(res.data);
      });
    },
    setService: function setService(service) {
      this.selectedService = service;
    },
  },
};
</script>
