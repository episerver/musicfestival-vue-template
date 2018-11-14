import { mapState } from 'vuex';

export default {
    watch: {
        isEditable: '_registerContentSavedEvent'
    },

    computed: mapState({
        isEditable: state => state.context.isEditable
    }),

    methods: {

        _registerContentSavedEvent(isEditable) {
            /**
             * If we enter an editable context we want to enable On-Page Editing
             * and therefore start listening to the "beta/contentSaved" event to
             * be notified when content has been edited. We then update the
             * model and trigger an update of the components.
             */

            if (isEditable) {
                window.epi.subscribe('beta/contentSaved', message => {
                    this.$store.dispatch('updateModelByContentLink', message.contentLink);
                });
            }
        }
    }
};
