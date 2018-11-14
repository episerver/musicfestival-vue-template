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
            /**
             * When updating a model by friendly URL we assume that the friendly URL
             * contains every querystring parameter that we might need on the server.
             */

            api.getContentByFriendlyUrl(friendlyUrl, parameters).then(response => {
                commit(UPDATE_MODEL, response.data);
            });
        },
        updateModelByContentLink({commit, state}, contentLink) {
            /**
             * Updating a model by content link is done when something is being edited and when viewing a block.
             * In order to be sure that we get the correct model, we need to keep any previously
             * existing query string from the friendly URL.
             *
             * See the implementation of ExtendedContentModelMapper.GetContextMode for additional details.
             */

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
