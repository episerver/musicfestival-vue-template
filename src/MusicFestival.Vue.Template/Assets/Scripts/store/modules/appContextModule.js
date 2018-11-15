import { SHOW_MODAL, HIDE_MODAL } from '../mutation-types.js';

const state = {
    modalShowing: false
};

const mutations = {
    [SHOW_MODAL](state) {
        state.modalShowing = true;
    },
    [HIDE_MODAL](state) {
        state.modalShowing = false;
    }
};

export default {
    state,
    mutations
};
