<!--
    This should be used for links instead of regular anchor elements. It will
    detect if the view is in an Episerver CMS UI editing context, and then
    disable Vue router. That's needed to get context changes to work, such as
    updating the page navigation tree.
-->

<template>
    <component :is="tagType" class="EPiLink" :to="url" :href="url" :class="className">
        <slot></slot>
    </component>
</template>

<script>
import { mapState } from 'vuex';

export default {
    props: [
        'url',
        'className'
    ],
    computed: mapState({
        tagType: function (state) {
            // summary:
            //      Define whether we should use the tag 'a' or 'router-link' when generating a link.
            //      The reason is because <router-link> doesn't support absolute link
            //      (https://github.com/vuejs/vue-router/issues/1131), which happens when we link to a page
            //      in another site in a multi-sites system.
            //      There is an open feature-request for making 'router-link' support absolute links.
            //      https://github.com/vuejs/vue-router/issues/1280
            //
            //      Always user an 'a' tag in edit mode to update the Episerver UI

            if (state.epiContext.inEditMode) {
                return 'a';
            }
            return (this.url.match(/^(http(s)?|ftp):\/\//)) ? 'a' : 'router-link';
        }
    })
};
</script>
