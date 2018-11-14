import Vue from 'vue';
import Vuex from 'vuex';
import api from '@/Scripts/api/api.js';
Vue.use(Vuex);

const UPDATE_MODEL = 'UPDATE_MODEL';

const store = new Vuex.Store({
    state: {
        model: {},
        modelLoaded: false
    },
    mutations: {
        [UPDATE_MODEL](state, newModel) {
            state.model = newModel;
            state.modelLoaded = true;
        }
    },
    actions: {
        updateModelByFriendlyUrl({commit}, friendlyUrl) {
            const parameters = {
                expand: '*'
            };
            api.getContentByFriendlyUrl(friendlyUrl, parameters).then(response => {
                commit(UPDATE_MODEL, response.data);
            });
        }
    }
});

export default store;
