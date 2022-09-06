function buscarOferta(codigo) {

    var parametros = {
        "codigo": codigo
    };

    $.ajax({
        data: parametros,
        url: '/Admin/GetOfferId',
        type: 'get',
        success: function (response) {
            $("#inputid").val(response.id_Offer);
            $("#inputdescripcion").val(response.description);
            $("#descuento").val(response.discountPrice);
            $("#fechainicio").val(response.startDate.split('T')[0]);
            $("#fechafinal").val(response.endDate.split('T')[0]);
            $("#idImage").val(response.image.idImage);
            $("#img2").attr("src", "/img/" + response.image.idImage + response.image.imageFormat);
        },
        error: function () {
            alert("Error");
            $("#codigo").val("Error de conexión con el servidor");
        }
    });
}

function buscarOferta2(codigo) {

    var parametros = {
        "codigo": codigo
    };

    $.ajax({
        data: parametros,
        url: '/Admin/GetOfferId',
        type: 'get',
        success: function (response) {
            console.log("hola" + response.image.idImage);
            console.log("hola" + response.image.imageFormat);
            $("#inputid2").val(response.id_Offer);
            $("#inputdescripcion2").val(response.description);
            $("#descuento2").val(response.discountPrice);
            $("#fechainicio2").val(response.startDate.split('T')[0]);
            $("#fechafinal2").val(response.endDate.split('T')[0]);
            $("#img1").attr("src", '/img/' + response.image.idImage + response.image.imageFormat);
        },
        error: function () {
            alert("Error");
            $("#codigo").val("Error de conexión con el servidor");
        }
    });
}

function buscarSeason(codigo) {

    var parametros = {
        "codigo": codigo
    };

    $.ajax({
        data: parametros,
        url: '/Admin/GetSeasonId',
        type: 'get',
        success: function (response) {
            var d = new Date(response.end_Date);
            $("#inputid").val(response.id);
            $("#inputtipo").val(response.season_Type);
            $("#inputstart").val(response.start_Date.split('T')[0]);
            $("#inputend").val(response.end_Date.split('T')[0]);
            $("#inputprice").val(response.price);
        },
        error: function () {
            alert("Error");
            $("#codigo").val("Error de conexión con el servidor");
        }
    });
}

function buscarSeason2(codigo) {

    var parametros = {
        "codigo": codigo
    };

    $.ajax({
        data: parametros,
        url: '/Admin/GetSeasonId',
        type: 'get',
        success: function (response) {
            $("#inputid2").val(response.id);
            $("#inputtipo2").val(response.season_Type);
            $("#inputstart2").val(response.start_Date.split('T')[0]);
            $("#inputend2").val(response.end_Date.split('T')[0]);
            $("#inputprice2").val(response.price);
        },
        error: function () {
            alert("Error");
            $("#codigo").val("Error de conexión con el servidor");
        }
    });
}

function deleteTemporada(){
    var opcion = confirm("¿Desea eliminar la temporada?");
    if (opcion == true) {
        mensaje = "Has clickado OK";
    } else {
        mensaje = "Has clickado Cancelar";
    }
}

function deleteOferta() {
    var opcion = confirm("¿Desea eliminar la oferta?");
    if (opcion == true) {
        mensaje = "Has clickado OK";
    } else {
        mensaje = "Has clickado Cancelar";
    }
}