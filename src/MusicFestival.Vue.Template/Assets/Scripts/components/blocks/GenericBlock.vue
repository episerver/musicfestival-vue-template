<!--
    Rendered when no view was found by BlockComponentSelector.vue for the loaded block model.
-->

<template>
    <div class="Page-container GenericBlock">
        <div class="Grid Grid--alignMiddle Grid--gutterA">
            <div class="Grid-cell">
                <p>Could not load {{model.Name}} vue component.</p>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: ['model']
};
</script>
