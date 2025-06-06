<script setup>
import BreadCrumb from "../components/Breadcrumb.vue";
import Button from "../components/Button.vue";
import Input from "../components/Input.vue";
import Pagination from "../components/Pagination.vue";
import Swal from "sweetalert2"
import { onMounted, ref, reactive, computed } from "vue";
import axios from "axios";
const apiUrl = import.meta.env.VITE_API_URL;
const breadCrumbMenu = [{ name: "หมวดหมู่" }];
const categories = reactive({
  data: [],
  total: 0,
});
const query = reactive({
  search: "",
  page: 1,
  pageSize: 5,
});
const destroy = ref([]);
onMounted(() => {
  getCategories();
});
async function getCategories() {
  await axios
    .get(
      `${apiUrl}/categories?search=${query.search}&page=${query.page}&pageSize=${query.pageSize}`
    )
    .then((response) => {
      categories.data = response.data.categories;
      categories.total = response.data.total;
    })
    .catch((error) => {
      console.error("Error fetching categories:", error);
    });
}

async function handleDestroy() {
  console.log(destroy.value);
  await axios
    .delete(`${apiUrl}/categories`, {
      data: destroy.value,
    })
    .then((res) => {
window.location.reload();
    });
}
</script>

<template>
  <div class="w-full">
    <div class="flex h-5 items-center justify-between">
      <BreadCrumb :items="breadCrumbMenu" />
      <div class="flex items-center gap-2">
        <Button
          @click="handleDestroy"
          size="md"
          variant="danger"
          :disabled="destroy.length == 0"
        >
          <i class="fas fa-trash mr-1"></i> ลบ
        </Button>
        <router-link
          to="/add-categories"
          class="text-white bg-blue-600 hover:bg-blue-800 font-medium rounded-lg text-sm px-4 py-2"
        >
          <i class="fas fa-plus mr-1"></i> เพิ่ม
        </router-link>
      </div>
    </div>
    <div class="mt-7">
      <div class="relative bg-gray-100 overflow-x-auto shadow sm:rounded-lg">
        <div class="px-4 my-4 flex gap-2 items-center justify-between">
          <div class="h-8 flex items-center gap-2">
            <Input
              v-model="query.search"
              type="text"
              placeholder="ค้นหาหมวดหมู่"
              class="w-52 h-full"
            />
            <Button
              @click="getCategories"
              variant="primary"
              type="button"
              size="lg"
            >
              <i class="fas fa-search"></i>
            </Button>
          </div>
          <div class="flex items-center gap-2">
            <span class="text-gray-600 hidden md:block">แสดง:</span>
            <select
              class="bg-white border border-gray-300 rounded-md p-1"
              v-model="query.pageSize"
              @change="getCategories"
            >
              <option value="5">5</option>
              <option value="10">10</option>
              <option value="20">20</option>
              <option value="50">50</option>
            </select>
            <span class="text-gray-600 hidden md:block">รายการต่อหน้า</span>
          </div>
        </div>
        <table class="w-full text-sm text-left text-gray-500">
          <thead class="text-xs text-gray-700 uppercase bg-gray-100">
            <tr>
              <th scope="col" class="px-6 pt-3"></th>
              <th scope="col" class="px-6 pt-3"></th>
              <th scope="col" class="px-6 py-3">#</th>
              <th scope="col" class="px-6 py-3">Name</th>
              <th scope="col" class="px-6 py-3">Description</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(item, index) in categories.data"
              :key="item.id"
              class="bg-white border-b border-gray-200 hover:bg-gray-50"
            >
              <th scope="row" class="px-6 py-4 font-medium text-gray-900">
                <!-- chechbox -->
                <input
                  id="default-checkbox"
                  type="checkbox"
                  v-model="destroy"
                  :value="item.id"
                  class="w-4 h-4 text-blue-600 bg-gray-100 border-gray-300 rounded-sm focus:ring-blue-500"
                />
              </th>
              <th scope="row" class="px-6 py-4 font-medium text-gray-900">
                <!-- chechbox -->
                <span class="text-blue-400 border-b">แก้ไข</span>
              </th>
              <th
                scope="row"
                class="px-6 py-4 font-medium text-gray-900 whitespace-nowrap"
              >
                {{ (query.page - 1) * query.pageSize + index + 1 }}
              </th>
              <td class="px-6 py-4">
                {{ item.name }}
              </td>
              <td class="px-6 py-4">
                {{ item.description ? item.description : "ไม่มีคำอธิบาย" }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <!-- make button pagination -->
      <Pagination
        :current-page="query.page"
        :total="categories.total"
        :page-size="query.pageSize"
        @update:currentPage="
          query.page = $event;
          getCategories();
        "
      ></Pagination>
    </div>
  </div>
</template>
