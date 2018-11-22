/**
 * The module that is responsible for handling the state of the current content
 * that is being either viewed or edited. This module will handle talking to
 * the API when the model needs to be updated when navigating or editing the
 * site.
 */

import api from '@/Scripts/api/api';

//actions for the epiDataModel module
export const UPDATE_MODEL_BY_URL = 'epiDataModel/UPDATE_MODEL_BY_URL';
export const UPDATE_MODEL_BY_CONTENT_LINK = 'epiDataModel/UPDATE_MODEL_BY_CONTENT_LINK';

const state = {
    model: {},
    modelLoaded: false
};

const UPDATE_MODEL = 'epiDataModel/UPDATE_MODEL';
const mutations = {
    [UPDATE_MODEL](state, newModel) {
        state.model = newModel;
        state.modelLoaded = true;
    },
};

const parameters = {
    expand: '*'
};

const actions = {
    [UPDATE_MODEL_BY_URL]({commit}, friendlyUrl) {
        /**
         * When updating a model by friendly URL we assume that the friendly URL
         * contains every querystring parameter that we might need on the server.
         */

        return api.getContentByFriendlyUrl(friendlyUrl, parameters).then(response => {
            commit(UPDATE_MODEL, response.data);
        });
    },
    [UPDATE_MODEL_BY_CONTENT_LINK]({commit, rootState}, contentLink) {
        /**
         * Updating a model by content link is done when something is being
         * edited and when viewing a block. In order to be sure that we get the
         * correct model, we need to keep any previously existing query string
         * from the friendly URL.
         *
         * See the implementation of ExtendedContentModelMapper.GetContextMode
         * for additional details.
         */

        const params = Object.assign({},
            parameters,
            rootState.route.query
        );

        return api.getContentByContentLink(contentLink, params).then(response => {
            commit(UPDATE_MODEL, response.data);
        });
    }
};

export default {
    state,
    mutations,
    actions
};
