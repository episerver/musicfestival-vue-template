/**
 * The directive `v-epi-edit` is used similarly to @Html.EditAttributes() in
 * Razor views. It enables On-Page Editing on elements and disables the DOM
 * updating from the CMS so that Vue can keep the responsibility over the DOM.
 *
 * It's enabled by the `isEditable` value that is stored in the vuex store, but
 * can be overwritten by a component having a property named
 * `epiDisableEditing` being true.
 *
 * Set's the needed data-epi attributes on an element:
 * - data-epi-property-name="[the given property name]"
 * - data-epi-property-render="none"
 * - data-epi-property-edittype="floating"
 *
 * Usage can be found on most Vue components, such as ArtistDetailsPage.vue.
 */

import store from '@/Scripts/store';

function removeEditAttributes(el) {
    el.removeAttribute('data-epi-property-name');
    el.removeAttribute('data-epi-property-render');
    el.removeAttribute('data-epi-property-edittype');
}

function setEditAttributes(el, binding) {
    el.setAttribute('data-epi-property-name', binding.value);
    el.setAttribute('data-epi-property-render', 'none');
    el.setAttribute('data-epi-property-edittype', 'floating');
}

function toggleEditAttributes(el, binding, vnode) {
    const siteIsEditable = store.state.epiContext.isEditable;
    const componentIsEditable = !vnode.context.epiDisableEditing;

    if (siteIsEditable && componentIsEditable) {
        setEditAttributes(el, binding);
    } else {
        removeEditAttributes(el);
    }
}

export default {
    bind: toggleEditAttributes,
    update: toggleEditAttributes
};
