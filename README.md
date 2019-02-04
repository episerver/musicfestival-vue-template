# MusicFestival Vue.js Templates

This sample site demonstrates one approach to render Episerver content in a client side framework that is using client side routing for navigation with a working On Page Edit (OPE) mode in the Episerver UI.

This particular solution uses [Vue.js](https://vuejs.org/) with [Vuex](https://vuex.vuejs.org/) to handle the state of the app in a `single source of truth`. Most of the techniques are framework agnostic and can be used with any other framework, such as React or Angular.

Content is fetched from Episerver using the Content Delivery API: https://world.episerver.com/documentation/developer-guides/CMS/Content/content-delivery-api/

## Prerequisites

This project uses:
* npm 6+
* Visual Studio 2015+
* SQL Server 2016 Express LocalDB ([download here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads))

## Setup and Run

1. Run `setup.cmd`
2. Open `MusicFestival.Vue.Template.sln` and hit `Ctrl + F5`
    * Or build with `build.cmd`, and set up the site on IIS
4. Login on `/episerver` with either one of the following:

|Name    |Password    |Mailbox | Email |
|--------|------------|--------|-------|
|cmsadmin|sparr0wHawk |        |       |
|emil    |sparr0wHawk |https://www.mailinator.com/v3/index.jsp?zone=public&query=epic-emil   |epic-emil@mailinator.com   |
|ida     |sparr0wHawk |https://www.mailinator.com/v3/index.jsp?zone=public&query=epic-ida    |epic-ida@mailinator.com    |
|alfred  |sparr0wHawk |https://www.mailinator.com/v3/index.jsp?zone=public&query=epic-alfred |epic-alfred@mailinator.com |
|lina    |sparr0wHawk |https://www.mailinator.com/v3/index.jsp?zone=public&query=epic-lina   |epic-lina@mailinator.com   |

## Notable files

### Vuex store modules

* [epiDataModel.js](src/MusicFestival.Vue.Template/Assets/Scripts/store/modules/epiDataModel.js): the module that stores and updates the model object to be displayed on every component.
* [epiContext.js](src/MusicFestival.Vue.Template/Assets/Scripts/store/modules/epiContext.js): makes `inEditMode` and `isEditable` flags available to the OPE helpers.

### On-Page Editing helpers

* [epiEdit.js](src/MusicFestival.Vue.Template/Assets/Scripts/directives/epiEdit.js): a directive that can be added on components to make them optionally editable (e.g. `<span v-epi-edit="Name">`), through `isEditable` and `epiDisableEditing`.
* [EpiProperty.vue](src/MusicFestival.Vue.Template/Assets/Scripts/components/EpiProperty.vue): a component that renders a button to edit a property (e.g. `<epi-property property-name="Name">`).
* [epiMessages.js](src/MusicFestival.Vue.Template/Assets/Scripts/epiMessages.js): registers the `beta/contentSaved` and `beta/epiReady` message handlers that updates the vuex store.

### Routing helpers

* [PageComponentSelector.vue](src/MusicFestival.Vue.Template/Assets/Scripts/components/PageComponentSelector.vue): loads the Vue page component and owns its model.
* [BlockComponentSelector.vue](src/MusicFestival.Vue.Template/Assets/Scripts/components/BlockComponentSelector.vue): loads the Vue block component.
* [EpiLink.vue](src/MusicFestival.Vue.Template/Assets/Scripts/components/widgets/EpiLink.vue): regular links when in OPE and Vue router links otherwise.
* [EpiViewModeLink.vue](src/MusicFestival.Vue.Template/Assets/Scripts/components/widgets/EpiViewModeLink.vue): disables links completely when in OPE.

### API

* [ExtendedContentModelMapper.cs](src/MusicFestival.Vue.Template/Models/ExtendedContentModelMapper.cs): flattens the ContentDeliveryAPI JSON and enables languages.
* [ContentDeliveryExtendedRouting folder](src\MusicFestival.Vue.Template\Infrastructure\ContentDeliveryExtendedRouting): regular routing with friendly URLs is enabled by Johan Björnfot's ContentDeliveryExtendedRouting (see [his github page](https://github.com/jbearfoot/ContentDeliveryExtendedRouting) and [his blog](https://world.episerver.com/blogs/Johan-Bjornfot/Dates1/2018/5/extended-routing-for-episerver-content-delivery-api/)).

## Overall structure

To avoid having multiple razor files the pages and blocks have their own controller and only one razor view each.

```
DefaultPageController.cs
    DefaultPage/Index.cshtml
        DefaultPage.vue
            router-view (Vue.js)
                router.js (Updates the store model with the url)
                    PageComponentSelector.vue (Use the model from the store)
                        ArtistContainerPage/ArtistDetailsPage/LandingPage.vue (Gets the model as a prop)

PreviewController.cs
    Preview/Index.cshtml (sets content-link attribute on <preview>)
        Preview.vue (Use and update the model in the store with the content link)
            BlockComponentSelector.vue (Gets the model as a prop and passes it on)
                BuyTicketBlock/ContentBlock/GenericBlock.vue (Gets the model as a prop)
```

## Building client side resources

* `npm run webpack`: Alias for `npm run webpack-build-dev`.
* `npm run webpack-build-dev`: Builds a development package.
* `npm run webpack-watcher`: Builds a development package, and re-builds automatically when relevant files are changed.
* `npm run webpack-build-prod`: Builds a production package.

## Debugging Vuex state

Using the [Vue-devtools](https://github.com/vuejs/vue-devtools) to see the state changes in the store in view mode works as expected. There are however some limitations to follow state changes when you are editing in Episerver edit mode because of the site is running inside an iframe. To be able to see the vuex state while editing you need to run the stand alone electron app as described on the github page: [Vue standalone Electron app](https://github.com/vuejs/vue-devtools/blob/master/shells/electron/README.md).
