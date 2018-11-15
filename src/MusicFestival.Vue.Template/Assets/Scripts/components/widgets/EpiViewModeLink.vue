<!--
    This should be used for links when they should be completely disabled when in
    an Episerver CMS UI editing context. One example is language selection links,
    because language selection should be done in the CMS UI instead.
-->

<template>
    <router-link :class="{ 'edit-mode': inEditMode }" :to="url">
        <slot></slot>
    </router-link>
</template>

<script>
import { mapState } from 'vuex';

export default {
    props: ['url'],
    computed: mapState({
        inEditMode: state => state.epiContext.inEditMode
    })
};
</script>

<style lang="less" scoped>
    .edit-mode {
        pointer-events: none;
    }
</style>
