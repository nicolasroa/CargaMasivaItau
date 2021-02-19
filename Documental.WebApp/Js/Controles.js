

jQuery(function ($) {
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '&#x3c;Ant',
        nextText: 'Sig&#x3e;',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
                'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
                'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Mi&eacute;rcoles', 'Jueves', 'Viernes', 'S&aacute;bado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mi&eacute;', 'Juv', 'Vie', 'S&aacute;b'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'S&aacute;'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);
});



/********************************************************************************************    
function que permite mostrar y ocultar las opciones de usuario
********************************************************************************************/

function MostrarOpcionesUsuario() {
    if ($('.DatosOpciones').attr('style') == 'display: none') {
        $('.DatosOpciones').attr('style', 'display: block');
    }
    else {
        $('.DatosOpciones').attr('style', 'display: none');
    }
}

/********************************************************************************************    
function que permite dar configuracion basica a todos los popus
********************************************************************************************/

function ConfiguracionPopups() {

    elementos = $('div[title=VentanaModal]')


    elementos.each(function (index) {

        var z_Index = index + 999;

        $(this).dialog({
            autoOpen: false,
            modal: true,
            resizable: false,
            show: 'clip',
            hide: 'clip',
            zIndex: z_Index,
            overlay: {
                opacity: 0.5,
                background: "black"
            },
            open: function (event, ui) {
                $(this).parent().find('.ui-dialog-titlebar').append('<div class="ui-dialog-titlebar-icon"></div>');
            }

        });
    });

}
/********************************************************************************************
function que permite dar configuracion personalizada a cada popup
********************************************************************************************/
function ConfiguracionPopup(elementoDiv, elementoFrame, url, ancho, alto, titulo) {

    elementoFrame.attr('src', url);
    elementoDiv.dialog('option', 'width', ancho);
    elementoDiv.dialog('option', 'height', alto);
    elementoDiv.dialog('option', 'title', titulo);
    elementoDiv.dialog('open');

}

/********************************************************************************************
verifica que hay un popup disponible y lo abre si hay uno disponible
********************************************************************************************/

function AsignaPopupDisponible(lstFrames, url, ancho, alto, titulo) {

    lstFrames.each(function (index) {

        var SelectorPopup = '#' + $(this).attr('NombreDivPadre');
        var elemento = $(SelectorPopup);

        if (elemento.length > 0) {
            ConfiguracionPopups();
            ConfiguracionPopup(elemento, $(this), url, ancho, alto, titulo);

            return false;
        }
        else {

            if (elemento.dialog('isOpen') == false) {
                elemento.dialog('open');
                elemento.dialog('isOpen') = true;
                $(this).attr('src', url);

                return false;
            }

        }

    });
}
/********************************************************************************************
funcion que permite determinar la ventana donde estan los div de pupus para poder abrirlos
asi tambien cuando encuentra la posición, abre el que encuentra disponible
********************************************************************************************/

function AbrirPopupMain(url, ancho, alto, titulo) {

    var lstFrames = $('.cssFramePopup');
    var ventana;
    var encuentraElmentosPopup = false;

    while (encuentraElmentosPopup == false) {

        if (lstFrames.length > 0) {
            AsignaPopupDisponible(lstFrames, url, ancho, alto, titulo);
            encuentraElmentosPopup = true;
            break;
        }

        ventana = window.parent;

        if (ventana.$('.cssFramePopup').length > 0) {

            Popups = ventana.$('.cssFramePopup');
            encuentraElmentosPopup = true;
        }
        else
            ventana = ventana.window.parent;



    }

}

function AbrirPopup(url) {

    var ancho = 500;
    var alto = 500;
    var titulo = '';

    $(document).ready(function () {
        AbrirPopupMain(url, ancho, alto, titulo);
    });
    return false;
}

function AbrirPopup(url, ancho, alto) {

    var titulo = '';
    $(document).ready(function () {
        AbrirPopupMain(url, ancho, alto, titulo);
    });
    return false;
}

function AbrirPopup(url, ancho, alto, titulo) {
    $(document).ready(function () {
        AbrirPopupMain(url, ancho, alto, titulo);
    });
    return false;
}
function CerrarPopup(ConEvento) {

    var ventanaPopups = window.parent;
    var ElementosPopup = ventanaPopups.$('.cssFramePopup').parent();
    if (navigator.appName != "Microsoft Internet Explorer") {
        if (!ConEvento)
            return false;
        else {
            window.parent.$('.CssCerrarPopup').click();
        }
    }
    if (ElementosPopup.length == 0) {
        while (true) {
            ventanaPopups = ventanaPopups.window.parent;
            ElementosPopup = ventanaPopups.$('.cssFramePopup').parent()
            if (ElementosPopup.length > 0) {
                break;
            }
        }
    }

    for (var i = ElementosPopup.length - 1; i >= 0; i--) {
        var elmentoDivPopup = $(ElementosPopup[i]);
        if (window.parent.$(elmentoDivPopup).dialog('isOpen')) {
            alert(ElementosPopup);
            window.parent.$(elmentoDivPopup).dialog('close');
            break;
        }
    }
    if (!ConEvento)
        return false;
    else {
        window.parent.$('.CssCerrarPopup').click();
    }

}

function ModalURL(url, ancho, alto, titulo) {

    var $dialog = $('#divModalPopup1')
               .html('<iframe style="border: 0px; " src="' + url + '" width="100%" height="100%"></iframe>')
               .dialog({
                   autoOpen: false,
                   modal: true,
                   height: alto,
                   width: ancho,
                   title: titulo,
                   show: {
                       effect: "clip"
                   },
                   hide: {
                       effect: "clip"

                   }
               });
    $dialog.dialog('open');

    //bootbox.dialog({
    //    title: titulo,
    //    message: '<iframe style="border: 0px; " src="' + url + '" width="100%" height="100%"></iframe>'
    //});


}

function ModalDiv(IdDiv, titulo) {

    var $dialog = $('#' + IdDiv + '').dialog({
        modal: true,
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 500
        },
        hide: {
            effect: "explode",
            duration: 500
        },
        title: titulo

    });

    $dialog.dialog('open');

}
function CerrarModal(Id) {

    var Dialog = $('#' + Id + '');
    Dialog.dialog('close');



}

function CerrarVentana() {

    window.parent.$('.CssCerrarPopup').click();
    window.parent.$('#divModalPopup1').dialog('close');

    return false;

}

function CerrarVentanaMenu() {

    window.parent.$('.CssCargarMenu').click();
    window.parent.$('#divModalPopup1').dialog('close');
    return false;

}

function AbrirMensaje(texto) {

    var ancho = 350;
    var alto = 250;
    var titulo = 'prueba ';
    CargarMensaje(texto, ancho, alto, titulo);
    return false;
}

function AbrirMensaje2(texto, titulo) {

    var ancho = 350;
    var alto = 250;

    CargarMensaje(texto, ancho, alto, titulo);
    return false;
}

function AbrirMensaje(texto, ancho, alto) {

    var titulo = ' ';
    CargarMensaje(texto, ancho, alto, titulo);
    return false;
}

function AbrirMensaje(texto, ancho, alto, titulo) {

    CargarMensaje(texto, ancho, alto, titulo);
    return false;
}

function CargarMensaje(texto, ancho, alto, titulo) {
    $('#lbltextoMensaje').html(texto);
    $('#divMensajes').dialog({
        autoOpen: false,
        modal: true,
        height: alto,
        width: ancho,
        title: titulo,
        resizable: false,
        show: 'clip',
        hide: 'clip',
        overlay: {
            opacity: 0.5
        },
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
            }
        }
    });
    $('#divMensajes').dialog('open');
}
function CerrarMensaje() {
    $('#divMensajes').dialog('close');
    return false;
}

function MostrarPdfModal(NombreArchivo, ancho, alto, titulo) {


    try {




        var pObj = new PDFObject({

            url: "../Repositorio/Pdf/" + NombreArchivo,
            id: "myPDF",
            pdfOpenParams: {
                navpanes: 0,
                toolbar: 0,
                statusbar: 0,
                view: "FitV"
            }
        });
        var msg;
        document.getElementById("DivPdf").className = "pdf";
        var htmlObj = pObj.embed("DivPdf");

        $('#DivPdf').dialog({
            modal: true,
            autoOpen: false,
            width: ancho,
            height: alto,
            title: titulo,
            show: 'clip',
            hide: 'clip',
            resizable: false
        });
        $('#DivPdf').dialog('open');
        return false;

    } catch (e) {
        alert(e.Message);
        return false;
    }
}



function MostrarPdfDiv(NombreArchivo, NombreDiv) {
    var pObj = new PDFObject({

        url: "../Repositorio/Pdf/" + NombreArchivo,
        id: "myPDF",
        pdfOpenParams: {
            navpanes: 0,
            toolbar: 0,
            statusbar: 0,
            view: "FitV"
        }
    });
    var msg;
    document.getElementById(NombreDiv).className = "pdf";
    var htmlObj = pObj.embed(NombreDiv);
    return false;
}




function MostrarMensajeError(mensaje) {
    MensajeError(mensaje);
}
function MostrarMensajeInfo(mensaje) {
    MensajeInfo(mensaje);
}
function MostrarMensajeExito(mensaje) {

    MensajeExito(mensaje);
}
function MostrarMensajeAlerta(mensaje) {
    MensajeAlerta(mensaje);
}
function MostrarMensajeConfirmacion(mensaje) {
    return MensajeConfirmacion(mensaje);
}



function MensajeInfo(texto) {
    $('#lbltextoMensaje').html("<p class='messager-info'>" + texto + "</p>");
    $('#divMensajes').dialog({
        autoOpen: false,
        modal: true,
        title: 'Información',
        resizable: false,
        show: 'clip',
        hide: 'clip',
        width: 500,
        overlay: {
            opacity: 0.5
        },
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
            }
        }
    });
    $('#divMensajes').dialog('open');
}
function MensajeError(texto) {
    $('#lbltextoMensaje').html("<p class='messager-error'>" + texto + "</p>");
    $('#divMensajes').dialog({
        autoOpen: false,
        modal: true,
        title: 'Error',
        resizable: false,
        show: 'clip',
        hide: 'clip',
        width: 500,
        overlay: {
            opacity: 0.5
        },
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
            }
        }
    });
    $('#divMensajes').dialog('open');
}

function MensajeExito(texto) {
    $('#lbltextoMensaje').html("<p class='messager-Exito'>" + texto + "</p>");
    $('#divMensajes').dialog({
        autoOpen: false,
        modal: true,
        title: 'Proceso Correcto',
        resizable: false,
        show: 'clip',
        hide: 'clip',
        width: 500,
        overlay: {
            opacity: 0.5
        },
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
            }
        }
    });
    $('#divMensajes').dialog('open');
}
function MensajeAlerta(texto) {
    $('#lbltextoMensaje').html("<p class='messager-warning'>" + texto + "</p>");
    $('#divMensajes').dialog({
        autoOpen: false,
        modal: true,
        title: 'Alerta',
        resizable: false,
        show: 'clip',
        hide: 'clip',
        width: 500,
        overlay: {
            opacity: 0.5
        },
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
            }
        }

    });
    $('#divMensajes').dialog('open');
}

function MensajeConfirmacion(texto, obj) {
    $('#lbltextoConfirmacion').html("<p class='messager-confirm'>" + texto + "</p>");
    $('#divConfirmacion').dialog({
        autoOpen: false,
        modal: true,
        title: 'Alerta',
        resizable: false,
        show: 'clip',
        hide: 'clip',
        width: 500,
        overlay: {
            opacity: 0.5
        },
        buttons: {
            "Si": function () {
                obj.click();
                $(this).dialog("close");
            },
            "No": function () { $(this).dialog("close"); }
        }
    });
    $('#divConfirmacion').dialog('open');
}

function ConfirmacionConEspera(Confirmacion, texto, obj) {
    $('#lbltextoConfirmacion').html("<p class='messager-confirm'>" + texto + "</p>");
    $('#divConfirmacion').dialog({
        autoOpen: false,
        modal: true,
        title: 'Alerta',
        resizable: false,
        show: 'clip',
        hide: 'clip',
        width: 500,
        overlay: {
            opacity: 0.5
        },
        buttons: {
            "Si": function () {
                obj.click();
                $(this).dialog("close");
                $.blockUI({
                    message: "<p class='tdInfoData'>" + Confirmacion + "</p>",
                    css: {
                        border: 'none',
                        padding: '15px',
                        backgroundColor: '#fff',
                        '-webkit-border-radius': '10px',
                        '-moz-border-radius': '10px',
                        color: '#fff'
                    }
                });


            },
            "No": function () { $(this).dialog("close"); }
        }
    });
    $('#divConfirmacion').dialog('open');
}





function tip(obj, txt, maxW) {
    maxW = maxW || 'auto';
    Tipped.create(obj, txt,
                {
                    skin: 'blue',
                    closeButton: true,
                    hook: 'topmiddle',
                    maxWidth: maxW,
                    shadow: {
                        blur: 4,
                        offset: { x: 3, y: 3 },
                        opacity: .2
                    },
                    showOn: ['', ''],
                    hideOn: ['blur', 'mouseleave']
                });



}


function MensajeEnControl(idcontrol, mensaje) {

    tip("#" + idcontrol + "", mensaje, 350);
    Tipped.show("#" + idcontrol + "");

}


//NOTIFICACIONES

function MostrarNotificacion(mensaje) {


    alertify.prompt("This is a prompt dialog", function (e, str) {
        if (e) {
            alertify.init("You've clicked OK and typed: " + str);
        } else {
            alertify.log("You've clicked Cancel");
        }
    }, "Default Value");
    return false;

}



function AsignarFormatoFecha(idControl) {


    $("#" + idControl).attr('onblur', 'esFechaValida(this);');
    $("#" + idControl).datepicker({
        dateFormat: "dd/mm/yy",
        defaultDate: "+1d",
        changeMonth: true,
        numberOfMonths: 3,
        maxDate: new Date()
    });




}
function LimpiarFormatoFecha(idControl) {
    $("#" + idControl).removeAttr("onblur");
    $("#" + idControl).datepicker("destroy");
}








