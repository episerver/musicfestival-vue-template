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

Vue.use(Vuex);

const store = new Vuex.Store({
    modules: {
        appContext,
        epiDataModel,
        epiContext
    }
});

export default store;
