import carregaMesAno from './carrega-mesAno.js';
(() => {

    $(window).on("load", function (evento) {
        evento.preventDefault();
        debugger;

        carregaMesAno(".lblMesAno");

        var btnRela = $('button.btn.btn-default.glyphicon.glyphicon-triangle-bottom');

        for (var i = 0; i < btnRela.length; i++) {

            var pai = btnRela[i].parentElement;
            var avo = pai.parentElement;
            var bisavo = avo.parentElement;

            var titioAvo = avo.previousElementSibling
                .previousElementSibling
                .previousElementSibling
                .previousElementSibling
                .previousElementSibling;

            if (titioAvo.textContent.trim() == "S") {
                btnRela[i].classList.add('btn-rela-none');
                var titioBisavo = bisavo.nextElementSibling;
                titioBisavo.remove();
            }
        }

    })

    $('[data-btn-rela]').on('click', function (event) {
        event.preventDefault();
        /*debugger;*/
        var paiButton = this.parentElement;
        var avoButton = paiButton.parentElement;
        var bisaboButton = avoButton.parentElement;
        var titioBisavo = bisaboButton.nextElementSibling;
        
        titioBisavo.classList.toggle('data-add-cosif-show');

        console.log(titioBisavo);

    })

})()
