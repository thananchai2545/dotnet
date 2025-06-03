<template>
  <div>
    <Navbar />
    <Sidebar />
    <div class="p-4 sm:ml-64">
   <div class="p-4 mt-14 w-full">
      <router-view />
   </div>
</div>
  </div>
</template>

<script setup>
  import Navbar from '../components/Navbar.vue';
  import Sidebar from '../components/Sidebar.vue';
</script>
