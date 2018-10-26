<!--
    Displays an artist image.
-->

<template>
    <div class="artistImage">
        <div>
            <ConditionalImage :src="imageUrl" :alt="name"></ConditionalImage>
        </div>
    </div>
</template>

<script>
import ConditionalImage from '@/Scripts/components/widgets/ConditionalImage.vue';

export default {
    props: ['name', 'imageUrl'],
    components: {
        ConditionalImage
    }
};
</script>

<style lang="less" scoped>
    .artistImage {
        padding-top: 80px;
    }

    img {
        width: 100%;
        height: auto;
        animation: fadein 2s;
    }

    @keyframes fadein {
        from { opacity: 0; }
        to   { opacity: 1; }
    }
</style>
