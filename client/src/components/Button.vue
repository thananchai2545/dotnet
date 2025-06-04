<script setup>
import { computed,defineProps } from 'vue'
const props = defineProps({
  type: {
    type: String,
    default: 'button'
  },
  variant: {
    type: String,
    default: 'primary', // primary, secondary, danger, etc.
    validator: (val) => ['primary', 'secondary', 'danger','success'].includes(val)
  },
  disabled: {
    type: Boolean,
    default: false
  }
})

const variantClasses = computed(() => {
  switch (props.variant) {
    case 'primary':
      return 'bg-blue-600 text-white hover:bg-blue-700'
    case 'secondary':
      return 'bg-gray-200 text-gray-800 hover:bg-gray-300'
    case 'danger':
      return 'bg-red-600 text-white hover:bg-red-700'
    case 'success':
      return 'bg-green-700 text-white hover:bg-green-800'
    default:
      return ''
  }
})
</script>

<template>
  <button
    :type="type"
    :class="[
      'px-4 py-2 rounded font-medium transition duration-200 focus:outline-none focus:ring-2 focus:ring-offset-2 rounded-md',
      variantClasses,
      { 'opacity-50 cursor-not-allowed': disabled }
    ]"
    :disabled="disabled"
  >
    <slot />
  </button>
</template>