<!--
    Renders a ContentArea by iterating through all the blocks and using the
    `BlockComponentSelector` to render the corresponding Vue component.

    By setting the `data-epi-block-id` attribute the block becomes editable
    during On-Page Edit. It will not be set in View mode to not leak out too
    much info about Episerver to visitors.

    The model property is provided by the page or block that owns the
    ContentArea.
-->

<template>
    <section class="Grid Grid--alignMiddle Grid--gutterA ContentArea">
        <div :key="index" v-for="(block, index) in model" class="Grid-cell" :class="getDisplayOption(block.displayOption)">
            <BlockComponentSelector :data-epi-block-id="isEditable ? block.contentLink.id : null" :model="block"></BlockComponentSelector>
        </div>
    </section>
</template>

<script>
import * as CONSTANTS from '@/Scripts/constants';
import BlockComponentSelector from '@/Scripts/components/BlockComponentSelector.vue';
import { mapState } from 'vuex';

export default {
    props: ['model'],
    computed: mapState({
        isEditable: state => state.epiContext.isEditable
    }),
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
