<!--
    A list of all the artists that are children pages to this page.

    View for Models/Pages/ArtistContainerPage.cs
-->

<template>
    <div class="ArtistContainerPage">
        <nav class="Page-container PageHeader NavBar">
            <BackButton :prevUrl="model.parentLink.url"/>
            <LanguageSelector :existingLanguages="model.existingLanguages" :language="model.language"></LanguageSelector>
        </nav>

        <div class="Page-container">
            <div class="top gutter">
                <h1 v-epi-edit="'Name'">{{model.name}}</h1>
            </div>
            <div class="list">
                <div v-for="(value, key) in artists" :key="key">
                    <h3>{{key}}</h3>
                    <Card :name="value.artistName" :url="value.url" :image="value.artistPhoto"  v-for="(value, key) in value" :key="key"></Card>
                </div>
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
import api from '@/Scripts/api/api';
import BackButton from '@/Scripts/components/widgets/BackButton.vue';
import Card from '@/Scripts/components/widgets/Card.vue';
import LanguageSelector from '@/Scripts/components/widgets/LanguageSelector.vue';
import _ from 'lodash';
import { mapState } from 'vuex';

export default {
    props: ['model'],
    data() {
        return {
            artists: {}
        };
    },
    computed: mapState({
        url: state => state.epiDataModel.model.url
    }),
    created() {
        this.updateData();
    },
    watch: {
        model: 'updateData'
    },
    methods: {
        updateData() {
            const parameters = {};

            return api.getChildren(this.url, parameters)
                .then(success => {
                    // sort response alphabetically
                    let ordered = _.orderBy(success.data, [artist => artist.artistName.toLowerCase()], ['asc']);
                    // group them by first letter of artist name and store in data.artists object
                    this.artists = _.groupBy(ordered, artist => artist.artistName.substring(0, 1));
                    return success;
                });
        }
    },
    components: {
        BackButton,
        Card,
        LanguageSelector
    }
};
</script>

<style lang="less" scoped>
    .top h1 {
        text-transform: none;
        margin: 0 40px 0 40px;
        padding: 0.3em 0;

        @media (min-width: 425px) {
            margin-right: 140px;
        }
    }
    h3 {
        text-transform: uppercase;
        width: 100%;
        text-align: center;
        background: rgba(236, 64, 122, .24);
        padding: 5px 0 7px;
        margin: 0;
    }
</style>
