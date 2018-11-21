/**
 * The module responsible to handling Episerver specific state that is relevant
 * when editing content in edit mode.
 */

import { UPDATE_CONTEXT } from '../mutation-types.js';

const state = {
    inEditMode: false,
    isEditable: false
};

const mutations = {
    [UPDATE_CONTEXT](state, newContext) {
        state.isEditable = newContext.isEditable;
        state.inEditMode = newContext.inEditMode;
    }
};

export default {
    state,
    mutations
};
