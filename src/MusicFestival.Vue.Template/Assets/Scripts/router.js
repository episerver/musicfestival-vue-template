/**
 * Sets up the router that is used in View mode. When the site is loaded in
 * CMS editing mode, the whole page is always reloaded and the router only gets
 * the current page. The router will not be used for navigating between pages.
 */

import Vue from 'vue';
import Router from 'vue-router';
import store from '@/Scripts/store';
import { updateUrl } from '@/Scripts/store/action-types.js';

import PageComponentSelector from '@/Scripts/components/PageComponentSelector.vue';

Vue.use(Router);
const router = new Router({
    // Use the HTML HistoryAPI so the # isn't needed in the URL, and
    // Vue routing will work even when going directly to a URL.
    mode: 'history',

    routes: [
        {
            path: '*',
            component: PageComponentSelector
        }
    ]
});

router.beforeEach((to, from, next) => {
    store.dispatch(updateUrl, to.fullPath);
    next();
});

export default router;
