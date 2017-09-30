$(function() {
    $("#menu-lateral>nav>ul>li>a").click(function() {
        var getItens = $(this).parent();
        var getOpen = getItens.find(".fa-plus-square-o").val();

        if (getOpen !== undefined) {
            $("#menu-lateral>nav>ul>li>a>b>em.fa.fa-minus-square-o").addClass("fa-plus-square-o");
            $("#menu-lateral>nav>ul>li>a>b>em.fa.fa-minus-square-o.fa-plus-square-o").removeClass("fa-minus-square-o");
            $("#menu-lateral>nav>ul>li>ul").slideUp(300);
            getItens.addClass("active open");
            getItens.find("a>b>em").removeClass("fa-plus-square-o");
            getItens.find("a>b>em").addClass("fa-minus-square-o");
            getItens.children("ul").slideDown(300);
        }

        if (getItens.attr("children") === "false") {
            $("#menu-lateral>nav>ul>li>ul").slideUp(300);
            $("#menu-lateral>nav>ul>li>a>b>em.fa.fa-minus-square-o").addClass("fa-plus-square-o");
            $("#menu-lateral>nav>ul>li>a>b>em.fa.fa-minus-square-o.fa-plus-square-o").removeClass("fa-minus-square-o");
            $("#menu-lateral>nav>ul>li>ul").removeClass("display-block");
            getItens.addClass("active");
        }
    });

    $("#menu-lateral>nav>ul>li>ul>li>a").click(function () {
        var getItens = $(this).parent();
        var getActive = getItens.find(".active").val();

        if (getActive === undefined) {
            $("#menu-lateral>nav>ul>li>ul>li").removeClass();
            getItens.addClass("active");
        }
    });

    $("#aside-nav button.btn-mobile").click(function () {
        $(this).animate({ "left": "-200" }, 200);
        $("#menu-lateral").removeClass("nav-right-collapse").animate({ left: 0 }, "slow");
        $(".menu-close").removeClass("display-none").animate({ right: 15 }, "slow");
    });

    //poderá causar problemas 
    $("#aside-nav .menu-close").click(function () {
        $("#aside-nav button.btn-mobile").animate({ "left": 15 }, 200);
        $("#menu-lateral").addClass("nav-right-collapse");
        $(".menu-close").addClass("display-none");
    });

    $("#menu-lateral li a.linkto").click(function () {
        if ($(this).attr("linkto").length !== 0 && $("#TestId").val() > 0) {
            window.location.href = $(this).attr("linkto") + $("#TestId").val();
        }
    });

    if ($("#TestId").val() > 0) {
        $("#aside-nav").removeClass();

        $.ajax({
            url: "/Pesquisa/GetInfo",
            type: "GET",
            data: { id: $("#TestId").val() },
            dataType: "html",
            cache: false,
            success: function (data) {
                $("#aside-info").html(data);
                $("#aside-info").removeClass();

                var html = $.parseHTML(data);
                $("p.list-group-item-text", html).each(function () {
                    if ($(this).text().trim() === "Repasse") {
                        $("li[tipo='repasse']").removeClass();
                    } else {
                        $("li[tipo='repasse']").attr("display-none");
                    }
                });
            },
            error: function () {
                ShowMessage("Error", "Ocorreu um problema inesperado ao tentar carregar dados", null, 3);
            }
        });
    }
});

