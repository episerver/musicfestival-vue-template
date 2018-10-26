/**
 * The directive `v-epi-edit` is used similarly to @Html.EditAttributes() in
 * Razor views. It enables On-Page Editing on elements and disables the DOM
 * updating from the CMS so that Vue can keep the responsibility over the DOM.
 *
 * It's enabled by the Vue component instance property `$epi.isEditable`, but
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

function removeEditAttributes(el) {
    el.removeAttribute('data-epi-property-name');
    el.removeAttribute('data-epi-property-render');
    el.removeAttribute('data-epi-property-edittype');
}

function setEditAttributes(el, binding) {
    let value = binding.value;
    let propertyName = '',
        renderType = 'none',
        editType = 'floating';
    if (typeof value === 'string') {
        propertyName = value;
    } else {
        propertyName = value.name;
        renderType = value.renderType ? value.renderType : renderType;
        editType = value.editType ? value.editType : editType;
    }

    el.setAttribute('data-epi-property-name', propertyName);
    el.setAttribute('data-epi-property-render', renderType);
    el.setAttribute('data-epi-property-edittype', editType);
}

function toggleEditAttributes(el, binding, vnode) {
    const siteIsEditable = vnode.context.$epi.isEditable;
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
