jQuery.validator.addMethod("myvalidation", function (value, element, params) {
    return value == 'bzy';
});

jQuery.validator.unobtrusive.adapters.add("myvalidation", [], function (options) {
    options.messages["myvalidation"] = options.message;
    options.rules["myvalidation"] = {};
});