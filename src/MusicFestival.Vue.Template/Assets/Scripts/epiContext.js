/**
 * Context flags that are useful to enable properly working On-Page Editing.
 * Also sets them on every Vue component under the `$epi` property, which is
 * then picked up by the different Epi helpers in this template site.
 *
 * Exports:
 * - `inEditMode`: We want to turn off routing as long as we're in the CMS UI.
 *                 Used in the Vue components `EpiLink` and `EpiViewModeLink`.
 * - `isEditable`: We want to know when to enable On-Page Editing (OPE). Used
 *                 in the Vue directive `epiEdit`, Vue component `EpiProperty`,
 *                 and Vue mixin `EpiDataModelMixin`.
 *
 * These values are `false` by default and will be updated when the page has
 * finished loading. See the event handler at the bottom of the page.
 */

import store from '@/Scripts/store';
import { UPDATE_CONTEXT } from '@/Scripts/store/mutation-types';
import { updateModelByContentLink } from '@/Scripts/store/action-types.js';

const registerContentSavedEvent = (isEditable) => {
    /**
     * If we enter an editable context we want to enable On-Page Editing
     * and therefore start listening to the "beta/contentSaved" event to
     * be notified when content has been edited. We then update the
     * model and trigger an update of the components.
     */

    if (isEditable) {
        window.epi.subscribe('beta/contentSaved', message => {
            store.dispatch(updateModelByContentLink, message.contentLink);
        });
    }
};

// Listen to the `beta/epiReady` event to update the `context` property.
window.addEventListener('load', () => {
    // Expect `epi` to be there after the `load` event. If it's not then we're
    // not in any editing context.
    if (!window.epi) {
        return;
    }

    function setContext() {
        // The event only has `isEditable`, but the epi object has both.
        const context = {
            inEditMode: window.epi.beta.inEditMode,
            isEditable: window.epi.beta.isEditable
        };

        store.commit(UPDATE_CONTEXT, context);
        registerContentSavedEvent(context.isEditable);
    }

    // Check for beta and that ready is an actual true value (not just truthy).
    if (window.epi.beta && window.epi.beta.ready === true) {
        // `beta/epiReady` already fired.
        setContext();

    // The subscribe method won't be available in View mode.
    } else if (window.epi.subscribe) {
        // Subscribe if the `beta/epiReady` event hasn't happened yet.
        window.epi.subscribe('beta/epiReady', () => setContext());
    }
});
