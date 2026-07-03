<template>

<div class="modal d-block" tabindex="-1" style="background: rgba(0,0,0,0.5);">

    <div class="modal-dialog">

        <div class="modal-content">

            <div class="modal-header">

                <h5 class="modal-title">
                    Add Medicine
                </h5>

                <button
                    type="button"
                    class="btn-close"
                    @click="closeModal">
                </button>

            </div>

            <div class="modal-body">

                <div class="mb-3">

                    <label class="form-label">Full Name</label>

                    <input
                        v-model="medicine.fullName"
                        class="form-control">

                </div>

                <div class="mb-3">

                    <label class="form-label">Notes</label>

                    <textarea
                        v-model="medicine.notes"
                        class="form-control">
                    </textarea>

                </div>

                <div class="mb-3">

                    <label class="form-label">Expiry Date</label>

                    <input
                        type="date"
                        v-model="medicine.expiryDate"
                        class="form-control">

                </div>

                <div class="mb-3">

                    <label class="form-label">Quantity</label>

                    <input
                        type="number"
                        v-model="medicine.quantity"
                        class="form-control">

                </div>

                <div class="mb-3">

                    <label class="form-label">Price</label>

                    <input
                        type="number"
                        step="0.01"
                        v-model="medicine.price"
                        class="form-control">

                </div>

                <div class="mb-3">

                    <label class="form-label">Brand</label>

                    <input
                        v-model="medicine.brand"
                        class="form-control">

                </div>

            </div>

            <div class="modal-footer">

                <button
                    class="btn btn-secondary"
                    @click="closeModal">

                    Cancel

                </button>

                <button
                    class="btn btn-primary"
                    @click="saveMedicine">

                    {{ medicine.id ? 'Update' : 'Save' }}

                </button>

            </div>

        </div>

    </div>

</div>

</template>

<script setup>

import { reactive, watch } from "vue"
import api from "../services/api"

const emit = defineEmits(["saved","close"])
const props = defineProps({

    medicineData: Object

})
const medicine = reactive({

    id: 0,
    fullName:"",
    notes:"",
    expiryDate:"",
    quantity:0,
    price:0,
    brand:""

})
watch(

    () => props.medicineData,

    (value)=>{

        if(value){

            Object.assign(medicine,value)

        }

    },

    { immediate:true }

)

async function saveMedicine() {

    if (medicine.id) {

        // Update existing medicine
        await api.put(`/Medicines/${medicine.id}`, medicine);

    } else {

        // Add new medicine
        await api.post("/Medicines", medicine);

    }

    emit("saved");

    clearForm();

    emit("close");
}

function clearForm() {

    medicine.id = 0;
    medicine.fullName = "";
    medicine.notes = "";
    medicine.expiryDate = "";
    medicine.quantity = 0;
    medicine.price = 0;
    medicine.brand = "";

}

function closeModal(){

    emit("close");

}

</script>