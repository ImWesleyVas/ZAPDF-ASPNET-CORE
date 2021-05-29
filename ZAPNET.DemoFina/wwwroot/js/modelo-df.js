import carregaMesAno from './carrega-mesAno.js';
(() => {

    $(window).on("load", function(evento) {
        evento.preventDefault();
        carregaMesAno(".lblMesAno");

        $('[data-table-modelos]').fadeIn();
        $('[data-form-date]').prop('disabled', true);
        $('[data-btn-mes-ano]').prop('disabled', true);
        $("#addModelo").fadeIn();
    })


    $('[data-btn-cancel-mes-ano]').click(function (evento) {
        evento.preventDefault();

        $('[data-table-modelos]').fadeOut();
        $('[data-form-date]').prop('disabled', false);
        $('[data-btn-mes-ano]').prop('disabled', false);
        $('[data-form-date]').val("");
        $("#addModelo").fadeOut();
        $(".lblMesAno").fadeOut();
    })    

})()
