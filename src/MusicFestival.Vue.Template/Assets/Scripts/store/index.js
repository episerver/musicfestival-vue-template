import Vue from 'vue';
import Vuex from 'vuex';
import api from '@/Scripts/api/api.js';
Vue.use(Vuex);

const UPDATE_MODEL = 'UPDATE_MODEL';
const UPDATE_CONTEXT = 'UPDATE_CONTEXT';

const parameters = {
    expand: '*'
};

const store = new Vuex.Store({
    state: {
        model: {},
        modelLoaded: false,
        context: {
            inEditMode: false,
            isEditable: false
        }
    },
    mutations: {
        [UPDATE_MODEL](state, newModel) {
            state.model = newModel;
            state.modelLoaded = true;
        },
        [UPDATE_CONTEXT](state, newContext) {
            state.context = newContext;
        }
    },
    actions: {
        updateModelByFriendlyUrl({commit}, friendlyUrl) {
            api.getContentByFriendlyUrl(friendlyUrl, parameters).then(response => {
                commit(UPDATE_MODEL, response.data);
            });
        },
        updateModelByContentLink({commit, state}, contentLink) {
            let queryString = null;
            if (state.model && state.model.url) {
                queryString = state.model.url.split('?')[1];
            }
            let contentLinkUrl = queryString ? contentLink + '?' + queryString : contentLink;
            api.getContentByContentLink(contentLinkUrl, parameters).then(response => {
                commit(UPDATE_MODEL, response.data);
            });
        }
    }
});

export default store;
