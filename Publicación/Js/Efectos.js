function Block() {
    $.blockUI({
        message: $('#DivBlock')
    });
}

function UnBlock() {
    setTimeout($.unblockUI, 0);
}


function Espera(obj, mensaje) {
    obj.click();
    $.blockUI({
        message: "<p class='tdInfoData'>" + mensaje + "</p>",
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#fff',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            color: '#fff'
        }
    });
}




$(function () {
    $("#accordion").accordion();
});
function MostrarBusqueda() {
    getElement('lnkBusqueda').click();
    getElement('hFormulario').style.display = "none";
    getElement('hBusqueda').style.display = "block";
}
function MostrarFormulario() {

    getElement('lnkFormulario').click();
    getElement('hFormulario').style.display = "block";
    getElement('hBusqueda').style.display = "none";
}
function getElement(id) {

    if (document.getElementById) {
        return document.getElementById(id);
    }

    else if (document.all) {
        return window.document.all[id];
    }

    else if (document.layers) {
        return window.document.layers[id];
    }
}