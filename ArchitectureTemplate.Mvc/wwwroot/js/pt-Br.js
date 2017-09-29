$(function () {
    $.validator.methods.date = function (value, element) {
        Globalize.culture("pt-BR");
        return this.optional(element) || Globalize.parseDate(value) !== null;
    }

    $.validator.methods.number = function(value, element) {
        Globalize.culture("pt-BR");
        return this.optional(element) || Globalize.parseInt(value) !== null;
    }
});