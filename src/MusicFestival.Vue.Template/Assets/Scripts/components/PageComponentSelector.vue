<!--
    Will select a page component based on the page name.

    * Maps Models/Pages/*.cs to Assets/Scripts/components/pages/*.vue
    * The `url` prop is provided by the route registered in router.js

    It uses the `EpiDataModelMixin` mixin to get a `model` that's automatically
    updated in OPE when data is edited (through the `beta/contentSaved` event).
    The mixin also includes `modelLoaded` that's set to true when the model has
    been populated by the ContentDeliveryAPI (in api.js). This flag toggles the
    rendering of the page with `v-if`.
-->

<template>
    <div v-if="modelLoaded">
        <component :is="getComponentTypeForPage(model)" :url="url" :model="model"></component>
    </div>
</template>

<script>
import EpiDataModelMixin from '@/Scripts/mixins/epiDataModelMixin';
import getComponentTypeForContent from '@/Scripts/api/getComponentTypeForContent';

export default {
    mixins: [EpiDataModelMixin],
    props: ['url'],
    watch: {
        url: 'updateData'
    },
    created() {
        this.updateData();
    },
    methods: {
        updateData() {
            this.updateModelByFriendlyUrl(this.$props.url);
        },
        getComponentTypeForPage(model) {
            // this.$options.components will contain all globally registered components from main.js
            return getComponentTypeForContent(model, this.$options.components);
        }
    }
};
</script>
