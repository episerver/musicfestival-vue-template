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
import Vue from 'vue';

const context = {
    inEditMode: false,
    isEditable: false
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
        context.inEditMode = window.epi.beta.inEditMode;
        context.isEditable = window.epi.beta.isEditable;
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

// Make the context available to all Vue components.
// > "$ is a convention Vue uses for properties that are available to all instances."
// https://vuejs.org/v2/cookbook/adding-instance-properties.html#The-Importance-of-Scoping-Instance-Properties
Vue.prototype.$epi = context;

export default context;
