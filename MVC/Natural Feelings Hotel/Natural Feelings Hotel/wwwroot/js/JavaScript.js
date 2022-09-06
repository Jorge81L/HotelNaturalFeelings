function cargarTemporadas() {
    var codigo = $("#buscarCaso").val();
    var parametros = {
        "codigo": codigo
    };
    var tbody = $("#cuerpoTabla");
    $.ajax({
        data: parametros,
        url: '/Habitation/readRate',
        type: 'get',
        beforeSend: function () {
            tbody.empty();
        },
        success: function (response) {
            if (response.length === 0) {

            } else {
                for (var i = 0; i < response.length; i++) {
                    tbody.append("<tr>" +
                        '<td id="' + response[i].idRate + '">' + response[i].idOficina + "</td>" +
                        "<td>" + response[i].name + "</td>" +
                        "<td>" + '<button type="button" class="btn btn-danger" onclick="eliminarOficina(this);">X</button>' + "</td>" +
                        + "</tr>");
                }
            }
        },
        error: function () {
            $("#cuerpoTabla").val("Error de conexión con el servidor");
        }
    });
}