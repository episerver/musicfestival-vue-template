<!--
    Used to render an image in content areas.
-->

<template>
    <div class="Page-container ImageFile">
        <div class="Grid Grid--alignMiddle Grid--gutterA">
            <div class="Grid-cell">
                <ConditionalImage :src="model.url"></ConditionalImage>
            </div>
        </div>
    </div>
</template>

<script>
import ConditionalImage from '@/Scripts/components/widgets/ConditionalImage.vue';

export default {
    props: ['model'],
    components: {
        ConditionalImage
    }
};
</script>
