<template>

<div class="card shadow">

    <div class="card-header bg-primary text-white">

        <h5 class="mb-0">Record Sale</h5>

    </div>

    <div class="card-body">

        <div class="mb-3">

            <label class="form-label">
                Medicine
            </label>

            <select
                class="form-select"
                v-model="sale.medicineId">

                <option value="">
                    Select Medicine
                </option>

                <option
                    v-for="medicine in medicines"
                    :key="medicine.id"
                    :value="medicine.id">

                    {{ medicine.fullName }}

                </option>

            </select>

        </div>

        <div class="mb-3">

            <label class="form-label">
                Quantity Sold
            </label>

            <input
                type="number"
                class="form-control"
                min="1"
                v-model="sale.quantitySold">

        </div>

        <button
            class="btn btn-primary"
            @click="saveSale">

            Save Sale

        </button>

    </div>

</div>

</template>

<script setup>

import { ref, reactive, onMounted } from "vue"
import api from "../services/api"

const emit = defineEmits(["saved"])

const medicines = ref([])

const sale = reactive({

    medicineId: "",
    quantitySold: 1,
    saleDate: ""

})

async function loadMedicines() {

    const response = await api.get("/Medicines")

    medicines.value = response.data

}

async function saveSale() {

    if (!sale.medicineId) {

        alert("Please select a medicine.");

        return;

    }

    if (sale.quantitySold <= 0) {

        alert("Quantity must be greater than zero.");

        return;

    }

    // Automatically use the current date and time
    sale.saleDate = new Date().toISOString();

    await api.post("/Sales", sale);

    emit("saved");

    sale.medicineId = "";
    sale.quantitySold = 1;
    sale.saleDate = "";

}

onMounted(loadMedicines)

</script>