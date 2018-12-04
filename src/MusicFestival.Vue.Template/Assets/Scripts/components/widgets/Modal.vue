<!--
    Generic modal with a slot to display any component. It's used to display
    the BuyTicketBlock on the LandingPage.
-->

<template>
    <transition name="modal" v-if="modalShowing">
        <div class="modal-mask">
            <div class="modal-wrapper">
                <div class="modal-container">

                    <div class="modal-content">
                        <slot name="content">
                            default content
                        </slot>
                    </div>

                    <div class="modal-footer">
                        <slot name="footer">
                            <button class="Button modal-default-button" @click="closeModal()">
                                OK
                            </button>
                        </slot>
                    </div>
                </div>
            </div>
        </div>
    </transition>
</template>

<script>
import { mapState, mapMutations } from 'vuex';
import { HIDE_MODAL } from '@/Scripts/store/modules/appContext';

export default {
    computed: mapState({
        modalShowing: state => state.appContext.modalShowing
    }),
    methods: mapMutations({
        closeModal: HIDE_MODAL
    })
};
</script>

<style lang="less" scoped>
    @import '../../../Styles/Common/variables.less';

    .modal-mask {
        position: fixed;
        z-index: 9998;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, .5);
        display: table;
        transition: opacity .3s ease;
    }

    .modal-wrapper {
        display: table-cell;
        vertical-align: middle;

        // HACK - Having it in the middle doesn't work in OPE
        vertical-align: top;
        padding-top: 10rem;
    }

    .modal-container {
        width: 300px;
        margin: 0px auto;
        padding: @space;
        background-color: @backgroundColor;
        border-radius: 1em;
        box-shadow: 0 2px 8px rgba(0, 0, 0, .33);
        transition: all .3s ease;
    }

    .modal-default-button {
        float: right;
    }

    .modal-enter {
        opacity: 0;
    }

    .modal-leave-active {
        opacity: 0;
    }

    .modal-enter .modal-container,
    .modal-leave-active .modal-container {
        -webkit-transform: scale(1.1);
        transform: scale(1.1);
    }
</style>
