<!--
    Will select a vue component based on the name.
    Used to match Models/Blocks/*.cs with Assets/Scripts/components/blocks/*.vue.
    `component` dynamically loads a Vue component: https://vuejs.org/v2/guide/components.html#Dynamic-Components

    Compared to the `PageComponentSelector`, this does not use the store to get
    the model. It must take the model as a prop as the store model can be either:
    * the block, when editing in Preview.vue
    * the page that the block belongs to, when rendered by ContentArea.vue
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
