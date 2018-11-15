import Vue from 'vue';
import Vuex from 'vuex';

import content from './modules/contentModule.js';
import epiContext from './modules/epiContextModule.js';
import appContext from './modules/appContextModule.js';

import { UPDATE_URL } from './mutation-types.js';
import { updateModelByFriendlyUrl, updateUrl } from './action-types.js';

Vue.use(Vuex);

const store = new Vuex.Store({
    modules: {
        appContext,
        content,
        epiContext
    },
    state: {
        url: ''
    },
    mutations: {
        [UPDATE_URL](state, newUrl) {
            state.url = newUrl;
        }
    },
    actions: {
        [updateUrl]({commit, dispatch}, url) {
            /**
             * When the url is updated we will also update the model.
             */

            commit(UPDATE_URL, url);
            return dispatch(updateModelByFriendlyUrl, url);
        }
    }
});

export default store;
