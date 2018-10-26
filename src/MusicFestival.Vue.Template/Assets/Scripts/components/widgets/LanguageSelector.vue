<!--
    Language selector that can be shown on any page.
-->

<template>
    <ul class="lang">
        <li v-for="item in this.existingLanguages" :key="item.name" :class="{ active: isCurrentLanguage(item) }">
            <epi-view-mode-link :url="item.link">{{item.displayName}}</epi-view-mode-link>
        </li>
    </ul>
</template>

<script>
import EpiViewModeLink from '@/Scripts/components/widgets/EpiViewModeLink.vue';

export default {
    props: ['existingLanguages', 'language'],
    methods: {
        isCurrentLanguage(item) {
            return item.name === this.language.name;
        }
    },
    components: {
        EpiViewModeLink
    }
};
</script>

<style lang="less" scoped>
    @import '../../../Styles/Common/variables.less';

    ul {
        list-style: none;
        margin:0;
        padding:0;

        &.main {
            min-height:55vh;
            margin-bottom:50px;
        }

        &.lang {
            position: absolute;
            right: 10px;
            top: 25px;
            z-index: 10;

            li {
                position: relative;
                margin-bottom: 8px;

                &.active:after {
                    content: '';
                    display: block;
                    position: absolute;
                    width: 100%;
                    height: 2px;
                    background: @colorTurquoise;
                    top: 13px;
                }

                a {
                    display: block;
                    user-select: none;
                    font: 10px @fontSubHeading;
                    text-transform: uppercase;
                    cursor: pointer;
                    text-decoration: none;
                }
            }
        }
    }

    @media (min-width:425px) {
        ul {
            &.lang {
                top: 38px;

                li {
                    display: inline-block;
                    right: 0;
                    margin-right: 30px;
                    margin-bottom: 0;

                    &:last-of-type {
                        margin-right: 0;
                    }

                    &.active:after {
                        height: 4px;
                        top: 17px;
                    }

                    a {
                        font-size: 13px;
                    }
                }
            }
        }
    }
</style>
