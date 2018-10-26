<!--
    Will select a vue component based on the name.
    Used to match Models/Blocks/*.cs with Assets/Scripts/components/blocks/*.vue.
    `component` dynamically loads a Vue component: https://vuejs.org/v2/guide/components.html#Dynamic-Components

    Compared to the `PageComponentSelector`, this does not use the
    `EpiDataModelMixin` mixin, so it takes its model as a Vue prop. This is
    because a block will never be rendered by itself. It will be rendered
    either in Preview.vue during OPE or ContentArea.vue, and they provide the
    model.
-->

<template>
    <component :is="getComponentTypeForBlock(model)" :model="model"></component>
</template>

<script>
import getComponentTypeForContent from '@/Scripts/api/getComponentTypeForContent';

export default {
    props: ['model'],
    methods: {
        getComponentTypeForBlock: function (block) {
            // this.$options.components will contain all globally registered components from main.js
            // Load the "GenericBlock" in the case that no block is found.
            return getComponentTypeForContent(block, this.$options.components) || 'GenericBlock';
        }
    }
};
</script>
