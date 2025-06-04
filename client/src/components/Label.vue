<script setup>
import { defineProps } from 'vue';

defineProps({
    htmlFor: {
    type: String,
    required: false
  }
})
</script>

<template>
    <label :for="htmlFor" class="block text-sm font-medium text-gray-700"><slot /></label>
</template>