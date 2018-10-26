<!--
    Shows a sign up form that's displayed inside a modal on the LandingPage.
    Because it's a block and we want to be able to edit its properties inside
    the modal in OPE, the property names need to be prefixed with the block
    name, which is done by `prefixPropertyName`.

    View for Models/Blocks/BuyTicketBlock.cs
-->

<template>
    <div class="buyTickets-container">
        <h3 v-epi-edit="prefixPropertyName('Heading')">{{heading || model.heading}}</h3>
        <div>
            <p v-epi-edit="prefixPropertyName('Message')">{{message || model.message}}</p>
            <input type="email" />
        </div>
    </div>
</template>

<script>
export default {
    props: ['pagePropertyName', 'model', 'heading', 'message'],
    methods: {
        prefixPropertyName: function (propertyName) {
            // Prefix propertyName with the block name (pagePropertyName), if it's being edited on a page.

            if (this.pagePropertyName) {
                return this.pagePropertyName + '.' + propertyName;
            }
            return propertyName;
        }
    }
};
</script>

<style lang="less" scoped>
    @import '../../../Styles/Common/variables.less';

    .buyTickets-container {
        background: @backgroundColor;
        padding: @space;
        border-radius: 1em;
    }

</style>
