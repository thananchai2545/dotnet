<script setup>
import Input from '../components/Input.vue';
import Label from '../components/Label.vue';
import Button from '../components/Button.vue';
import ErrorMessage from '../components/ErrorMessage.vue';
import { reactive } from 'vue';
import useVuelidate from '@vuelidate/core'
import { required } from '@vuelidate/validators';
import axios from 'axios';
import Swal from 'sweetalert2';
import { useRouter } from 'vue-router';

const router = useRouter();
const formData = reactive({
    name: '',
    description: ''
});
const rules = {
    name: { required },
};
const v$ = useVuelidate(rules, formData);

async function addCategoty(e) {
    e.preventDefault();    
    v$.value.$touch();
    if (v$.value.$invalid) {
        document.querySelector('#name').className += ' border-red-500';
        return;
    }
    const apiUrl = import.meta.env.VITE_API_URL;
    try {
       const response = await axios.post(`${apiUrl}/categories`, 
         {
              name: formData.name,
              description: formData.description !== '' ? formData.description : null
         }
       )
       if (response.status === 200) {
            Swal.fire({
                icon: 'success',
                title: 'สำเร็จ',
                text: 'เพิ่มหมวดหมู่เรียบร้อยแล้ว',
                confirmButtonText: 'ตกลง'
            }).then(() => {
                formData.name = '';
                formData.description = '';
                v$.value.$reset();
                router.push('/categories');
            });
       } else {
            Swal.fire({
                icon: 'error',
                title: 'ผิดพลาด',
                text: 'ไม่สามารถเพิ่มหมวดหมู่ได้ กรุณาลองใหม่อีกครั้ง',
                confirmButtonText: 'ตกลง'
            });
       }
    } catch (error) {
        console.error('เกิดข้อผิดพลาด:', error);
    }
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
                        <ErrorMessage v-if="v$.name.$error" message="กรุณากรอกชื่อหมวดหมู่" />
                    </div>
                    <div class="mb-4">
                       <Label>คำอธิบาย</Label>
                        <textarea v-model="formData.description" id="categoryDescription" name="categoryDescription" rows="10"
                                  class="mt-1 block w-full p-2 border border-gray-300 rounded-md focus:ring-blue-500 focus:border-blue-500" placeholder="ระบุรายละเอียด..."></textarea>
                    </div>
                    <div class="flex items-center justify-end">
                        <Button size="lg" type="submit" variant="success"><i class="fas fa-save mr-1"></i> บันทึก</Button>
                    </div>
                </form> 
            </div>
        </div>
    </div>
</template>