<!--
    Renders a block in the different display option sizes as an OPE preview.

    * The block is selected with the `BlockComponentSelector` based on its name.
    * Views/Preview/Index.cshtml outputs this component with the content link.

    Compared to the DefaultPage.vue, this does not use the page's friendly URL since
    blocks don't have them. Instead, the content link is used.

    Like `PageComponentSelector`, this page owns the model through the
    `EpiDataModelMixin` mixin.
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
import EpiDataModelMixin from '@/Scripts/mixins/epiDataModelMixin';
import BlockComponentSelector from '@/Scripts/components/BlockComponentSelector.vue';

export default {
    props: ['contentLink'],
    mixins: [EpiDataModelMixin],
    components: {
        BlockComponentSelector
    },
    created() {
        this.updateModelByContentLink(this.contentLink);
    }
};
</script>

<style lang="less">
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
