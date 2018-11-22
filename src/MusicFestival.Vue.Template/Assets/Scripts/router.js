/**
 * Sets up the router that is used in View mode. When the site is loaded in
 * CMS editing mode, the whole page is always reloaded and the router only gets
 * the current page. The router will not be used for navigating between pages.
 */

import Vue from 'vue';
import Router from 'vue-router';
import store from '@/Scripts/store';
import { UPDATE_MODEL_BY_URL } from '@/Scripts/store/modules/epiDataModel';

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
    // URL is updated by vue-route-sync, and when time travelling with the
    // debugger we don't want to trigger a model commit as the model is already
    // part of the store holding the url update.
    if (store.state.epiDataModel.model.url !== to.fullPath) {
        store.dispatch(UPDATE_MODEL_BY_URL, to.fullPath);
    }

    next();
});

export default router;
