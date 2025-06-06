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
  },
  size: {
    type: String,
    default: 'md', // small, medium, large
    validator: (val) => ['sm', 'md', 'lg'].includes(val)
  }
})

const size = computed(() => {
  switch (props.size) {
    case 'sm':
      return 'text-sm py-1 px-3'
    case 'md':
      return 'text-base py-1.5 px-4'
    case 'lg':
      return 'text-lg py-2 px-4'
    default:
      return ''
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
      'rounded font-medium transition duration-200 rounded-lg',
      variantClasses,
      size,
      { 'opacity-50 cursor-not-allowed': disabled },
      'flex items-center justify-center',
      $attrs.class,

    ]"
    :disabled="disabled"
  >
    <slot />
  </button>
</template>