<template>

<div class="container mt-4">

    <h2 class="mb-4">

        Sales

    </h2>

    <SaleForm
        @saved="loadSales" />

    <div class="card shadow mt-4">

        <div class="card-header bg-primary text-white">

            <h5 class="mb-0">

                Sales History

            </h5>

        </div>

        <div class="card-body">

            <table class="table table-bordered table-hover">

                <thead class="table-header">

                    <tr>

                        <th>Medicine</th>
                        <th>Quantity Sold</th>
                        <th>Sale Date</th>

                    </tr>

                </thead>

                <tbody>

                    <tr
                        v-for="sale in sales"
                        :key="sale.id">

                        <td>{{ getMedicineName(sale.medicineId) }}</td>

                        <td>{{ sale.quantitySold }}</td>

                        <td>{{ formatDate(sale.saleDate) }}</td>

                    </tr>

                </tbody>

            </table>

        </div>

    </div>

</div>

</template>

<script setup>

import { ref, onMounted } from "vue"

import api from "../services/api"

import SaleForm from "../components/SaleForm.vue"

const sales = ref([])
const medicines = ref([])
async function loadSales(){

    const response = await api.get("/Sales")

    sales.value = response.data

}
async function loadMedicines() {

    const response = await api.get("/Medicines")

    medicines.value = response.data

}
function formatDate(date){

    return new Date(date).toLocaleDateString()

}
function getMedicineName(id) {

    const medicine = medicines.value.find(m => m.id === id)

    return medicine ? medicine.fullName : "Unknown Medicine"

}

onMounted(() => {

    loadSales()

    loadMedicines()

})

</script>

<style scoped>

.table-header th{

    background-color:#0d6efd;
    color:white;
    text-align:center;

}

</style>