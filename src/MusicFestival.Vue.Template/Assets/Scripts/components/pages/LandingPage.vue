<!--
    Landing page for the site.
    When the BuyTicketBlock modal is shown, the OPE overlays in the Hero are turned off through the `showModal` data property.

    View for Models/Pages/LandingPage.cs
-->

<template>
    <div class="LandingPage" scoped>
        <nav class="Page-container PageHeader NavBar">
            <button class="Button buy-ticket-button" @click="showModal()">{{model.buyTicketBlock.heading}}</button>
            <language-selector :existingLanguages="model.existingLanguages" :language="model.language"></language-selector>
        </nav>

        <Hero :title="model.title" :subtitle="model.subtitle" :heroimage="model.heroImage" />

        <epi-link
            :class-name="'Button modal-default-button landing-page-button'"
            :url="model.artistsLink.expandedValue.url">{{model.artistsLink.expandedValue.name}}
        </epi-link>

        <main class="Page-container">
            <div v-epi-edit="'MainContentArea'">
                <ContentArea :model="model.mainContentArea"></ContentArea>
            </div>
        </main>

        <footer>
            <div v-epi-edit="'FooterContentArea'">
                <ContentArea :model="model.footerContentArea"></ContentArea>
            </div>
            <div class="FooterBottom">
                <h6>&copy; Music Festival 2018</h6>
            </div>
        </footer>

        <Modal>
            <BuyTicketBlock slot="content" :page-property-name="'BuyTicketBlock'" :heading="model.buyTicketBlock.heading" :message="model.buyTicketBlock.message"/>
        </Modal>
    </div>
</template>

<script>
import ContentArea from '@/Scripts/components/ContentArea.vue';
import Hero from '@/Scripts/components/widgets/Hero.vue';
import LanguageSelector from '@/Scripts/components/widgets/LanguageSelector.vue';
import EpiLink from '@/Scripts/components/widgets/EpiLink.vue';
import Modal from '@/Scripts/components/widgets/Modal.vue';
import BuyTicketBlock from '@/Scripts/components/blocks/BuyTicketBlock.vue';
import { SHOW_MODAL } from '@/Scripts/store/modules/appContext';
import { mapMutations } from 'vuex';

export default {
    components: {
        ContentArea,
        Hero,
        LanguageSelector,
        EpiLink,
        Modal,
        BuyTicketBlock
    },
    props: ['model'],
    methods: mapMutations({
        showModal: SHOW_MODAL
    })
};
</script>

<style lang="less">
    @import '../../../Styles/Common/variables.less';

    main, footer {
        overflow: hidden;
        width: 100%;
    }

    footer .ContentArea.Grid--gutterA {
        // Disable gutters because we want this content area to be full width.
        margin: 0;
    }

    .landing-page-button {
        position: absolute;
        left: 50%;
        transform: translateX(-50%);
        margin-top: 1em;
    }

    .buy-ticket-button {
        margin-top: 11px;
    }
</style>
