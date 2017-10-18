$(document).ready(function () {
    $.fn.extend({
        treed: function (o) {
            var openedClass = 'glyphicon-minus-sign';
            var closedClass = 'glyphicon-plus-sign';

            if (typeof o != 'undefined') {
                if (typeof o.openedClass != 'undefined') {
                    openedClass = o.openedClass;
                }
                if (typeof o.closedClass != 'undefined') {
                    closedClass = o.closedClass;
                }
            };

            //initialize each of the top levels
            var tree = $(this);
            tree.addClass("tree");
            tree.find('li').has("ul").each(function () {
                var branch = $(this); //li with children ul
                branch.prepend("<i class='indicator glyphicon " + closedClass + "'></i>");
                branch.addClass('branch');
                branch.on('click', function (e) {
                    if (this == e.target) {
                        var icon = $(this).children('i:first');
                        icon.toggleClass(openedClass + " " + closedClass);
                        $(this).children().children().toggle();
                    }
                });
                branch.children().children().toggle();
            });
            //fire event from the dynamically added icon
            tree.find('.branch .indicator').each(function () {
                $(this).on('click', function () {
                    $(this).closest('li').click();
                });
            });

            //fire event to open branch if the li contains a button instead of text
            tree.find('.branch>button').each(function () {
                $(this).on('click', function (e) {
                    $(this).closest('li').click();
                    e.preventDefault();
                });
            });
        }
    });

    //$("#ulTreeOne").treed({ openedClass: "fa fa-minus-circle", closedClass: "fa fa-plus-circle" });

    ////$("#ulTreeOne ul").each(function () {
    ////    $(this).show();
    ////});

    ////$("#ulTreeOne li").each(function () {
    ////    $(this).show();
    ////});

    $("#ulTreeOne").treed({ openedClass: "fa fa-plus-circle", closedClass: "fa fa-minus-circle" });

    $("#ulTreeOne ul").each(function () {
        $(this).show();
    });

    $("#ulTreeOne li").each(function () {
        $(this).show();
    });

    $("#ulTreeOne a>i").each(function () {
        $(this).show();
    });

    $(".branch").click(function () {
        $("#ulTreeOne a>i").each(function () {
            $(this).show();
        });
    });



    if ($("#HierarchyDetalhe_CpfCnpj").length > 0) {
        MascaraCpfCnpj("#HierarchyDetalhe_CpfCnpj", $("#HierarchyDetalhe_PessoaFisica").prop("checked"));
        var cpfCnpj = $("#HierarchyDetalhe_CpfCnpj").unmask().val();

        if (cpfCnpj === "0" || cpfCnpj === "")
            $("#HierarchyDetalhe_CpfCnpj").val("");
        else {
            if (cpfCnpj.length > 11) {
                $("#HierarchyDetalhe_CpfCnpj").val(preencheZeros(cpfCnpj, 14)).mask("00.000.000/0000-00", { reverse: true });
            } else {
                $("#HierarchyDetalhe_CpfCnpj").val(preencheZeros(cpfCnpj, 11)).mask("000.000.000-00", { reverse: true });
            }
        }

        $("#HierarchyDetalhe_PessoaFisica").click(function () {
            MascaraCpfCnpj("#HierarchyDetalhe_CpfCnpj", $(this).prop("checked"));
        });

        var spMaskBehavior = function (val) {
            return val.replace(/\D/g, "").length === 11 ? "(00) 00000-0000" : "(00) 0000-00009";
        }, spOptions = {
            onKeyPress: function (val, e, field, options) {
                field.mask(spMaskBehavior.apply({}, arguments), options);
            }
        };

        $("#HierarchyDetalhe_Telefone").mask(spMaskBehavior, spOptions);
        $("#HierarchyDetalhe_Codigo").mask("00000000");
    }

    $("#formHierarchy").on("submit", function () {
        if ($(this).valid()) {
            $("#HierarchyDetalhe_CpfCnpj").unmask();
            $("#HierarchyDetalhe_Telefone").unmask();
        }
    });
});
