import { createRouter, createWebHistory } from 'vue-router';
// import Home from '../page/Home.vue';
import Layout from '../layout/Layout.vue';
import axios from 'axios';
const routes = [
  {
    path: '/',
    component: Layout,
    children: [
      {
        path: '',
        name: 'Home',
        component: () => import('../page/Home.vue'),
        meta : { requiresAuth: true }
      },
      {
        path: 'categories',
        name: 'Categories',
        component: () => import('../page/Categories.vue'),
        meta : { requiresAuth: true }
      },
    ],
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('../page/Login.vue'),
  }
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
});

async function isAuthenticated(): Promise<boolean> {
  const apiUrl = import.meta.env.VITE_API_URL;
  const accessToken = localStorage.getItem('access_token');
  if (!accessToken || accessToken === 'undefined') {
    try {
      const response = await axios.post(`${apiUrl}/auth/refresh-token`, {}, {
        withCredentials: true,
      });
      const { accessToken: newAccessToken } = response.data;
      localStorage.setItem('access_token', newAccessToken);
      return true;
    } catch (error) {
      return false;
    }
  } else {
    return true;
  }
}

router.beforeEach(async (to, _, next) => {
  const isAuth = await isAuthenticated()
  if (to.matched.some(record => record.meta.requiresAuth) && !isAuth) {
    console.log('Not authenticated, redirecting to login');
    next({ name: 'Login' });
  }
  else {
    console.log('Authenticated, proceeding to route');
    next();
  }
});

export default router;