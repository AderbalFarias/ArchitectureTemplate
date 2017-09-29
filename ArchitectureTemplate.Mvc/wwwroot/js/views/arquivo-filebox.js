$(function () {
    var docId;

    $(".indexar-li>a").droppable({
        hoverClass: "droppable-hover",
        drop: function (event) {
            docId = $(event.target).attr("id");
        }
    });

    $("div").on("mouseenter", ".file-preview-frame", function (e) {
        e.preventDefault();
        $(this).draggable({
            opacity: 0.35,
            revert: "invalid",
            containment: "docunent",
            cursor: "move",
            //start: function (event, ui) {
            //    //implementar
            //},
            stop: function (event) {
                if (docId !== undefined && docId !== "") {
                    var docIdDestino = parseInt(docId);
                    var ddId = $(event.target).attr("id").replace(/file_/g, "");
                    var docOrigemId = $("#OrigemFile").val();
                    var processoId = $("#ProcessoId").val();

                    $.ajax({
                        url: "/Arquivo/AtribuirDocumento",
                        type: "POST",
                        data: { processoId: processoId, ddId: ddId, docId: docId, docOrigemId },
                        dataType: "html",
                        cache: false,
                        success: function (data) {
                            if (docIdDestino === 0) {
                                $("div[role=tabpanel]").each(function () {
                                    var spanQtdeDestino = $(this).find("li.indexar-li:first > a > span");
                                    var qtdeDestino = parseInt(spanQtdeDestino.text().replace(/\(Qtde./g, "").replace("(", "").replace(")", "")) + 1;
                                    spanQtdeDestino.text("(Qtde. " + qtdeDestino + ")");
                                });
                            } else {
                                var spanQtdeDestino = $(".indexar-li > #" + docIdDestino).parent().find("span");
                                var qtdeDestino = parseInt(spanQtdeDestino.text().replace(/\(Qtde./g, "").replace(")", "")) + 1;
                                spanQtdeDestino.text("(Qtde. " + qtdeDestino + ")");
                            }

                            if (docOrigemId === "0") {
                                $("div[role=tabpanel]").each(function () {
                                    var spanQtdeOrigem = $(this).find("li.indexar-li:first > a > span");
                                    var qtdeOrigem = parseInt(spanQtdeOrigem.text().replace(/\(Qtde./g, "").replace(")", "")) - 1;
                                    spanQtdeOrigem.text("(Qtde. " + qtdeOrigem + ")");
                                });
                            } else {
                                var spanQtdeOrigem = $(".indexar-li > #" + docOrigemId).parent().find("span");
                                var qtdeOrigem = parseInt(spanQtdeOrigem.text().replace(/\(Qtde./g, "").replace(")", "")) - 1;
                                spanQtdeOrigem.text("(Qtde. " + qtdeOrigem + ")");
                            }

                            $("#divBoxFiles").html(data);
                        },
                        error: function () {
                            ShowMessage("Error", "Ocorreu um problema inesperado ao tentar indexar a imagem ao documento", null, 2);
                        }
                    });

                    $(this).draggable("destroy");
                    docId = "";
                }
            }
        });
    });
});