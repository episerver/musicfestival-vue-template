/**
 * The module responsible for handling app-wide context state that is
 * interesting for several components that otherwise doesn't share state.
 */

//mutations for the appContext module
export const SHOW_MODAL = 'appContext/SHOW_MODAL';
export const HIDE_MODAL = 'appContext/HIDE_MODAL';

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
