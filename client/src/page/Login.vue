<script setup>
import Cookies from 'js-cookie';
import { reactive,ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';
import useVuelidate from '@vuelidate/core'
import { required } from '@vuelidate/validators';

    const router = useRouter();
    const errorMessage = ref('');
    const formData = reactive({
        username: 'admin',
        password: '1234',
    });

    const rules = {
        username: { required },
        password: { required }
    };

    const v$ = useVuelidate(rules, formData);

    function handleLogin(event) {
        const apiUrl = import.meta.env.VITE_API_URL;
        event.preventDefault();
        v$.value.$touch()
        if (v$.value.$invalid) {
            console.log('Form is invalid');
            return;
        }
    axios.post( apiUrl + '/auth/login', {
        username: formData.username,
        password: formData.password
    }).then(response => {        
        if (response.status === 200) {
            const {accessToken , refreshToken} = response.data;
            localStorage.setItem('access_token', accessToken);
            Cookies.set('refresh_token', refreshToken, { expires: 7 }); 
            router.push('/');
        }
    }).catch(error => {
        console.error('Login failed:', error);
        if (error.response && error.response.status === 401) {
            errorMessage.value = error.response.data;
        } else {
            errorMessage.value = error.message || 'An error occurred during login.';
        }
    });   
    }
</script>

<template>
    <section class="bg-gray-50 ">
  <div class="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
      
      <div class="w-full bg-white rounded-lg shadow md:mt-0 sm:max-w-md xl:p-0">
          <div class="p-6 space-y-4 md:space-y-6 sm:p-8">
              <h1 class="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl">
                  Sign in to your account
              </h1>
              <form class="space-y-4 md:space-y-6" action="#" @submit="handleLogin">
                  <div>
                      <label for="username" class="block mb-2 text-sm font-medium text-gray-900">username</label>
                      <input v-model="formData.username" type="text" name="username" id="username" class="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5" placeholder="username">
                        <p v-if="v$.username.$error" class="mt-2 text-sm text-red-500">กรุณากรอกชิ่อผู้ใช้</p>
                  </div>
                  <div>
                      <label for="password" class="block mb-2 text-sm font-medium text-gray-900">Password</label>
                      <input v-model="formData.password" type="password" name="password" id="password" placeholder="password" class="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 ">
                      <p v-if="v$.password.$error" class="mt-2 text-sm text-red-500">กรุณากรอกรหัสผ่าน</p>
                  </div>
                  <p class="text-sm text-red-600 mt-2">{{ errorMessage }}</p>
                  <div class="flex items-center justify-between">
                      <div class="flex items-start">
                          <div class="flex items-center h-5">
                            <input id="remember" aria-describedby="remember" type="checkbox" class="w-4 h-4 border border-gray-300 rounded bg-gray-50">
                          </div>
                          <div class="ml-3 text-sm">
                            <label for="remember" class="text-gray-500">Remember me</label>
                          </div>
                      </div>
                  </div>
                    <button type="submit" class="w-full text-black bg-blue-500 hover:bg-blue-700 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center">
                      Sign in
                    </button>
              </form>
          </div>
      </div>
  </div>
</section>
</template>