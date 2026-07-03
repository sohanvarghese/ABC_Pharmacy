<template>

<table class="table table-bordered table-hover">

    <thead class="table-header">

        <tr>

            <th>Name</th>
            <th>Expiry</th>
            <th>Quantity</th>
            <th>Price</th>
            <th>Brand</th>
            <th width="180">Actions</th>

        </tr>

    </thead>

    <tbody>

        <tr
            v-for="medicine in medicines"
            :key="medicine.id"
            :class="rowColor(medicine)">

            <td>{{ medicine.fullName }}</td>

            <td>{{ formatDate(medicine.expiryDate) }}</td>

            <td>{{ medicine.quantity }}</td>

            <td>{{ medicine.price.toFixed(2) }}</td>

            <td>{{ medicine.brand }}</td>

            <td>

                <button
                    class="btn btn-warning btn-sm me-2"
                    @click="$emit('edit', medicine)">

                    Edit

                </button>

                <button
                    class="btn btn-danger btn-sm"
                    @click="$emit('delete', medicine.id)">

                    Delete

                </button>

            </td>

        </tr>

    </tbody>

</table>

</template>

<script setup>

defineProps({
    medicines: Array
})

defineEmits([
    "edit",
    "delete"
])

function formatDate(date){

    return new Date(date).toLocaleDateString()

}

function rowColor(medicine){

    const today = new Date()
    const expiry = new Date(medicine.expiryDate)

    const days = (expiry - today) / (1000 * 60 * 60 * 24)

    if(days >= 0 && days <= 30)
        return "expiry-row"

    if(medicine.quantity < 10)
        return "low-stock-row"

    return ""

}

</script>

<style scoped>

.expiry-row td{

    background:#dc3545 !important;
    color:white;

}

.low-stock-row td{

    background:#fff3cd !important;

}
.table-header th {
    background-color: #0d6efd;
    color: white;
    text-align: center;
}

</style>