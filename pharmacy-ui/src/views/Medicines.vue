<template>

<div class="container mt-4">

    <h2 class="mb-4">
        Medicines
    </h2>

    <div class="d-flex justify-content-between align-items-center mb-4">

      <input
        type="text"
        class="form-control w-50"
        placeholder="Search Medicine..."
        v-model="searchText" />

        <button
            class="btn btn-primary"
            @click="showModal = true">

            + Add Medicine

        </button>

    </div>

    <MedicineTable
    :medicines="filteredMedicines"
    @edit="editMedicine"
    @delete="deleteMedicine" />

    <MedicineForm
        v-if="showModal"
        :medicineData="selectedMedicine"
        @saved="loadMedicines"
        @close="closeModal" />

</div>

</template>

<script setup>

import { ref, computed, onMounted } from "vue"
import api from "../services/api"

import MedicineTable from "../components/MedicineTable.vue"
import MedicineForm from "../components/MedicineForm.vue"

const medicines = ref([])
const searchText = ref("")
const showModal = ref(false)
const selectedMedicine = ref(null)
async function loadMedicines() {

    const response = await api.get("/Medicines")

    medicines.value = response.data

}
const filteredMedicines = computed(() => {

    if (!searchText.value)
        return medicines.value;

    return medicines.value.filter(medicine =>
        medicine.fullName
            .toLowerCase()
            .includes(searchText.value.toLowerCase())
    );

});
function editMedicine(medicine) {

    selectedMedicine.value = { ...medicine }

    showModal.value = true

}
function closeModal(){

    showModal.value = false

    selectedMedicine.value = null

}
async function deleteMedicine(id) {

    const confirmDelete = confirm("Are you sure you want to delete this medicine?");

    if (!confirmDelete)
        return;

    await api.delete(`/Medicines/${id}`);

    loadMedicines();

}
onMounted(loadMedicines)

</script>