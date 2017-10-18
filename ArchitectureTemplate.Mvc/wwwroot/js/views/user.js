$(document).ready(function () {
    $("#Cpf").mask("000.000.000-00", { reverse: true });

    var spMaskBehavior = function (val) {
        return val.replace(/\D/g, "").length === 11 ? "(00) 00000-0000" : "(00) 0000-00009";
    }, spOptions = {
        onKeyPress: function (val, e, field, options) {
            field.mask(spMaskBehavior.apply({}, arguments), options);
        }
    };

    $("#Telefone").mask(spMaskBehavior, spOptions);

    $("#DataExpiracaoSenha").datepicker({ altFormat: "dd-MM-yyyy" });
    $("#InvalidacaoProgramadaInicio").datepicker({ altFormat: "dd-MM-yyyy" });
    $("#InvalidacaoProgramadaFim").datepicker({ altFormat: "dd-MM-yyyy" });

    $("#DataExpiracaoSenha").change(function () {
        $(this).datepicker("hide");
    });

    $("#InvalidacaoProgramadaInicio").change(function () {
        $(this).datepicker("hide");
    });

    $("#InvalidacaoProgramadaFim").change(function () {
        $(this).datepicker("hide");
    });

    $("#formUser").on("submit", function () {
        if ($(this).valid()) {
            $("#Cpf").unmask();
            $("#Telefone").unmask();
        }
    });
});


