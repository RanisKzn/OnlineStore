import { createRouter, createWebHistory } from "vue-router";
import HomePage from "../page/HomePage.vue";
import LoginUser from "../components/LoginUser.vue"; // Импортируем компонент авторизации

const routes = [
  {
    path: "/",
    redirect: "/login", // Перенаправление на страницу входа по умолчанию
  },
  {
    path: "/login",
    name: "Login",
    component: LoginUser, // Компонент авторизации
  },
  {
    path: "/home",
    name: "Home",
    component: HomePage, // Защищенная страница
    meta: { requiresAuth: true }, // Метаданные для проверки авторизации
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// Глобальная проверка авторизации
router.beforeEach((to, from, next) => {
  const isAuthenticated = localStorage.getItem("token"); // Проверяем наличие токена в localStorage
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (!isAuthenticated) {
      next("/login"); // Если пользователь не авторизован, перенаправляем на страницу входа
    } else {
      next(); // Если авторизован, продолжаем переход
    }
  } else {
    next(); // Для открытых страниц (например, /login) разрешаем доступ
  }
});

export default router;
