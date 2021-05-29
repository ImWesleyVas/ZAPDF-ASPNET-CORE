
var meses = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];

function carregaMesAno(elemento) {
    var ano = $(elemento).text().substr(0, 4);
    var mes = $(elemento).text().substr(5, 2);
    $(elemento).text(meses[parseInt(mes) - 1] + " / " + ano).fadeIn();
}

export default carregaMesAno;
