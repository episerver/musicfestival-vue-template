<!--
    A component that will only render the img-tag when the `src` is set.
    This avoids:
    * Chrome rendering a broken image icon when the alt property is set but the src is empty or missing.
    * IE rendering an empty area when the src is empty.
-->

<template>
    <img class="ConditionalImage" v-if="src" :src="src" :alt="alt" />
</template>

<script>
export default {
    props: ['alt', 'src']
};
</script>
