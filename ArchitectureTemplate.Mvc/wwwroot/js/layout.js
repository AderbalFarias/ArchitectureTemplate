$(function () {
    $.fn.reloadSelect = function (data) {
        $(this).each(function (indexM, itemM) {
            var jThis = $(itemM);

            jThis.empty();
            jThis.append($('<option/>', {
                value: "",
                text: "Selecione"
            }));

            try {
                if (data.length > 0) {
                    $.each(data, function (index, item) {
                        jThis.append($('<option/>', {
                            value: item.Value,
                            text: item.Text
                        }));
                    });
                }
            } catch (e) {
                jThis.change();
            }
        });
    };

    $.fn.reloadDictionary = function (data) {
        $(this).each(function (indexM, itemM) {
            var jThis = $(itemM);

            jThis.empty();
            jThis.append($('<option/>', {
                value: "",
                text: "Selecione"
            }));

            try {
                if (data.length > 0) {
                    $.each(data, function (index, item) {
                        jThis.append($('<option/>', {
                            value: item.Key,
                            text: item.Value
                        }));
                    });
                }
            } catch (e) {
                jThis.change();
            }
        });
    };

    $("#alert>.close").click(function () {
        $("#sectionAlert").removeClass("section-alert");
        $("#sectionAlert").addClass("display-none");
    });

    //$("#topMain li a").click(function() {
    //    $(this).parents().children("#topMain li").removeClass("active");
    //    $(this).parent().find("li").addClass("active");
    //});

    $.fn.unmaskMoeda = function () {
        try {
            return $(this).data("mask").replace(/./g, "");
        } catch (e) { };;
    };
});

//Mapeamento de rota
//function UrlRota(controllerName, actionName) {
//    var urlPath = $("#UrlPath").val();
//    var urlRota = urlPath + controllerName + "/" + actionName;

//    return urlRota;
//}

////1=Sucesso, 2=Informacao, 3=aviso e 4=erro
// Depreciado - Substituido por: ShowMesage(kind, title, detail, timeoutSeg)
// Está aqui para garantir integridade do sistema até verificação de chamadas
// OBS: Chamadas antigas automaticamente caem no outro método, mesmo sem o 4º parametro 
function ShowMessage(kind, title, detail) {
    var timeoutSeg = 3;
    ShowMessage(kind, title, detail, timeoutSeg);
}

//1=Sucesso, 2=Informacao, 3=aviso e 4=erro
// No timeout = 30min para timeout da mensagem.
function ShowMessageNoTimeOut(kind, title, detail) {
    var timeoutSeg = 3;
    ShowMessage(kind, title, detail, timeoutSeg);
}

//1=Sucesso, 2=Informacao, 3=aviso e 4=erro
// TimeoutSeg = tempo para o timeout da mensagem, em SEGUNDOS
function ShowMessage(kind, title, detail, timeoutSeg) {
    if (timeoutSeg == undefined || timeoutSeg === "") {
        timeoutSeg = 1;
    }

    var timeout = timeoutSeg * 1000;

    $("#sectionAlert").slideDown(function () {
        setTimeout(function () {
            $("#sectionAlert").slideUp(200);
        }, timeout);
    });       

    $("#alert").removeClass();

    $("#alert").addClass("alert");

    if (kind === "Information") {
        $("#alert").addClass("alert-info");
        $("#tipo").html("<i class=\"fa fa-info-circle\" aria-hidden=\"true\"></i>");
    } else if (kind === "Success") {
        $("#alert").addClass("alert-success");
        $("#tipo").html("<i class=\"fa fa-check-circle-o\" aria-hidden=\"true\"></i>");
    } else if (kind === "Warning") {
        $("#alert").addClass("alert-warning");
        $("#tipo").html("<i class=\"fa fa-exclamation-triangle\" aria-hidden=\"true\"></i>");
    } else if (kind === "Error") {
        $("#alert").addClass("alert-danger");
        $("#tipo").html("<i class=\"fa fa-frown-o\" aria-hidden=\"true\"></i>");
    }

    if (title != null) {
        $("#title").html(title);
    }
}

function ConfirmDialog(url, message) {
    bootbox.confirm(message, function (result) {
        if (result) {
            document.location.href = url;
        }
    });
}

//function addFiltro(i, campo, idInput, hiddenAdd, parentAdd) {
//    var conteudoAtual = $(hiddenAdd).val();
//    var conteudoInserido = $(idInput).val();
//    var appendId = "Append" + campo + i;

//    $(hiddenAdd).val(conteudoAtual + conteudoInserido + ";");
 
//    var texto = "<span id='" + appendId + "' class='text-append'>" +
//        "<a href='#' onclick=\"removeFiltro(\'#" + appendId + "\', \'" + hiddenAdd + "\')\" class='control-label'>" +
//        "<i class='fa fa-minus-square-o ajuste-append'></i></a>" + conteudoInserido + ", </span>";

//    $(parentAdd).append(texto);
//    $(idInput).val("");
//}

//function removeFiltro(id, itens) {
//    var allItens = $(itens).val();
//    var removeItem = $(id).text().replace(", ", ";");
//    var newItens = allItens.replace(removeItem, "");

//    $(itens).val(newItens);
//    $(id).remove();
//    return false;
//}

function SubmitForm(form, url, idPag) {
    if ($("#idPag").length > 0) {
        $("#idPag").val(idPag);
    } else {
        $("#ObjPagination_PaginaAtual").val(idPag);
    }

    $("#"+form).append("<div class='load text-center' tabindex='-1' role='dialog' aria-hidden='false'><i class='fa fa-spinner fa-pulse fa-5x'></i></div>");
    
    var formSubmit = $("form#" + form);
    formSubmit.attr("action", url);
    formSubmit.submit();
}

function AjaxSubmitForm(form, div, url, idPag) {
    $("#"+ form + " #ObjPagination_PaginaAtual").val(idPag);

    var model = $("#" + form).serialize();

    $(div).append("<div class='load text-center' tabindex='-1' role='dialog' aria-hidden='false'><i class='fa fa-spinner fa-pulse fa-5x'></i></div>");

    $.ajax({
        url: url,
        type: "POST",
        data: model,
        dataType: "html",
        cache: false,
        async: true,
        success: function (data) {
            $(div).html(data);
        },
        error: function () {
            ShowMessage("Error", "Ocorreu um problema ao tentar carregar dados", "");
        }
    });
}

function MascaraCpfCnpj(seletor, pf) {
    if (pf === true) {
        $(seletor).mask("000.000.000-00", { reverse: true });
    } else {
        $(seletor).mask("00.000.000/0000-00", { reverse: true });
    }
}

function preencheZeros(valor, tamanho) {
    var qtd = valor.length;
    if (qtd < tamanho) {
        var limite = tamanho - qtd;
        for (var i = 0; i < limite; i++) {
            valor = "0" + valor;
        }
    }

    return valor;
}