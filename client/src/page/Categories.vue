<script setup>
    import BreadCrumb from '../components/Breadcrumb.vue';
    const breadCrumbMenu = [
    { name: 'หมวดหมู่' },
];
</script>

<template>
    <div class="w-full"> 
        <div class="flex items-center justify-between ">
            <BreadCrumb :items="breadCrumbMenu" />
            <router-link to="/add-categories" class="text-white bg-blue-600 hover:bg-blue-800 font-medium rounded-lg text-sm px-4 py-2">
               <i class="fas fa-plus mr-1"></i> เพิ่ม
            </router-link>
        </div> 
        <div class="mt-4">1111</div>
    </div>
</template>