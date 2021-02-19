function SoloIP(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
        return false;

    return true;
}
function ValidarIP(obj) {
    //Creamos un objeto 

    valueForm = obj.value;

    // Patron para la ip
    var patronIp = new RegExp("^([0-9]{1,3}).([0-9]{1,3}).([0-9]{1,3}).([0-9]{1,3})$");
    //window.alert(valueForm.search(patronIp));
    // Si la ip consta de 4 pares de números de máximo 3 dígitos
    if (valueForm.search(patronIp) == 0) {
        // Validamos si los números no son superiores al valor 255
        valores = valueForm.split(".");
        if (valores[0] <= 255 && valores[1] <= 255 && valores[2] <= 255 && valores[3] <= 255) {
            //Ip correcta
            obj.style.color = "#000";
            return true;
        }
    }
    //Ip incorrecta



    MostrarMensajeAlerta('La Ip Ingresada ' + obj.value + ' es Incorrecta');
    obj.value = "";
    obj.focus();
    return false;
}



function ValidNum(e) {

    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
    return ((tecla > 47 && tecla < 58) || tecla == 44);
}

function validaRut(Obj) {
    var rut = Obj.value.toString();
    var pos = rut.indexOf('-');
    var verificador = '';
    var cuerpo = '';
    if (pos == -1) {
        verificador = rut.substr(rut.length - 1, 1);
        cuerpo = rut.substr(0, rut.length - 1);
        rut = cuerpo + "-" + verificador;
    }

    var rexp = new RegExp(/^([0-9])+\-([k0-9])+$/);
    if (rut == "") {
        return true;
    }
    if (rut.match(rexp)) {
        try {
            var _RUT = rut.split("-");

            var elRut = _RUT[0];

            var factor = 2;
            var suma = 0;
            var dv;
            for (i = (elRut.length - 1); i >= 0; i--) {

                factor = factor > 7 ? 2 : factor;
                suma += parseInt(elRut.charAt(i)) * parseInt(factor++);

            }
            dv = 11 - (suma % 11);

            if (dv == 11) {
                dv = 0;
            }
            else if (dv == 10) {
                dv = "k";
            }

            if (dv == _RUT[1]) {
                Obj.value = rut;
                return true;
            } else {

                MostrarMensajeAlerta('El rut es incorrecto');
                Obj.value = "";
                Obj.focus();
                return false;
            }
        } catch (e) {
            MostrarMensajeAlerta(e.InnerException + 'Linea' + e.Line);
        }
    } else {
        Obj.value = "";
        Obj.focus();
        MostrarMensajeAlerta('Formato incorrecto. El formato correcto es 12345678-k');

        return false;
    }
}

function SoloRut(evt) {

    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45 && charCode != 107)
        return false;

    return true;
}



function SoloNumeros(evt, texto, decimales, obj) {
    obj.value = obj.value.replace(".", ",");

    var _texto = texto.split(",");
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 44 && charCode != 46)
        return false;
    if (_texto.length > 1 && charCode == 44)
        return false;

    if (_texto[1] != null) {
        if (_texto[1].length > decimales - 1 && charCode != 8 && charCode != 127)
            return false;
    }


    return true;
}

function SoloFolios(evt, texto) {
    var _texto = texto.split("-");
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 45)
        return false;

    return true;
}



function ValidaSoloNumeros() {
    if ((event.keyCode < 48) || (event.keyCode > 57))
        event.returnValue = false;
}



function Formato_Mes_Anho(oText) {
    try {
        var texto, dia, mes, anho;
        texto = oText;
        if (texto != '') {
            while (texto.indexOf('/') != -1) {
                texto = texto.replace("/", "");
            }
            while (texto.indexOf('-') != -1) {
                texto = texto.replace("-", "");
            }
            dia = texto.substring(0, 2);
            mes = texto.substring(2, 4);
            anho = texto.substring(4, 8);
            texto = dia + "/" + mes + "/" + anho;
        }
        return texto;
    } catch (e) {
        alert(e.Message);
    }


}

function esFechaValida(fecha) {

    fecha.value = Formato_Mes_Anho(fecha.value);

    if (fecha != undefined && fecha.value != "") {
        if (!/^\d{2}\/\d{2}\/\d{4}$/.test(fecha.value)) {
            MostrarMensajeAlerta("formato de fecha no válido (dd-mm-aaaa)");
            fecha.value = "";
            return false;
        }
        var dia = parseInt(fecha.value.substring(0, 2), 10);
        var mes = parseInt(fecha.value.substring(3, 5), 10);
        var anio = parseInt(fecha.value.substring(6), 10);

        switch (mes) {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                numDias = 31;
                break;
            case 4: case 6: case 9: case 11:
                numDias = 30;
                break;
            case 2:
                if (comprobarSiBisisesto(anio)) { numDias = 29 } else { numDias = 28 };
                break;
            default:
                MostrarMensajeAlerta("Fecha introducida errónea");
                fecha.value = "";
                return false;
        }

        if (dia > numDias || dia == 0) {
            MostrarMensajeAlerta("Fecha introducida errónea");
            fecha.value = "";
            return false;
        }
        return true;
    }
}

function comprobarSiBisisesto(anio) {
    if ((anio % 100 != 0) && ((anio % 4 == 0) || (anio % 400 == 0))) {
        return true;
    }
    else {
        return false;
    }
}

function validarEmail(obj) {
    expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!expr.test(obj.value.toString())) {
        MostrarMensajeAlerta("La dirección de correo " + obj.value + " es incorrecta.");
        obj.value = "";
        return false;
    }
}





