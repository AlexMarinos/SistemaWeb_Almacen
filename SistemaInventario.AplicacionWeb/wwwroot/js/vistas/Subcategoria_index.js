const MODELO_BASE = {
    idSubCategoria: 0,
    descripcion: "",
    idcategoria: 0,
    esActivo: 1

}

let tablaData;

$(document).ready(function () {
    fetch("/Categoria/Lista")
        .then(response => response.ok ? response.json() : Promise.reject(response))
        .then(responseJson => {
            console.log(responseJson);
            if (responseJson.data.length > 0) {
                responseJson.data.forEach(item => {
                    $("#cbocategoria").append(
                        $("<option>").val(item.idCategoria).text(item.descripcion)
                    );
                });
            }
        })
        .catch(error => console.error('Error fetching categories:', error));

    tablaData = $('#tbdata').DataTable({
        responsive: true,
        "ajax": {
            "url": '/Subcategoria/lista',
            "type": "GET",
            "datatype": "json",
            "dataSrc": "data"
        },
        "columns": [
            { "data": "idSubCategoria", "visible": false, "searchable": false },
            { "data": "idcategoria", "visible": false, "searchable": false },
            { "data": "categoriaDescripcion", "title": "Categoria" },
            { "data": "descripcion", "title": "Subcategoria" },

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
    $("#txtId").val(modelo.idSubCategoria);
    $("#txtDescripcion").val(modelo.descripcion);
    $("#cboEstado").val(modelo.esActivo);
    $("#cbocategoria").val(modelo.idcategoria == 0 ? $('#cbocategoria option:first').val() : modelo.idcategoria);


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
    modelo["idSubCategoria"] = parseInt($('#txtId').val());
    modelo["descripcion"] = $('#txtDescripcion').val();
    modelo["idcategoria"] = parseInt($('#cbocategoria').val());
    modelo["esActivo"] = parseInt($('#cboEstado').val());


    // Verificar que no hay valores nulos
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

    if (modelo.idSubCategoria == 0) {
        fetch("/Subcategoria/Crear", {
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
        fetch("/Subcategoria/Editar", {
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
            text: `Eliminar la subcategoria ${data.descripcion}`,
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

                    fetch(`/Subcategoria/Eliminar?idsubcategoria=${data.idSubCategoria}`, {
                        method: "DELETE"
                    })
                        .then(response => {
                            $(".showSweetAlert").LoadingOverlay("hide");
                            return response.ok ? response.json() : Promise.reject(response);
                        })
                        .then(responseJson => {
                            if (responseJson.estado) {
                                tablaData.row(fila).remove().draw();
                                swal("Listo!", "La subcategoria  fue eliminado", "success");
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
