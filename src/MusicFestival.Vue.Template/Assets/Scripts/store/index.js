/**
 * The main vuex store. This holds the state of the URL and makes sure that
 * when the URL is updated, the model gets updated too.
 */

import Vue from 'vue';
import Vuex from 'vuex';

// the module handling model state
import epiDataModel from './modules/epiDataModel.js';

// the module handling episerver specific state
import epiContext from './modules/epiContext.js';

// the module handling app specific state
import appContext from './modules/appContext.js';

import { UPDATE_URL } from './mutation-types.js';
import { updateModelByFriendlyUrl, updateUrl } from './action-types.js';

Vue.use(Vuex);

const store = new Vuex.Store({
    modules: {
        appContext,
        epiDataModel,
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
             * When the url is updated we will also update the model. This is
             * as an action because updating the model is an async operation.
             */

            commit(UPDATE_URL, url);
            return dispatch(updateModelByFriendlyUrl, url);
        }
    }
});

export default store;
