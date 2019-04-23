module.exports = {
    "root": true,
    "env": {
        "browser": true,
        "commonjs": true
    },
    "parserOptions": {
        "sourceType": "module"
    },
    "plugins": [
        "@episerver/cms",
        "vue"
    ],
    "extends": [
        "eslint:recommended",
        "plugin:@episerver/cms/recommended",
        "plugin:vue/essential"
    ],
    "rules": {
        "array-bracket-spacing": ["error", "never"],
        "brace-style": "error",
        "comma-style": ["error", "last"],
        "curly": ["error", "all"],
        "eol-last": "error",
        "eqeqeq": ["error", "smart"],
        "indent": ["error", 4, { "SwitchCase": 1 }],
        "key-spacing": ["error", { "afterColon": true }],
        "keyword-spacing": "error",
        "no-cond-assign": ["error", "except-parens"],
        "no-console": ["error", { "allow": ["error"] }],
        "no-trailing-spaces": "error",
        "no-unused-expressions": ["error", { "allowTernary" : true, "allowShortCircuit": true }],
        "no-unused-vars": ["error", { "args": "none" }],
        "no-use-before-define": "error",
        "no-useless-escape": "off",
        "no-with": "error",
        "prefer-arrow-callback": "error",
        "quote-props": ["error", "as-needed", { "keywords": true }],
        "quotes": ["error", "single", { "avoidEscape": true }],
        "semi": ["error", "always"],
        "space-before-blocks": ["error", "always"],
        "space-before-function-paren": ["error", { "anonymous": "always", "named": "never" }],
        "space-in-parens": ["error", "never"],
        "space-infix-ops": "error",
        "space-unary-ops": "off",
        "strict": "off"
    }
};
