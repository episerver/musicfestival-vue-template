<!--
    Renders a ContentArea by iterating through all the blocks and using the
    `BlockComponentSelector` to render the corresponding Vue component.

    The model property is provided by the page or block that owns the
    ContentArea.
-->

<template>
    <section class="Grid Grid--alignMiddle Grid--gutterA ContentArea">
        <div :key="index" v-for="(block, index) in model" class="Grid-cell" :class="getDisplayOption(block.displayOption)">
            <BlockComponentSelector :model="block"></BlockComponentSelector>
        </div>
    </section>
</template>

<script>
import * as CONSTANTS from '@/Scripts/constants.js';
import BlockComponentSelector from '@/Scripts/components/BlockComponentSelector.vue';

export default {
    props: ['model'],
    components: {
        BlockComponentSelector
    },
    methods: {
        getDisplayOption(value) {
            let displayoption = value;
            for (var key in CONSTANTS.DISPLAY_OPTIONS) {
                if (CONSTANTS.DISPLAY_OPTIONS.hasOwnProperty(key)) {
                    if (displayoption === key) {
                        return CONSTANTS.DISPLAY_OPTIONS[key];
                    }
                }
            }
        }
    }
};
</script>
