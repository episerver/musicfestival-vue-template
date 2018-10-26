/**
 * Registers all of the content components representing pages and blocks. They
 * need to be globally registered because they are dynamically rendered by the
 * component selectors (`PageComponentSelector` and `BlockComponentSelector`).
 */

import Vue from 'vue';
import '@/Styles/Main.less';

import router from '@/Scripts/router.js';

// generate svg sprite from all files in /Assets/Images/SVG
const files = require.context('@/Images/SVG', false, /.*\.svg$/);
files.keys().forEach(files);

// Episerver helpers
import EpiEdit from '@/Scripts/directives/epiEdit';
import epiContext from '@/Scripts/epiContext';

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

// Episerver helpers
Vue.directive('epi-edit', EpiEdit);

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

const appContext = {
    modalShowing: false
};

Vue.prototype.$app = appContext;

/* eslint-disable-next-line no-unused-vars */
let App = new Vue({
    el: '#App',
    router,
    // This data is only to convert `epiContext` and `appContext` to reactive
    // properties, which is referenced through `this.$epi` on all components.
    data: {
        epiContext,
        appContext
    }
});
