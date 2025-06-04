<script setup lang="ts">
import {defineProps} from 'vue';
const props = defineProps({
    message: {
        type: String,
        default: ''
    }
});
</script>

<template>
    <div class="text-red-500 text-sm mt-2" v-if="props.message">
        {{ props.message }}
    </div>
</template>