/**
 * Sets up the router that is used in View mode. When the site is loaded in
 * CMS editing mode, the whole page is always reloaded and the router only gets
 * the current page. The router will not be used for navigating between pages.
 */

import Vue from 'vue';
import Router from 'vue-router';

import PageComponentSelector from '@/Scripts/components/PageComponentSelector.vue';

Vue.use(Router);

export default new Router({
    // Use the HTML HistoryAPI so the # isn't needed in the URL, and
    // Vue routing will work even when going directly to a URL.
    mode: 'history',

    routes: [
        {
            path: '*',
            component: PageComponentSelector,
            // This will merge route data with the component $props, so components don't need to know about routing.
            props: (route) => ({ url: route.fullPath })
        }
    ]
});
