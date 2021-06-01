import carregaMesAno from './carrega-mesAno.js';
(() => {

    $(window).on("load", function (evento) {
        evento.preventDefault();

        carregaMesAno(".lblMesAno");

        var btnRela = $('[data-btn-rela]');

        for (var i = 0; i < btnRela.length; i++) {

            var trBtn = btnRela[i].parentElement
                .parentElement
                .parentElement;

            var tdTipo = btnRela[i].parentElement.parentElement
                .previousElementSibling
                .previousElementSibling
                .previousElementSibling
                .previousElementSibling
                .previousElementSibling;

            if (tdTipo.textContent.trim() == "S") {
                btnRela[i].classList.add('btn-rela-none');
                var nextTR = trBtn.nextElementSibling;
                nextTR.remove();
            }
        }
    })


    $('[data-btn-rela]').on('click', function (event) {
        event.preventDefault();
        debugger;
        var $this = $(this);
        var thisTR = $(this).parent().parent().parent();
        var nextTROculta = thisTR.next();
        var brothersTR = thisTR.siblings('tr.conta-df');

        if ($this.hasClass('glyphicon-triangle-left')) {
            $this.toggle().removeClass('glyphicon-triangle-left');
            $this.toggle().addClass('glyphicon-triangle-bottom');
        }
        else {
            $this.toggle().removeClass('glyphicon-triangle-bottom');
            $this.toggle().addClass('glyphicon-triangle-left');
        }

        //nextTROculta.removeClass('data-add-cosif-show');

        setTimeout(function () {
            brothersTR.fadeToggle().addClass('conta-df-hide');

            setTimeout(function () {
                nextTROculta.fadeToggle().addClass('data-add-cosif-show');

            }, 300);
        }, 300);

    })

})()
