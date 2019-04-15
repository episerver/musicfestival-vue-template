<!--
    Displays some information regarding an artist along with the dates and location of its performance.

    View for Models/Pages/ArtistDetailsPage.cs
-->

<template>
    <div class="ArtistDetailsPage">
        <nav class="Page-container PageHeader NavBar">
            <BackButton :prevUrl="model.parentLink.url"/>
            <LanguageSelector :existingLanguages="model.existingLanguages" :language="model.language"></LanguageSelector>
        </nav>

        <div class="Page-container u-posRelative">
            <ArtistImage :name="model.artistName" :image-url="model.artistPhoto" />

            <div class="top">
                <h1 v-epi-edit="'ArtistName'">{{model.artistName}}</h1>
            </div>

            <epi-property property-name="ArtistPhoto"></epi-property>
            <epi-property property-name="ArtistGenre"></epi-property>
            <epi-property property-name="ArtistIsHeadliner"></epi-property>

            <div class="artist-information">
                <p v-epi-edit="'StageName'" v-html="model.stageName"></p>
                <p><span v-epi-edit="'PerformanceStartTime'">{{friendlyStartTime}}</span> - <span v-epi-edit="'PerformanceEndTime'">{{friendlyEndTime}}</span></p>
            </div>
            <div class="artist-description">
                <p v-epi-edit="'ArtistDescription'" v-html="model.artistDescription"></p>
            </div>
        </div>

        <footer>
            <div class="FooterBottom">
                <h6>&copy; Music Festival 2018</h6>
            </div>
        </footer>
    </div>
</template>

<script>
import ArtistImage from '@/Scripts/components/widgets/ArtistImage.vue';
import BackButton from '@/Scripts/components/widgets/BackButton.vue';
import LanguageSelector from '@/Scripts/components/widgets/LanguageSelector.vue';
import EpiProperty from '@/Scripts/components/EpiProperty.vue';

export default {
    props: ['model'],
    computed: {
        friendlyStartTime() {
            return this.friendlyDateTime(this.model.performanceStartTime);
        },
        friendlyEndTime() {
            return this.friendlyDateTime(this.model.performanceEndTime);
        }
    },
    methods: {
        friendlyDateTime(dateTime) {
            return new Date(dateTime).toLocaleString();
        }
    },
    components: {
        ArtistImage,
        BackButton,
        LanguageSelector,
        EpiProperty
    }
};
</script>

<style lang="less" scoped>
    @import "../../../Styles/Common/variables.less";

    @footerHeight: 46px;

    .ArtistDetailsPage {
        position: relative;
        padding-bottom: @footerHeight;
    }

    .top {
        text-align: center;
        position:absolute;
        width: 100%;
        h1 {
            position: relative;
            font-size: 1em;
            top: -2.5em;
        }
    }

    .artist-information {
        p {
            margin: 0.55em 0;
            font: 12px @fontSubHeading;
            text-transform:uppercase;
        }
    }

    .artist-information,
    .artist-description {
        margin: 0 10px;

        @media (min-width:768px) {
            margin: 0;
        }
    }

    .FooterBottom {
        position: absolute;
        bottom: 0;
        left: 0;
        right: 0;
        height: @footerHeight;
    }

    @media (min-width:768px) {
        .top {
            h1 {
                font-size: 2em;
                top: -2em;
            }
        }
    }

    @media (min-width: 1224px) {
        .top {
            h1 {
                font-size: 2.5em;
                top: -1.5em;
            }
        }
    }
</style>
