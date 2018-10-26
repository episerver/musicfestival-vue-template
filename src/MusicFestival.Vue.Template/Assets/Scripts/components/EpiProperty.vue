<!--
    Renders a button to edit a property in OPE. Useful when the property to
    edit isn't directly visible (such as ArtistGenre on the ArtistDetailsPage
    component), or when the OPE overlays would be overlapping (e.g. the background
    image in the Hero component).
-->

<template>
    <button v-if="$epi.isEditable" v-epi-edit="propertyName" type="button">Edit property: {{propertyName}}</button>
</template>

<script>
export default {
    props: ['propertyName']
};
</script>

<style lang="less" scoped>
    button {
        background: gray;
        color: white;
    }
</style>
