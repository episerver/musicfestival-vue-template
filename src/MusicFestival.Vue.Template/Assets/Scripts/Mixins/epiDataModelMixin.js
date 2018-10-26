import api from '@/Scripts/api/api.js';

export default {
    watch: {
        '$epi.isEditable': '_registerContentSavedEvent'
    },

    data: function () {
        return {
            modelLoaded: false,
            model: {}
        };
    },

    methods: {
        updateModelByFriendlyUrl: function (friendlyUrl) {
            /**
             * When updating a model by friendly URL we assume that the friendly URL
             * contains every querystring parameter that we might need on the server.
             */

            return this._updateModelAsync(api.getContentByFriendlyUrl, friendlyUrl);
        },

        updateModelByContentLink: function (contentLink) {
            /**
             * Updating a model by content link is done when something is being edited and when viewing a block.
             * In order to be sure that we get the correct model, we need to keep any previously
             * existing query string from the friendly URL.
             *
             * See the implementation of ExtendedContentModelMapper.GetContextMode for additional details.
             */

            let queryString = null;
            if (this.model && this.model.url) {
                queryString = this.model.url.split('?')[1];
            }
            let contentLinkUrl = queryString ? contentLink + '?' + queryString : contentLink;
            return this._updateModelAsync(api.getContentByContentLink, contentLinkUrl);
        },

        _updateModelAsync: function (apiMethod, args) {
            /**
             * Updates the model by calling the ContentDeliveryAPI.
             */

            let vm = this;

            const parameters = {
                expand: '*'
            };

            return apiMethod(args, parameters).then((response) => {
                let data = response.data;
                vm.model = data;
                vm.modelLoaded = true;
                vm.$forceUpdate();
                return response;
            });
        },

        _registerContentSavedEvent(isEditable) {
            /**
             * If we enter an editable context we want to enable On-Page Editing
             * and therefore start listening to the "beta/contentSaved" event to
             * be notified when content has been edited. We then update the
             * model and trigger an update of the components.
             */

            if (isEditable) {
                window.epi.subscribe('beta/contentSaved', message => {
                    this.updateModelByContentLink(message.contentLink);
                });
            }
        }
    }
};
