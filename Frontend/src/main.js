import Vue from "vue";
import App from "./App.vue";
import VueToastify from "vue-toastify";
Vue.use(VueToastify);

Vue.config.productionTip = false;

new Vue({
  render: (h) => h(App),
}).$mount("#app");
