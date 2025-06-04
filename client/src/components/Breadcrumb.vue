<script setup>
import { defineProps } from 'vue';

const props = defineProps({
  items: {
    type: Array,
    required: true,
  }
});
console.log(props.items);

</script>

<template>
<nav class="flex" aria-label="Breadcrumb">
  <ol class="inline-flex items-center space-x-1 md:space-x-2 text-gray-500 ">
    <li class="inline-flex items-center">
      <router-link to="/">
        <div class="inline-flex items-center ">
          <i class="fa-solid fa-house"></i>
          <span class="ms-1 text-sm font-medium hover:text-gray-700 md:ms-2">Home</span>
        </div>
      </router-link>
    </li>
    <li v-for="(item,index) in items" :key="index">
        <div v-if="item.link" class="flex items-center">
            <i class="fa-solid fa-chevron-right"></i>
            <router-link :to="item.link">
                <span class="ms-1 text-sm hover:text-gray-700 font-medium md:ms-2">{{ item.name }}</span>
            </router-link>
        </div>
        <div v-else class="flex items-center">
            <i class="fa-solid fa-chevron-right"></i>
            <span class="ms-1 text-sm font-medium md:ms-2">{{ item.name }}</span>
        </div>
    </li>
   
  </ol>
</nav>
</template>