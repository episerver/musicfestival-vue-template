<!--
    Will select a page component based on the page name.

    * Maps Models/Pages/*.cs to Assets/Scripts/components/pages/*.vue

    It gets its `model` from the vuex stores `epiDataModel` module that's
    automatically updated in OPE when data is edited (through the
    `contentSaved` event). The store also includes `modelLoaded` that's
    set to true when the model has been populated in the store. This flag
    toggles the rendering of the page with `v-if`.
-->

<template>
    <div v-if="modelLoaded">
        <component :is="getComponentTypeForPage(model)" :model="model"></component>
    </div>
</template>

<script>
import getComponentTypeForContent from '@/Scripts/api/getComponentTypeForContent';
import { mapState } from 'vuex';

export default {
    computed: mapState({
        model: state => state.epiDataModel.model,
        modelLoaded: state => state.epiDataModel.modelLoaded
    }),
    methods: {
        getComponentTypeForPage(model) {
            // this.$options.components will contain all globally registered components from main.js
            return getComponentTypeForContent(model, this.$options.components);
        }
    }
};
</script>
