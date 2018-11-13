import Vue from 'vue';
import Vuex from 'vuex';

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
    }
});

export default store;
