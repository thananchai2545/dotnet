<script setup>
    import { required } from '@vuelidate/validators';
import { defineProps ,defineEmits } from 'vue';
    defineProps({
        modelValue: {
            type: String,
            required: true
        },
        id : String,
        type: {
            type: String,
            default: 'text',
            required: false,
            validator: (value) => ['text', 'email', 'password', 'number'].includes(value)
        },
        placeholder: {
            type: String,
            default: '',
            required: false
        }
    })
        defineEmits(['update:modelValue']);

</script>


<template>
    <div>
        <input @input="$emit('update:modelValue', $event.target.value)" 
               :value="modelValue" 
               :type="type" 
               :id="id" 
               name="categoryName"
               class="mt-1 block w-full p-2 border border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500" 
                :placeholder="placeholder"
               />
    </div>
</template>