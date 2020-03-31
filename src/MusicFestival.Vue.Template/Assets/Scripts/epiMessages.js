/**
 * Context flags that are useful to enable properly working On-Page Editing.
 * Sets the context in the vuex store to be used on every component that is
 * interested.
 *
 * These values are `false` by default and will be updated when the page has
 * finished loading. See the event handler at the bottom of the page.
 *
 * Also registers the `contentSaved` event that will update
 * the model in the store during editing.
 */

import store from '@/Scripts/store';
import { UPDATE_CONTEXT } from '@/Scripts/store/modules/epiContext';
import { UPDATE_MODEL_BY_CONTENT_LINK } from '@/Scripts/store/modules/epiDataModel';

function setContext() {
    // The `epiReady` event only has `isEditable`, but the epi object has both.
    const context = {
        inEditMode: window.epi.inEditMode,
        isEditable: window.epi.isEditable
    };

    // Make the context available to all Vue components.
    store.commit(UPDATE_CONTEXT, context);

    // If we're in an editable context we want to update the model on every change by the editor
    if (window.epi.isEditable) {
        window.epi.subscribe('contentSaved', message => {
            store.dispatch(UPDATE_MODEL_BY_CONTENT_LINK, message.contentLink);
        });
    }
}

// Listen to the `epiReady` event to update the `context` property.
window.addEventListener('load', () => {
    // Expect `epi` to be there after the `load` event. If it's not then we're
    // not in any editing context.
    if (!window.epi) {
        return;
    }

    // Check that ready is an actual true value (not just truthy).
    if (window.epi.ready === true) {
        // `epiReady` already fired.
        setContext();

    // The subscribe method won't be available in View mode.
    } else if (window.epi.subscribe) {
        // Subscribe if the `epiReady` event hasn't happened yet.
        window.epi.subscribe('epiReady', () => setContext());
    }
});
