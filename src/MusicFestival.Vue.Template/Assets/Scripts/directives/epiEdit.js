/**
 * The directive `v-epi-edit` is used similarly to @Html.EditAttributes() in
 * Razor views. It enables On-Page Editing on elements using the `data-epi-edit`
 * property (introduced in Episerver CMS UI 11.X.0) and disables the DOM
 * updating from the CMS so that Vue can keep the responsibility over the DOM.
 *
 * It's enabled by the `isEditable` value that is stored in the Vuex store, but
 * can be overwritten by a component having a property named
 * `epiDisableEditing` being true.
 *
 * Usage can be found on most Vue components, such as ArtistDetailsPage.vue.
 */

import store from '@/Scripts/store';

function toggleEditAttributes(el, binding, vnode) {
    const siteIsEditable = store.state.epiContext.isEditable;
    const componentIsEditable = !vnode.context.epiDisableEditing;

    if (siteIsEditable && componentIsEditable) {
        el.setAttribute('data-epi-edit', binding.value);
    } else {
        el.removeAttribute('data-epi-edit');
    }
}

export default {
    bind: toggleEditAttributes,
    update: toggleEditAttributes
};
