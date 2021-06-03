// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.





$(".btn").click(function (evento) {
    //debugger;
    //evento.preventDefault();
    var btn = $(this).find(".spinner");
  
    btn.toggle();
    setTimeout(function () { }, 1500);
})

$(".btn-import").click(function (evento) {
    debugger;
    //evento.preventDefault();
    var spanTag = $("span.spinner");
    console.log(spanTag);

    spanTag.addClass('spinnerGrande');
    spanTag.toggle();

    setTimeout(function () { }, 1500);


})