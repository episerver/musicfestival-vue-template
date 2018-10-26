/**
 * Will check the content's content type against the inheritance chain list in
 * components.
 *
 * Used to get the Vue component matching the loaded content's type by
 * `BlockComponentSelector` and `PageComponentSelector`.
 *
 * @param {*} content The content object that has a contentType property, which
 * holds the inheritance chain from the C# models for the content with the last
 * item being the actual implementation.
 * @param {Array} components The list of registered Vue components.
 * @returns The matching content type, or `null`.
 */
export default function getComponentTypeForContent(content, components) {
    // Here we will try to find a component that matches the content type name.
    for (let i = (content.contentType.length - 1); i >= 0; i--) {
        if (components[content.contentType[i]]) {
            return content.contentType[i];
        }
    }
    return null;
}
