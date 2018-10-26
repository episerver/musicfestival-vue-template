# MusicFestival Vue.js Templates

This project uses npm 6+ and Visual Studio 2015+.

This sample site demonstrates one approach to render Episerver content in a client side framework that is using client side routing for navigation with a working On Page Edit (OPE) mode in the Episerver UI.

This particular solution uses Vue.js but most of the techniques are framework agnostic and can be used with any other framework, such as React or Angular.

Content is fetched from Episerver using the Content Delivery API: https://world.episerver.com/documentation/developer-guides/CMS/Content/content-delivery-api/

## Setup and Run

1. Run `setup.cmd`
2. Open `MusicFestival.Vue.Template.sln`
3. Hit `Ctrl + F5`
4. Login with either one of the following:

|Name    |Password    |Mailbox | Email |
|--------|------------|--------|-------|
|cmsadmin|sparr0wHawk |        |       |
|emil    |sparr0wHawk |https://www.mailinator.com/v3/index.jsp?zone=public&query=epic-emil   |epic-emil@mailinator.com   |
|ida     |sparr0wHawk |https://www.mailinator.com/v3/index.jsp?zone=public&query=epic-ida    |epic-ida@mailinator.com    |
|alfred  |sparr0wHawk |https://www.mailinator.com/v3/index.jsp?zone=public&query=epic-alfred |epic-alfred@mailinator.com |
|lina    |sparr0wHawk |https://www.mailinator.com/v3/index.jsp?zone=public&query=epic-lina   |epic-lina@mailinator.com   |

## Notable files

### On-Page Editing helpers

* [epiEdit.js](src/MusicFestival.Vue.Template/Assets/Scripts/directives/epiEdit.js): a directive that can be added on components to make them editable (e.g. `<span v-epi-edit="Name">`).
* [EpiProperty.vue](src/MusicFestival.Vue.Template/Assets/Scripts/components/EpiProperty.vue): a component that renders a button to edit a property (e.g. `<epi-property property-name="Name">`).
* [epiDataModelMixin.js](src/MusicFestival.Vue.Template/Assets/Scripts/Mixins/epiDataModelMixin.js): adds a `model` property to any component and keeps it updated when content is updated.
* [epiContext.js](src/MusicFestival.Vue.Template/Assets/Scripts/epiContext.js): makes `inEditMode` and `isEditable` flags available to the OPE helpers.

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
        Site.vue
            router-view (Vue.js)
                router.js
                    PageComponentSelector.vue (owns the model)
                        ArtistContainerPage/ArtistDetailsPage/LandingPage.vue
 
PreviewController.cs
    Preview/Index.cshtml (sets content-link attribute on <preview>)
        Preview.vue (owns the model, and renders multiple BlockComponentSelector)
            BlockComponentSelector.vue
                BuyTicketBlock/ContentBlock/GenericBlock.vue
```

## Building client side resources

* `npm run webpack`: Alias for `npm run webpack-build-dev`.
* `npm run webpack-build-dev`: Builds a development package.
* `npm run webpack-watcher`: Builds a development package, and re-builds automatically when relevant files are changed.
* `npm run webpack-build-prod`: Builds a production package.
