import { createRouter, createWebHistory } from 'vue-router'

import Dashboard from '../views/Dashboard.vue'
import Medicines from '../views/Medicines.vue'
import Sales from '../views/Sales.vue'

const routes = [
    {
        path: '/',
        name: 'Dashboard',
        component: Dashboard
    },
    {
        path: '/medicines',
        name: 'Medicines',
        component: Medicines
    },
    {
        path: '/sales',
        name: 'Sales',
        component: Sales
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router