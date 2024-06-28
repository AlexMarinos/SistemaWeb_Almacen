const MODELO_BASE = {
    idArticulo: 0,
    codigoArticulo: "",
    serie: "",
    descripcion: "",
    stock: 0,
    idMarca: 0,
    idSubcategoria: 0,
    idUnidadMedida: 0,
    esActivo: 1
};

let tablaData;

$(document).ready(function () {

    fetch("/Subcategoria/Lista")
        .then(response => response.ok ? response.json() : Promise.reject(response))
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach(item => {
                    $("#cbosub").append(
                        $("<option>").val(item.idSubCategoria).text(item.descripcion)
                    );
                });
            }
        })
        .catch(error => console.error('Error fetching subcategories:', error));

    fetch("/Unidadmedida/Lista")
        .then(response => response.ok ? response.json() : Promise.reject(response))
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach(item => {
                    $("#cbounidadmedida").append(
                        $("<option>").val(item.idUnidadMedida).text(item.descripcion)
                    );
                });
            }
        })
        .catch(error => console.error('Error fetching units:', error));

    fetch("/Marca/Lista")
        .then(response => response.ok ? response.json() : Promise.reject(response))
        .then(responseJson => {
            if (responseJson.data.length > 0) {
                responseJson.data.forEach(item => {
                    $("#cbomarca").append(
                        $("<option>").val(item.idMarca).text(item.descripcion)
                    );
                });
            }
        })
        .catch(error => console.error('Error fetching brands:', error));

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Articulo/Lista',
            "type": "GET",
            "datatype": "json",
            "dataSrc": "data"
        },
        "columns": [
            { "data": "idArticulo", "visible": false, "searchable": false },
            { "data": "codigoArticulo", "title": "Código" },
            { "data": "serie", "title": "Serie" },
            { "data": "descripcion", "title": "Artículo" },
            { "data": "idSubcategoria", "visible": false, "searchable": false },
            { "data": "subcategoriaDescripcion", "title": "Subcategoría" },
            { "data": "idMarca", "visible": false, "searchable": false },
            { "data": "marcaDescripcion", "title": "Marca" },
            { "data": "stock", "title": "Stock" },
            { "data": "idUnidadMedida", "visible": false, "searchable": false },
            { "data": "unidadMedidaDescripcion", "title": "Unidad Medida" },
            {
                "data": "esActivo", "title": "Estado",
                "render": function (data) {
                    return data == 1
                        ? '<span class="badge badge-info">Activo</span>'
                        : '<span class="badge badge-danger">No Activo</span>';
                }
            },
            {
                "data": null,
                "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2" title="Editar"><i class="fas fa-pencil-alt"></i></button>' +
                    '<button class="btn btn-danger btn-eliminar btn-sm" title="Eliminar"><i class="fas fa-trash-alt"></i></button>',
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }
        ],
        order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Articulo',
                exportOptions: {
                    columns: [1, 2, 3, 4, 6, 8, 10, 11]
                }
            },
            'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});


function mostrarModal(modelo = MODELO_BASE) {
  
    $("#txtId").val(modelo.idArticulo);
    $("#txtcodigo").val(modelo.codigoArticulo);
    $("#txtserie").val(modelo.serie);
    $("#txtDescripcion").val(modelo.descripcion);
    $("#cbosub").val(modelo.idSubcategoria == 0 ? $('#cbosub option:first').val() : modelo.idSubcategoria);
    $("#cbomarca").val(modelo.idMarca == 0 ? $('#cbomarca option:first').val() : modelo.idMarca);
    $("#cbounidadmedida").val(modelo.idUnidadMedida == 0 ? $('#cbounidadmedida option:first').val() : modelo.idUnidadMedida);
    $("#txtStock").val(modelo.stock);
    $("#cboEstado").val(modelo.esActivo);
    $("#modalData").modal("show");
}


$("#btnNuevo").click(function () {
    mostrarModal();
});

$("#btnGuardar").click(function () {
    const inputs = $("input.input-validar").serializeArray();
    const inputs_sin_valor = inputs.filter((item) => item.value.trim() === "");

    if (inputs_sin_valor.length > 0) {
        const mensaje = `Debe completar el campo: "${inputs_sin_valor[0].name}"`;
        toastr.warning("", mensaje);
        $(`input[name="${inputs_sin_valor[0].name}"]`).focus();
        return;
    }

    const modelo = structuredClone(MODELO_BASE);
    modelo["idArticulo"] = parseInt($('#txtId').val());
    modelo["codigoArticulo"] = $('#txtcodigo').val();
    modelo["serie"] = $('#txtserie').val();
    modelo["descripcion"] = $('#txtDescripcion').val();
    modelo["idUnidadMedida"] = parseInt($('#cbounidadmedida').val());
    modelo["idSubcategoria"] = parseInt($('#cbosub').val());
    modelo["idMarca"] = parseInt($('#cbomarca').val());
    modelo["stock"] = parseInt($('#txtStock').val());
    modelo["esActivo"] = parseInt($('#cboEstado').val());

    console.log("Datos antes de enviar:", modelo);
    for (const [key, value] of Object.entries(modelo)) {
        if (value === null || value === undefined) {
            console.error(`El valor de ${key} es nulo o indefinido:`, value);
            return;
        }
    }

    const jsonData = JSON.stringify(modelo);
    console.log("Datos a enviar:", jsonData);

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idArticulo == 0) {
        fetch("/Articulo/Crear", {
            method: "POST",
            headers: { "Content-Type": "application/json;charset=utf-8" },
            body: jsonData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado) {
                    tablaData.row.add(responseJson.objeto).draw(false);
                    $("#modalData").modal("hide");
                    swal("Listo!", "El Articulo fue creado", "success");
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error");
                }
            })
            .catch(error => console.error('Error creando el artículo:', error));
    } else {
        fetch("/Articulo/Editar", {
            method: "PUT",
            headers: { "Content-Type": "application/json; charset=utf-8" },
            body: jsonData
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => {
                if (responseJson.estado) {
                    tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);
                    filaSeleccionada = null;
                    $("#modalData").modal("hide");
                    swal("Listo!", "El Articulo fue modificado", "success");
                } else {
                    swal("Lo sentimos", responseJson.mensaje, "error");
                }
            })
            .catch(error => console.error('Error editando el artículo:', error));
    }
});

let filaSeleccionada;

$("#tbdata tbody").on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    const data = tablaData.row(filaSeleccionada).data();

    if (data) {
        mostrarModal(data);
    } else {
        console.error('Error: Data is null');
    }
});

$("#tbdata tbody").on("click", ".btn-eliminar", function () {
    let fila;
    if ($(this).closest("tr").hasClass("child")) {
        fila = $(this).closest("tr").prev();
    } else {
        fila = $(this).closest("tr");
    }

    const data = tablaData.row(fila).data();

    if (data) {
        swal({
            title: "¿Está seguro?",
            text: `Eliminar al Articulo ${data.descripcion}`,
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "No, cancelar",
            closeOnConfirm: false,
            closeOnCancel: true
        },
            function (respuesta) {
                if (respuesta) {
                    $(".showSweetAlert").LoadingOverlay("show");

                    fetch(`/Articulo/Eliminar?idarticulo=${data.idArticulo}`, {
                        method: "DELETE"
                    })
                        .then(response => {
                            $(".showSweetAlert").LoadingOverlay("hide");
                            return response.ok ? response.json() : Promise.reject(response);
                        })
                        .then(responseJson => {
                            if (responseJson.estado) {
                                tablaData.row(fila).remove().draw();
                                swal("Listo!", "El Articulo fue eliminado", "success");
                            } else {
                                swal("Lo sentimos", responseJson.mensaje, "error");
                            }
                        })
                        .catch(error => console.error('Error eliminando el artículo:', error));
                }
            });
    } else {
        console.error('Error: Data is null');
    }
});
