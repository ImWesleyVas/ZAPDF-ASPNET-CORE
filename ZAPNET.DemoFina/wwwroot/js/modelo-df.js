(() => {
        
    var meses = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];

    $(window).on("load", function(evento) {
        evento.preventDefault();

        var ano = $(".lblMesAno").text().substr(0, 4);
        var mes = $(".lblMesAno").text().substr(5, 2);
        $(".lblMesAno").text(meses[parseInt(mes) - 1] + " / " + ano).fadeIn();

        $('[data-table-modelos]').fadeIn();
        $('[data-form-date]').prop('disabled', true);
        $("#addModelo").fadeIn();
    })


    $('[data-btn-cancel-mes-ano]').click(function (evento) {

        evento.preventDefault();

        $('[data-form-date]').text("");
        $('[data-table-modelos]').fadeOut();
        inputMesAno = "";
        $('[data-form-date]').prop('disabled', false);
        $("#addModelo").fadeOut();
        $(".lblMesAno").fadeOut();
    })    

})()
