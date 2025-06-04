<template>
  <div>
      <Navbar />
      <Sidebar />
      <div class="p-4 bg-gray-50 h-screen sm:ml-64">
        <div class="p-4 mt-14">
          <router-view />
        </div>
      </div>
  </div>
</template>

<script setup>
  import Navbar from '../components/Navbar.vue';
  import Sidebar from '../components/Sidebar.vue';
</script>
