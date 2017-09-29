$(function () {
    $("#btnSincronizar").click(function () {
        var menus = [];

        $.ajax({
            url: "/Menu/GetHtml",
            type: "GET",
            dataType: "json",
            data: {},
            success: function (data) {
                if (data !== false) {
                    var htmlMenus = $.parseHTML(data);

                    $("#menu-lateral li", htmlMenus).each(function () {
                        menus.push($(this).find("a:first>span").attr("id").replace(/Menu_/g, "") + "|" + $(this).find("a:first>span").text().replace(/\r?\n|\r/g, ""));
                    });

                    $("#menu-top li.dropdown", htmlMenus).each(function () {
                        menus.push($(this).attr("id").replace(/Menu_/g, "") + "|" + $(this).find("a.dropdown-toggle>span").text());
                    });

                    $("#menu-top li.dropdown li", htmlMenus).each(function () {
                        menus.push($(this).attr("id").replace(/Menu_/g, "") + "|" + $(this).find("a:first").text());
                    });

                    $("#menu-top li[menu=principal]", htmlMenus).each(function () {
                        menus.push($(this).attr("id").replace(/Menu_/g, "") + "|" + $(this).find("a:first").text());
                    });

                    $.ajax({
                        url: "/Menu/Synchronize",
                        type: "POST",
                        dataType: "json",
                        traditional: true,
                        data: { menuList: menus },
                        success: function (dataResult) {
                            if (dataResult === true) {
                                ShowMessage("Success", "Menus sincronizados com sucesso", null, 3);
                                window.location.href = "/Menu/Index";
                            } else {
                                ShowMessage("Error", "Ocorreu um problema inesperado ao tentar sincronizados os dados", null, 2);
                            }
                        },
                        error: function () {
                            ShowMessage("Error", "Ocorreu um problema inesperado ao tentar sincronizados os dados", null, 2);
                        }
                    });
                } else {
                    ShowMessage("Error", "Ocorreu um problema inesperado ao tentar carregar dados", null, 2);
                }
            },
            error: function () {
                ShowMessage("Error", "Ocorreu um problema inesperado ao tentar carregar dados", null, 2);
            }
        });

    });
});

$(document).ready(function () {
    if ($("#Scroll").val() > 0) {
        $(window).scrollTop($("#Scroll").val());
    }

    $("#PerfilId").change(function () {
        var perfilId = $(this).val();

        if (perfilId != 0) {
            $.ajax({
                url: "/Menu/List",
                type: "Get",
                data: { id: perfilId },
                dataType: "html",
                cache: false,
                success: function (data) {
                    $("#DivDados").html(data);
                },
                error: function () {
                    ShowMessage("Error", "Ocorreu um problema inesperado ao tentar carregar os dados", null, 2);
                }
            });
        } else {
            window.location.href = "/Menu/Index";
        }
    });

    $("#DivDados").on("click", "a", function () {
        var scroll = $(window).scrollTop();
        $("#Scroll").val(scroll);
        window.location.href = $(this).attr("link") + "&scroll=" + scroll;
    });
});