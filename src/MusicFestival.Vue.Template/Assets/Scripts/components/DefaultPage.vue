<!--
    Renders the Vue router view. The router.js is configured to always load
    `PageComponentSelector`.

    * Views/DefaultPage/Index.cshtml outputs this component.

    Compared to Preview.vue, this does not use the content link, but uses the
    page's friendly URL instead.
-->

<template>
    <div class="DefaultPage">
        <router-view></router-view>
    </div>
</template>

<style lang="less" scoped>
    @import '../../Styles/common/variables.less';

    .DefaultPage {
        position: relative;
        top: 0;
        left: 0;
        background-color: @backgroundColor;
        background-image: @backgroundGradient;
        background-attachment: fixed;
        transition: @menuTransition, opacity .4s ease-in-out;
        opacity: 1;
    }
</style>
