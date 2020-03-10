/**
 * Wraps the calls to the ContentDeliveryAPI. It's used by the vuex
 * `epiDataModel` module and the `ArtistContainerPage` component.
 */

import axios from 'axios';

const get = (baseURL, url, parameters, headers) => {
    return axios({
        method: 'get',
        baseURL: baseURL,
        url: url,
        params: parameters,
        headers: Object.assign({}, headers)
    });
};

const applicationPath = document.documentElement.dataset.applicationPath;

export default {
    /**
     * Getting content with the content link is the default way of calling the ContentDeliveryAPI.
     * It is used in MusicFestival to get:
     *  - block data
     *  - updated data after a `contentSaved` message, which has the content link
     */
    getContentByContentLink: (contentLink, parameters) =>
        get(`${applicationPath}api/episerver/v2.0/`, `content/${contentLink}`, parameters),

    /**
     * Getting data from ContentDeliveryAPI through regular routing (friendly URLs) was added in ContentDeliveryAPI 2.3.0.
     * It is used in MusicFestival to get:
     *  - page data, through the vuex `epiDataModel` module
     */
    getContentByFriendlyUrl: (friendlyUrl, parameters) =>
        get('/', friendlyUrl, parameters, { Accept: 'application/json'}),

    /**
     * Getting the children of the page with ContentDeliveryAPI is enabled by
     * the extensions in Infrastructure/ContentDeliveryExtendedRouting.
     * It is used in MusicFestival to get:
     *  - artist list in ArtistContainerPage.vue
     */
    getChildren(friendlyUrl, parameters) {
        // Split URL into path and queries
        const urlPieces = friendlyUrl.split('?');
        // In View mode we might visit the URL with or without a trailing / (i.e. "http://localhost:56312/en/artists" or "http://localhost:56312/en/artists/")
        const pathname = (urlPieces[0].endsWith('/') ? urlPieces[0] : urlPieces[0] + '/');
        // In Edit mode we'll have URL queries (i.e. "/EPiServer/CMS/Content/en/artists,,6/?epieditmode=True")
        const queries = urlPieces[1] ? '?' + urlPieces[1] : '';

        // Concatenate the friendly URL with "/children" for the Content API
        const callUrl = pathname + 'children' + queries;

        return this.getContentByFriendlyUrl(callUrl, parameters);
    }
};
