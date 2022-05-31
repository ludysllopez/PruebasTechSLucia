// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#ObtenerTokenSubcription").submit(function (event) {
        event.preventDefault();
        obtenerToken();
    });

    $("#bcrearSuscripción").submit(function (event) {
        event.preventDefault();
        createSubscription();
    });

    $("#bAutorizationPayment").submit(function (event) {
        alert();
        event.preventDefault();
        preAutorizationPayment();
        
    });
});

var kushki = new Kushki({
    merchantId: '827e213983ea4cbb968b9a0493c0e5ca',
    inTestEnvironment: true,
    regional: false
});

var kushki2 = new KushkiCheckout({
    kformId: "8TyREgpdV",
    form: "my-form",
    publicMerchantId: "827e213983ea4cbb968b9a0493c0e5ca",// Reemplaza esto por tucredencial pública
    inTestEnvironment: true,
    amount: {
        subtotalIva: 0,
    iva: 0,
    subtotalIva0: 1000,
     }
 });

var callback = function (response) {
    limpiarMensajes();
    if (!response.code) {
        $("#error").addClass("d-none");
        $("#succes").removeClass("d-none");
        $("#succes").text(response.message);
    } else {
        $("#succes").addClass("d-none");
        $("#error").removeClass("d-none");
        $("#error").text(response.message);
    }
}

// Also you can set the function directly

function obtenerToken() {
    kushki.requestSubscriptionToken({
        card: {
            name: $("#nombreST").val(),
            number: $("#tarjetaST").val(),
            cvc: $("#ccvST").val(),
            expiryMonth: $("#expiryMonthST").val(),
            expiryYear: $("#expiryYearST").val()
        },
        currency: "PEN"
    }, callback);
}

function habilitarSubcriptionToken() {
    limpiarMensajes();
    if ($("#subscriptionToken").hasClass("d-none")) {
        ocultarcreateToken();
        ocutarAutorizacionPagos();
        mostrarsubscriptionToken();
        ocultarObtenerSubscripcion();
       
    }
}

function mostrarsubscriptionToken() {
    $("#subscriptionToken").removeClass("d-none");
    $("#subcription1").removeClass("btn-secondary");
    $("#subcription1").addClass("btn-primary");
}

function ocultarsubscriptionToken() {
    $("#subscriptionToken").addClass("d-none");
    $("#subcription1").removeClass("btn-primary");
    $("#subcription1").addClass("btn-secondary");
}

function habilitarCrearToken() {
    limpiarMensajes();
    if ($("#createToken").hasClass("d-none")) {
        mostrarcreateToken();
        ocultarsubscriptionToken();
        ocutarAutorizacionPagos();
        ocultarcrearPagos();
        ocultarObtenerSubscripcion();
    }
}

function mostrarcreateToken() {
    $("#createToken").removeClass("d-none");
    $("#subcription2").removeClass("btn-secondary");
    $("#subcription2").addClass("btn-primary");
}

function ocultarcreateToken() {
    $("#createToken").addClass("d-none");
    $("#subcription2").removeClass("btn-primary");
    $("#subcription2").addClass("btn-secondary");
}

function habilitarAutorizacionPagos() {
    limpiarMensajes();
    if ($("#AutorizationPayment").hasClass("d-none")) {
        mostrarAutorizacionPagos();
        ocultarcreateToken();
        ocultarsubscriptionToken();
        ocultarcrearPagos();
        ocultarObtenerSubscripcion();
        
    }
}

function mostrarAutorizacionPagos() {
    $("#AutorizationPayment").removeClass("d-none");
    $("#Autorizacion1").removeClass("btn-secondary");
    $("#Autorizacion1").addClass("btn-primary");
}

function ocutarAutorizacionPagos() {
    $("#AutorizationPayment").addClass("d-none");
    $("#Autorizacion1").removeClass("btn-primary");
    $("#Autorizacion1").addClass("btn-secondary");
}


function limpiarMensajes() {
    $("#ObtenerTokenSubcription")[0].reset();
    $("#fcreateToken")[0].reset();
    $("#fAutorizationPayment")[0].reset();
    $("#error").addClass("d-none");
    $("#succes").addClass("d-none");
}

function createSubscription() {
    loadSpinner();
    $.ajax({
        type: "POST",
        url: '/Home/crearSubcripcion',
        dataType: "json",
        success: function (result) {
            console.log("OK");
            console.log(result);
            closeSpinner();
            mostrarMensaje(result);
        },
        error: function (error) {
            console.log(error);
            closeSpinner();
            mostrarMensaje(error);
        }
    });   
}

function preAutorizationPayment() {
    loadSpinner();
    var data = [];
 
    if ($("#TokenAP").val() != null && $("#TokenAP").val() != "") {
        var t = $("#TokenAP").val().toString();
        data[0] = t;
        $.ajax({
            type: "POST",
            url: '/Home/preAutorizarPago',
            dataType: "json",
            data: { d: data },
            success: function (result) {
                console.log("OK");
                console.log(result);
                closeSpinner();
                mostrarMensaje(result);

            },
            error: function (error) {
                console.log(error);
                mostrarMensaje(error);
                closeSpinner();
            }
        });
    } else {
        $("#TokenAP").addClass("alert");
    }
    
}


function habilitarcrearPagos() {
    limpiarMensajes();
    if ($("#createPayment").hasClass("d-none")) {
        mostrarCrearPagos()
        ocutarAutorizacionPagos();
        ocultarcreateToken();
        ocultarsubscriptionToken();
        ocultarObtenerSubscripcion();
    }
}

function mostrarCrearPagos(){
    $("#createPayment").removeClass("d-none");
    $("#Payment1").removeClass("btn-secondary");
    $("#Payment1").addClass("btn-primary");
}
function ocultarcrearPagos() {
    $("#createPayment").addClass("d-none");
    $("#Payment1").addClass("btn-secondary");
    $("#Payment1").removeClass("btn-primary");
}

function GetSubscription() {
    loadSpinner();
    if ($("#subscription").val() != null && $("#subscription").val() != "") {
        var t = $("#subscription").val().toString();
        var datos = t; 
        $.ajax({
            type: "POST",
            url: '/Home/GetSubscription',
            contentType: "text/plain",
            data: datos,
            success: function (result) {
                console.log("OK");
                console.log(result);
                closeSpinner();
                mostrarMensaje(result);

            },
            error: function (error) {
                console.log(error);
                mostrarMensaje(error);
                closeSpinner();
            }
        });
    } else {
        $("#subscription").addClass("alert");
    }

}

function habilitarObtenerSubscripcion() {
    limpiarMensajes();
    if ($("#getsubscription").hasClass("d-none")) {
        mostrarObtenerSubscripcion();
        ocultarcrearPagos();
        ocutarAutorizacionPagos();
        ocultarcreateToken();
        ocultarsubscriptionToken();
    }
}

function mostrarObtenerSubscripcion() {
    $("#getsubscription").removeClass("d-none");
    $("#subcription3").removeClass("btn-secondary");
    $("#subcription3").addClass("btn-primary");
}
function ocultarObtenerSubscripcion() {
    $("#getsubscription").addClass("d-none");
    $("#subcription3").addClass("btn-secondary");
    $("#subcription3").removeClass("btn-primary");
}



function mostrarMensaje(response) {
    limpiarMensajes();
    if (!response.code) {
        $("#error").addClass("d-none");
        $("#succes").removeClass("d-none");
        $("#succes").text(response.message);
    } else {
        $("#succes").addClass("d-none");
        $("#error").removeClass("d-none");
        $("#error").text(response.message);
    }
}

function loadSpinner() {
    $('#dvCargando').css('visibility', 'visible');
}

function closeSpinner() {
    $('#dvCargando').css('visibility', 'hidden');
}






