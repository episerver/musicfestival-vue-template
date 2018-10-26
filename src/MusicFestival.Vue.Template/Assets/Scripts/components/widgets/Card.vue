<!--
    Displays a generic "card" with image and text. It's used for the artists
    lists in ArtistContainerPage.
-->

<template>
    <epi-link :url="url">
        <div class="card">
            <div class="round">
                <ConditionalImage :src="image" :alt="name"></ConditionalImage>
            </div>
            <div class="info">
                <p>{{name}}</p>
            </div>
        </div>
    </epi-link>
</template>

<script>
import ConditionalImage from '@/Scripts/components/widgets/ConditionalImage.vue';
import EpiLink from '@/Scripts/components/widgets/EpiLink.vue';

export default {
    props: ['name', 'image', 'url'],
    components: {
        ConditionalImage,
        EpiLink
    }
};
</script>

<style lang="less" scoped>
    @import "../../../styles/common/variables.less";

    .card{
        display: flex;
        flex-direction: row;
        align-items: center;
        padding: 10px 30px 10px 25px;
    }
    .round{
        width: 40px;
        height: 40px;
        border-radius: 40px;
        overflow:hidden;
        margin-right:10px;
        img{
            max-width:150%;
            margin-left:-15px;
        }

        &.big{
            width: 60px;
            height: 60px;
        }
    }
    .info{
        flex-grow: 1;
        p{
            margin:0;
            font:14px/20px @fontSubHeading;
        }
        span{
            display: block;
            font: 12px/16px @fontFamilyPrimary;
        }
    }
</style>
