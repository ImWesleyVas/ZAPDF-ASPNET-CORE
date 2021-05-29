import carregaMesAno from './carrega-mesAno.js';
(() => {

    $(window).on("load", function (evento) {
        evento.preventDefault();

        debugger;

        carregaMesAno(".lblMesAno");

        var btnRela = $('a.btn.btn-default.glyphicon.glyphicon-triangle-bottom');

        for (var i = 0; i < btnRela.length; i++) {
            
            var pai = btnRela[i].parentElement;
            var avo = pai.parentElement;
            var titioAvo = avo.previousElementSibling
                .previousElementSibling
                .previousElementSibling
                .previousElementSibling;
            
            if (titioAvo.textContent.trim() == "S") {
                btnRela[i].classList.add('btn-rela-none');
            }
        }
    })

})()
