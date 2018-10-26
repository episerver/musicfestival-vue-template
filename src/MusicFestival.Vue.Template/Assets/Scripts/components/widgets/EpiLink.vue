<!--
    This should be used for links instead of regular anchor elements. It will
    detect if the view is in an Episerver CMS UI editing context, and then
    disable Vue router. That's needed to get context changes to work, such as
    updating the page navigation tree.
-->

<template>
    <!-- Editmode -->
    <a v-if="$epi.inEditMode" class="EpiLink EpiLink-EditMode" :class="className" :href="url">
        <slot></slot>
    </a>
    <!-- Viewmode -->
    <router-link v-else class="EpiLink" :class="className" :to="url">
        <slot></slot>
    </router-link>
</template>

<script>
export default {
    props: [
        'url',
        'className'
    ]
};
</script>
