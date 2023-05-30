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
    merchantId: '5ae519fb18c14f4f88e4defddf7cf699',
    inTestEnvironment: true
});

var kushki2 = new KushkiCheckout({
    kformId: "8TyREgpdV",
    form: "my-form",
    publicMerchantId: "5ae519fb18c14f4f88e4defddf7cf699",// Reemplaza esto por tucredencial pública
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
        $("#success").addClass("d-none");
        $("#error").removeClass("d-none");
        $("#error").text(response.message);
    } else {
        $("#error").addClass("d-none");
        $("#success").removeClass("d-none");
        $("#success").text(response.message);
        
    }
}

var callbackRequestSubscriptionToken = function (response) {
    limpiarMensajes();
    if (response.token) {
        $("#error").addClass("d-none");
        $("#success").removeClass("d-none");
        $("#success").text("Token generado: " + response.token); 
        localStorage.setItem('token', response.token);
    } else {        
        $("#success").addClass("d-none");
        $("#error").removeClass("d-none");
        $("#error").text(response.message);
    }
    closeSpinner();

}

// Also you can set the function directly


function loadSpinner() {
    $('#dvCargando').css('visibility', 'visible');
}

function closeSpinner() {
    $('#dvCargando').css('visibility', 'hidden');
}

function obtenerToken() {
    loadSpinner();
    kushki.requestSubscriptionToken({
        card: {
            name: $("#nombreST").val(),
            number: $("#tarjetaST").val(),
            cvc: $("#ccvST").val(),
            expiryMonth: $("#expiryMonthST").val(),
            expiryYear: $("#expiryYearST").val()
        },
        currency: "USD"
    }, callbackRequestSubscriptionToken);
}

function createSubscription() {
    loadSpinner();
    var token;
    var form = $('#fcreateToken')[0];
    var formData = new FormData(form);      

    $.ajax({
        type: "POST",
        url: getUrlCreateSubcription(),
        dataType: "json",
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result.subscriptionId) {
                console.log("OK");
                localStorage.setItem('subscriptionId', result.subscriptionId);
                $("#error").addClass("d-none");
                $("#success").removeClass("d-none");
                $("#success").text("subscriptionId : " + result.subscriptionId);
                closeSpinner();
            } else {
                mostrarMensaje(result);
            }
            
        },
        error: function (error) {
            console.log(error);
            closeSpinner();
            mostrarMensaje(error);
        }
    }).done((res) => {
        console.log(res)
    });
}

function preAutorizationPayment() {
    loadSpinner();
    var form = $('#fAutorizationPayment')[0];
    var formData = new FormData(form);

    $.ajax({
        type: "POST",
        url: getUrlCreateAutorization(),
        dataType: "json",
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result.ticketNumber) {
                console.log("OK");
                localStorage.setItem('ticketNumber', result.ticketNumber);
                $("#error").addClass("d-none");
                $("#success").removeClass("d-none");
                $("#success").text("ticketNumber : " + result.ticketNumber);
                closeSpinner();
            } else {
                mostrarMensaje(result);
            }
        },
        error: function (error) {
            console.log(error);
            closeSpinner();
            mostrarMensaje(error);
        }
    }).done((res) => {
        console.log(res)
    }); 
}

function getAutorizationPayment() {
    loadSpinner();
    var form = $('#fGetAutorizationPayment')[0];
    var formData = new FormData(form);

    $.ajax({
        type: "POST",
        url: getUrlGetCreateAutorization(),
        dataType: "json",
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result.transactionReference) {
                console.log("OK");
                localStorage.setItem('transactionReference', result.transactionReference);
                $("#error").addClass("d-none");
                $("#success").removeClass("d-none");
                $("#success").text("transactionReference : " + result.transactionReference);
                closeSpinner();
            } else {
                mostrarMensaje(result);
            }
        },
        error: function (error) {
            console.log(error);
            closeSpinner();
            mostrarMensaje(error);
        }
    }).done((res) => {
        console.log(res)
    });
}

function GetSubscription() {
    loadSpinner();
    var form = $('#fGetSubscription')[0];
    var formData = new FormData(form);

        $.ajax({
            type: "POST",
            url: getUrlGetSubscription(),
            dataType: "json",
            data: formData,
            contentType: false,
            processData: false,
            success: function (result) {
                var obj = JSON.parse(result);
                if (obj.subscriptionId) {
                    console.log("OK");
                    $("#error").addClass("d-none");
                    $("#success").removeClass("d-none");
                    $("#success").text("Consulta exitosa");
                    closeSpinner();
                    mostrarInfosubscription(obj);
                } else {
                    mostrarMensaje(result);
                }
            },
            error: function (error) {
                console.log(error);
                closeSpinner();
                mostrarMensaje(error);
            }
        }); 
}

function mostrarInfosubscription(obj) {    
    $("#listaSubcripcion").append('<li>Name: '+obj.cardHolderName+'</li>');
    $("#listaSubcripcion").append('<li>Card Type: '+obj.cardType+'</li>');
    $("#listaSubcripcion").append('<li>Create: '+obj.created+'</li>');
    $("#listaSubcripcion").append('<li>Payment Brand: '+obj.paymentBrand+'</li>');
    $("#listaSubcripcion").append('<li>Periodicity: '+obj.periodicity+'</li>');
    $("#listaSubcripcion").append('<li>Plan Name: '+obj.planName+'</li>');
    $("#DvmostrarSubcripcion").removeClass("d-none");
}

function VoidTransaction() {
    loadSpinner();
    var form = $('#fVoidTransaction')[0];
    var formData = new FormData(form);

    $.ajax({
        type: "POST",
        url: getUrlVoidTransaction(),
        dataType: "json",
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            var objt = JSON.parse(result);
            if (objt.ticketNumber) {
                console.log("OK");
                $("#error").addClass("d-none");
                $("#success").removeClass("d-none");
                $("#success").text("Eliminacion exitosa");
                closeSpinner();                
            } else {
                mostrarMensaje2(result);               
            }
        },
        error: function (error) {
            console.log(error);
            closeSpinner();
            mostrarMensaje(error);
        }
    });  

}

function ListTransaction() {
    loadSpinner();
    var form = $('#flistTransaction')[0];
    var formData = new FormData(form);

    $.ajax({
        type: "POST",
        url: getUrlListTransaction(),
        dataType: "json",
        data: formData,
        contentType: false,
        processData: false,
        success: function (result) {
            var obj = JSON.parse(result);            
            if (obj.total < 0) {
                $("#error").addClass("d-none");
                $("#success").removeClass("d-none");
                $("#success").text("Consulta exitosa");
                closeSpinner();
                mostrarTablaListTrasaction(obj);
            } else {
                var objetoJSON = {
                    code: "0000",
                    message: "No se encontraron registros"
                };
                mostrarMensaje(objetoJSON);
            }
        },
        error: function (error) {
            console.log(error);
            closeSpinner();
            mostrarMensaje(error);
        }
    });

}

function mostrarTablaListTrasaction(obj) {
    for (let i = 0; i < obj.total; i++) {
        let clave = obj.data[i];
        var htmlTags = '<tr>' +
            '<td>' + clave.card_holder_name+'</td>' +
            '<td>' + clave.document_type + '</td>' + 
            '<td>' + clave.approved_transaction_amount + '</td>' + 
            '<td>' + clave.channel + '</td>' +
            '<td>' + clave.created + '</td>' +
            '</tr>';
        $('#tableListTransaction tbody').append(htmlTags);
    }
    $('#DvtableListTransaction').removeClass('d-none');
}

function habilitarSubcriptionToken() {
    limpiarMensajes();
    if ($("#subscriptionToken").hasClass("d-none")) {
        ocultarcreateToken();
        ocutarAutorizacionPagos();
        mostrarsubscriptionToken();
        ocultarObtenerSubscripcion();
        ocultarVoidTransaccion();
        ocultarcrearPagos();
        ocultarListTransaccion();
        ocultarGetAutorizacionPagos();
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
        ocultarGetAutorizacionPagos();
        ocultarObtenerSubscripcion();
        ocultarVoidTransaccion();
        ocultarListTransaccion();
    }

    if (localStorage.getItem('token') !== null) {
        $("#Token").val(localStorage.getItem('token'));
    } else {
        $("#Token").val("");
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
        ocultarGetAutorizacionPagos();
        ocultarVoidTransaccion();
        ocultarListTransaccion();
    }
    if (localStorage.getItem('token') !== null) {
        $("#tokenAP").val(localStorage.getItem('token'));
    } else {
        $("#tokenAP").val("");
    }
}

function habilitarGetAutorizacionPagos() {
    limpiarMensajes();
    if ($("#GetAutorizationPayment").hasClass("d-none")) {
        mostrarGetAutorizacionPagos();
        ocutarAutorizacionPagos();
        ocultarcreateToken();
        ocultarsubscriptionToken();
        ocultarcrearPagos();
        ocultarObtenerSubscripcion();
        ocultarVoidTransaccion();
        ocultarListTransaccion();
    }
}

function mostrarGetAutorizacionPagos() {
    $("#GetAutorizationPayment").removeClass("d-none");
    $("#Autorizacion2").removeClass("btn-secondary");
    $("#Autorizacion2").addClass("btn-primary");
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

function ocultarGetAutorizacionPagos() {
    $("#GetAutorizationPayment").addClass("d-none");
    $("#Autorizacion2").removeClass("btn-primary");
    $("#Autorizacion2").addClass("btn-secondary");
}

function limpiarMensajes() {
    $("#ObtenerTokenSubcription")[0].reset();
    $("#fcreateToken")[0].reset();
    $("#fAutorizationPayment")[0].reset();
    $("#fGetAutorizationPayment")[0].reset();
    $("#fGetSubscription")[0].reset();
    $("#fVoidTransaction")[0].reset();
    $("#error").addClass("d-none");
    $("#success").addClass("d-none");
    closeSpinner();
}

function habilitarcrearPagos() {
    limpiarMensajes();
    if ($("#createPayment").hasClass("d-none")) {
        mostrarCrearPagos()
        ocutarAutorizacionPagos();
        ocultarcreateToken();
        ocultarsubscriptionToken();
        ocultarGetAutorizacionPagos();
        ocultarObtenerSubscripcion();
        ocultarVoidTransaccion();
        ocultarListTransaccion();
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
function mostrarVoidTransaccion() {
    $("#voidTransaction").removeClass("d-none");
    $("#transaction").removeClass("btn-secondary");
    $("#transaction").addClass("btn-primary");
}
function ocultarVoidTransaccion() {
    $("#voidTransaction").addClass("d-none");
    $("#transaction").addClass("btn-secondary");
    $("#transaction").removeClass("btn-primary");
}

function mostrarListTransaccion() {
    $("#listTransaction").removeClass("d-none");
    $("#transaction2").removeClass("btn-secondary");
    $("#transaction2").addClass("btn-primary");
    $("#DvtableListTransaction").addClass("d-none");
}
function ocultarListTransaccion() {
    $("#listTransaction").addClass("d-none");
    $("#transaction2").addClass("btn-secondary");
    $("#transaction2").removeClass("btn-primary");    
}

function habilitarObtenerSubscripcion() {
    limpiarMensajes();
    if ($("#getsubscription").hasClass("d-none")) {        
        mostrarObtenerSubscripcion();
        ocultarcrearPagos();
        ocutarAutorizacionPagos();
        ocultarcreateToken();
        ocultarsubscriptionToken();
        ocultarGetAutorizacionPagos();
        ocultarVoidTransaccion();  
        ocultarListTransaccion();

        if (localStorage.getItem('subscriptionId') !== null) {
            $("#subscription").val(localStorage.getItem('subscriptionId'));
        } else {
            $("#subscription").val("");
        }
    }
}

function habilitarvoidTransaccion() {
    limpiarMensajes();
    if ($("#voidTransaction").hasClass("d-none")) {
        mostrarVoidTransaccion();        
        ocultarcrearPagos();
        ocutarAutorizacionPagos();
        ocultarcreateToken();
        ocultarsubscriptionToken();
        ocultarGetAutorizacionPagos();
        ocultarObtenerSubscripcion();
        ocultarListTransaccion();
    }
}

function habilitarListTransaccion() {
    limpiarMensajes();
    if ($("#listTransaction").hasClass("d-none")) {
        mostrarListTransaccion();
        ocultarVoidTransaccion();
        ocultarcrearPagos();
        ocutarAutorizacionPagos();
        ocultarcreateToken();
        ocultarsubscriptionToken();
        ocultarGetAutorizacionPagos();
        ocultarObtenerSubscripcion();
    }
}

function mostrarObtenerSubscripcion() {
    $("#getsubscription").removeClass("d-none");
    $("#subcription3").removeClass("btn-secondary");
    $("#subcription3").addClass("btn-primary");
    $("#DvmostrarSubcripcion").addClass("d-none");
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

function mostrarMensaje2(response) {
    var objt = JSON.parse(response);
    limpiarMensajes();
    if (!objt.code) {
        $("#error").addClass("d-none");
        $("#succes").removeClass("d-none");
        $("#succes").text(objt.message);
    } else {
        $("#succes").addClass("d-none");
        $("#error").removeClass("d-none");
        $("#error").text(objt.message);
    }
}

function loadSpinner() {
    $('#dvCargando').css('visibility', 'visible');
}

function closeSpinner() {
    $('#dvCargando').css('visibility', 'hidden');
}






