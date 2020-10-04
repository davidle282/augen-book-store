<template>
  <div
    class="modal fade"
    id="bookDetailModal"
    tabindex="-1"
    aria-labelledby="bookDetailModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header bg-danger text-white">
          <h5 class="modal-title" id="bookDetailModalLabel">Book detail</h5>
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
          <div class="card-detail mb-3">
            <div class="row no-gutters">
              <div class="col-md-4">
                <img v-bind:src="bookDetail.image" class="card-img p-3" />

                <h5 class="card-title">{{ bookDetail.title }}</h5>
                <p class="card-text">
                  {{ bookDetail.author }}
                </p>
                <p class="card-text">
                  <small class="text-muted">{{
                    formatDateString(bookDetail.publishedDate)
                  }}</small>
                </p>
              </div>
              <div class="col-md-8">
                <div class="card-body">
                  <p class="text-justify">
                    <small>
                      {{
                        bookDetail.description
                          ? bookDetail.description
                          : "No description"
                      }}</small
                    >
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
                v-on:click="setService(service)"
              >
                {{ service.deliveryService }}(${{ service.deliveryCost }})
              </button>
              <button
                v-else-if="service.deliveryService === 'Train'"
                class="btn btn-primary"
                v-on:click="setService(service)"
              >
                {{ service.deliveryService }}(${{ service.deliveryCost }})
              </button>
              <button
                v-else
                class="btn btn-warning"
                v-on:click="setService(service)"
              >
                {{ service.deliveryService }}(${{ service.deliveryCost }})
              </button>
            </div>
          </div>
        </div>

        <p>
          {{
            selectedService.deliveryService &&
            "Your seleted delivery service: " + selectedService.deliveryService
          }}
        </p>

        <div class="modal-footer justify-content-center">
          <button
            :disabled="isDisable"
            v-on:click="buyBook(selectedService)"
            type="button"
            id="btnBuy"
            class="btn btn-danger"
            data-dismiss="modal"
          >
            BUY
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { formatDate } from "./../helper";

export default {
  name: "BookDetail",
  props: ["bookId"],
  data() {
    return {
      bookDetail: {},
      selectBookId: this.bookId,
      isDisable: true,
      deliveryServices: [],
      selectedService: {},
    };
  },
  watch: {
    bookId: function () {
      this.getBook();
    },
  },
  mounted: function mounted() {
    this.getBook();
    this.getDeliveryServices();
  },
  methods: {
    getDeliveryServices: function () {
      axios.get("https://localhost:44302/api/buybook").then((res) => {
        this.deliveryServices = res.data;

        console.log(res.data);
      });
    },
    getBook: function () {
      this.isDisable = true;
      this.bookDetail = {};
      this.selectedService = {};

      axios
        .get(`https://localhost:44302/api/book/${this.bookId}`)
        .then((res) => {
          this.bookDetail = res.data;
          console.log(res.data);
        });
    },
    formatDateString: function (input) {
      return formatDate(input);
    },
    setService: function (service) {
      this.selectedService = service;
      this.isDisable = false;
    },

    renderSelectedDeliveryService: function (service) {
      if (service.deliveryService) {
        return <p>{service.deliveryService}</p>;
      }
    },

    buyBook: function (service) {
      axios.post("https://localhost:44302/api/buybook", service).then((res) => {
        console.log(res.data);

        this.$vToastify.success(res.data);
      });
    },
  },
};
</script>
