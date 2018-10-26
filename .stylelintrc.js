
// Use the editorconfig settings in order to set the stylelint settings
const editorconfig = require('editorconfig').parseSync('./editorconfig');

module.exports = Object.assign({},
    { rules:
        {
            'no-missing-end-of-source-newline': editorconfig.insert_final_newline,
            'indentation': editorconfig.indent_size,
            'no-eol-whitespace': editorconfig.trim_trailing_whitespace
        }
    });
