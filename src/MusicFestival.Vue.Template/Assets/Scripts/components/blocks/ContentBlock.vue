<!--
    Displays an optional image with a right or left alignment and some text.
    When there is no image selected, the right/left grid is not rendered, so to make OPE image selection possible we render a epi-property button.

    View for Models/Blocks/ContentBlock.cs
-->

<template>
    <div class="Page-container ContentBlock">
        <div class="Grid Grid--alignMiddle Grid--gutterA" :class="imageAlignment(model.imageAlignment)">
            <div class="Grid-cell u-md-size1of2" v-if="model.image">
                <ConditionalImage :src="model.image" :alt="model.title" v-epi-edit="'Image'"></ConditionalImage>
            </div>
            <div class="Grid-cell" :class="{'u-md-size1of2': model.image}">
                <h2 v-epi-edit="'Title'">{{model.title}}</h2>
                <div v-epi-edit="'Content'" v-html="model.content"></div>
                <epi-property v-if="!model.image" property-name="image"></epi-property>
            </div>
        </div>
    </div>
</template>

<script>
import ConditionalImage from '@/Scripts/components/widgets/ConditionalImage.vue';
import EpiProperty from '@/Scripts/components/EpiProperty.vue';

export default {
    props: ['model'],
    methods: {
        imageAlignment(direction) {
            if (direction === 'Right') {
                return 'Grid--rowReverse';
            }
        }
    },
    components: {
        ConditionalImage,
        EpiProperty
    }
};
</script>

<style lang="less" scoped>
    @import '../../../Styles/Common/variables.less';
</style>
