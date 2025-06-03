<script setup >
 import sidebarItems from '../utils/sidebar.ts'; 
</script>

<template>

<aside id="logo-sidebar" class="fixed top-0 left-0 z-40 w-64 h-screen pt-20 transition-transform -translate-x-full bg-white border-r border-gray-200 sm:translate-x-0" aria-label="Sidebar">
   <div class="h-full px-3 pb-4 overflow-y-auto bg-white">
      <ul v-for="(item, index) in sidebarItems" :key="index" class="space-y-2 font-medium">
         <li>
            <router-link :to="item.path" class="flex items-center p-2 text-gray-700 rounded-lg hover:bg-gray-100 hover:text-gray-900 group">
               <icon :class="item.icon"/>
               <span class="ms-3">{{ item.title || 'Dashboard' }}</span>
            </router-link> 
         </li>
      </ul>
   </div>
</aside>
    
</template>