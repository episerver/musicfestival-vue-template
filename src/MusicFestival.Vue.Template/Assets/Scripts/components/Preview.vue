<!--
    Renders a block in the different display option sizes as an OPE preview.

    * The block is selected with the `BlockComponentSelector` based on its name.
    * Views/Preview/Index.cshtml outputs this component with the content link.

    Compared to the DefaultPage.vue, this does not use the page's friendly URL
    since blocks don't have them. Instead, the content link is used.

    Like `PageComponentSelector`, this page gets the model from the vuex
    store's `epiDataModel` module.
-->

<template>
    <section class="Grid Preview" v-if="modelLoaded">
        <div class="Grid-cell u-border">
            <h5>Full</h5>
        </div>
        <div class="Grid-cell u-md-sizeFull">
            <BlockComponentSelector :model="model"></BlockComponentSelector>
        </div>

        <div class="Grid-cell u-border">
            <h5>Wide</h5>
        </div>
        <div class="Grid-cell u-md-size2of3">
            <BlockComponentSelector :model="model"></BlockComponentSelector>
        </div>

        <div class="Grid-cell u-border">
            <h5>Half</h5>
        </div>
        <div class="Grid-cell u-md-size1of2">
            <BlockComponentSelector :model="model"></BlockComponentSelector>
        </div>

        <div class="Grid-cell u-border">
            <h5>Narrow</h5>
        </div>
        <div class="Grid-cell u-md-size1of3">
            <BlockComponentSelector :model="model"></BlockComponentSelector>
        </div>
    </section>
</template>

<script>
import BlockComponentSelector from '@/Scripts/components/BlockComponentSelector.vue';
import { mapState } from 'vuex';
import { UPDATE_MODEL_BY_CONTENT_LINK } from '@/Scripts/store/modules/epiDataModel';

export default {
    props: ['contentLink'],
    computed: mapState({
        model: state => state.epiDataModel.model,
        modelLoaded: state => state.epiDataModel.modelLoaded
    }),
    components: {
        BlockComponentSelector
    },
    created() {
        this.$store.dispatch(UPDATE_MODEL_BY_CONTENT_LINK, this.contentLink);
    }
};
</script>

<style lang="less" scoped>
    @import '../../Styles/Common/variables.less';

    .PageHeader{
        display: none;
    }
    .Page > section{
        padding: 1em;
    }
    .u-border {
        border-bottom: 1px solid fade(@colorWhite, 50%);
    }

    .Grid {
        .ContentBlock {
            .Grid-cell>span {
                width: 100%;
            }
        }
    }
</style>
