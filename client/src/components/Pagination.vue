<script setup>
import { computed, defineProps, defineEmits } from 'vue';
const props = defineProps({
  total: {
    type: Number,
    required: true
  },
  pageSize: {
    type: Number,
    default: 5
  },
  currentPage: {
    type: Number,
    default: 1
  }
});
</script>

<template>
  <div class="flex items-center justify-between mt-4">
     <div class="text-sm text-gray-700">
      ทั้งหมด {{ props.total }} รายการ
    </div>
    <div class="flex items-center">
      <button
        @click="$emit('update:currentPage', Math.max(props.currentPage - 1, 1))"
        :disabled="props.currentPage === 1"
        class="px-3 py-1 bg-gray-200 rounded-md disabled:opacity-50"
      >
        &laquo;
      </button>
      <span class="mx-2 text-sm text-gray-700">
        หน้า {{ props.currentPage }} 
      </span>
      <button
        @click="$emit('update:currentPage', props.currentPage + 1)"
        :disabled="props.currentPage >= Math.ceil(props.total / props.pageSize)"
        class="px-3 py-1 bg-gray-200 rounded-md disabled:opacity-50"
      >
     &raquo;
      </button>
    </div>
   
  </div>
</template>