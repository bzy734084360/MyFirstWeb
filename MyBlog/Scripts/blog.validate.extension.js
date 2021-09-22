jQuery.validator.addMethod("myvalidation", function (value, element, params) {
    return value == "selim";
})
jQuery.validator.unobtrusive.adapters.add("myvalidation", [], function (options) {
    options.message["myvalidation"] = options.message;
    options.rules["myvalidation"] = {};
})