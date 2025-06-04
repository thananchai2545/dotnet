<script setup>
import Input from '../components/Input.vue';
import Label from '../components/Label.vue';
import Button from '../components/Button.vue';
import { reactive } from 'vue';


const formData = reactive({
    name: '',
    description: ''
});

function addCategoty(e) {
    e.preventDefault();
    console.log('Adding category:', formData);
}
</script>

<template>
    <div class="w-full"> 
        <div class="mt-4 flex justify-center lg:px-56">
            <div class="w-full bg-white p-6 rounded-lg shadow-md">
                <h2 class="text-2xl font-bold mb-4">เพิ่มหมวดหมู่</h2>
                <form @submit="addCategoty">
                    <div class="mb-4">
                        <Label for="name">ชื่อหมวดหมู่</Label>
                        <Input v-model="formData.name" type="text" id="name" placeholder="หมวดหมู่สินค้า"/>
                    </div>
                    <div class="mb-4">
                       <Label>คำอธิบาย</Label>
                        <textarea id="categoryDescription" name="categoryDescription" rows="10"
                                  class="mt-1 block w-full p-2 border border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500" placeholder="ระบุรายละเอียด..."></textarea>
                    </div>
                    <div class="flex items-center justify-end">
                        <Button variant="success"><i class="fas fa-save mr-1"></i> บันทึก</Button>
                    </div>
                </form> 
            </div>
        </div>
    </div>
</template>