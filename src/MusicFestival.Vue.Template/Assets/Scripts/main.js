/**
 * Registers all of the content components representing pages and blocks. They
 * need to be globally registered because they are dynamically rendered by the
 * component selectors (`PageComponentSelector` and `BlockComponentSelector`).
 */

import Vue from 'vue';
import '@/Styles/Main.less';

import router from '@/Scripts/router';
import store from '@/Scripts/store';
import { sync } from 'vuex-router-sync';
sync(store, router);

// `epiMessages` does not export anything but registers the `beta/contentSaved`
// and `beta/epiReady` message handlers that updates the vuex store.
// https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Statements/import#Import_a_module_for_its_side_effects_only
import '@/Scripts/epiMessages';

// generate svg sprite from all files in /Assets/Images/SVG
const files = require.context('@/Images/SVG', false, /.*\.svg$/);
files.keys().forEach(files);

// Episerver helpers
import EpiEdit from '@/Scripts/directives/epiEdit';
Vue.directive('epi-edit', EpiEdit);

// Blocks
import BuyTicketBlock from '@/Scripts/components/blocks/BuyTicketBlock.vue';
import ContentBlock from '@/Scripts/components/blocks/ContentBlock.vue';
import GenericBlock from '@/Scripts/components/blocks/GenericBlock.vue';
// Media
import ImageFile from '@/Scripts/components/media/ImageFile.vue';
// Pages
import ArtistContainerPage from '@/Scripts/components/pages/ArtistContainerPage.vue';
import ArtistDetailsPage from '@/Scripts/components/pages/ArtistDetailsPage.vue';
import LandingPage from '@/Scripts/components/pages/LandingPage.vue';
// Views
import Preview from '@/Scripts/components/Preview.vue';
import DefaultPage from '@/Scripts/components/DefaultPage.vue';

// Blocks
Vue.component('BuyTicketBlock', BuyTicketBlock);
Vue.component('ContentBlock', ContentBlock);
Vue.component('GenericBlock', GenericBlock);
// Media
Vue.component('ImageFile', ImageFile);
// Pages
Vue.component('ArtistContainerPage', ArtistContainerPage);
Vue.component('ArtistDetailsPage', ArtistDetailsPage);
Vue.component('LandingPage', LandingPage);
// Views
Vue.component('Preview', Preview);
Vue.component('DefaultPage', DefaultPage);

/* eslint-disable-next-line no-unused-vars */
let App = new Vue({
    el: '#App',
    store,
    router
});
