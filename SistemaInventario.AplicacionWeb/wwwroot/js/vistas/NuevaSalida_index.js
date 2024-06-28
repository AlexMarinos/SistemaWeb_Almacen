$(document).ready(function () {
    fetch("/Personal/Lista")
        .then(response => response.ok ? response.json() : Promise.reject(response))
        .then(responseJson => {
            console.log(responseJson);
            if (responseJson.data.length > 0) {
                responseJson.data.forEach(item => {
                    console.log(item); // Verifica las propiedades de cada item
                    $("#cbopersonal").append(
                        $("<option>").val(item.idPersonal).text(`${item.apellidos}, ${item.servicioDescripcion}`)
                    );
                });
            }
        })
        .catch(error => console.error('Error fetching personal list:', error));

    $("#cboBuscarProducto").select2({
        ajax: {
            url: "/Salida/Obtenerarticulos",
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            delay: 250,
            data: function (params) {
                return {
                    busqueda: params.term
                };
            },
            processResults: function (data) {

                return {
                    results: data.map((item) => (
                        {
                            id:item.idArticulo,
                            text: item.descripcion,
                            categoria: item.categoriaDescripcion,
                            marca: item.marcaDescripcion,
                            unidadmedida: item.unidadMedidaDescripcion
                        }
                    ))   
                };
            }
        },
        language:"es",
        placeholder: 'Buscar Articulo',
        minimumInputLength: 1,
        templateResult: formatoResultados
    });



});

function formatoResultados(data) {
    if (data.loading) {
        return data.text;
    }

    var contenedor = $(
        "<table width='100%'>" +
        "<tr>" +
        "<td>" +
        "<p style='font-weight: bolder;margin:2px;'>" + data.text + "</p>" +
        "<p style='margin:2px;'>" + data.categoria + "</p>" +
        "<p style='margin:2px;'>" + data.marca + "</p>" +
        "</td>" +
        "</tr>" +
        "</table>"
    );

    return contenedor;
}

$(document).on("select2:open", function () {
    document.querySelector(".select2-search__field").focus();
})

let articuloparasalida=[]
$("#cboBuscarProducto").on("select2:select", function (e){
    const data = e.params.data;
    /*  console.log(data)*/

    let articulo_encontrado = articuloparasalida.filter(p => p.idArticulo == data.id)
    if (articulo_encontrado.length > 0) {
        $("#cboBuscarProducto").val("").trigger("change")
        toastr.warning("", "El articulo ya fue agregado")
        return false
    }
    swal({
        title: data.text,
        text: data.marca,
        type: "warning",
        type:"input",
        showCancelButton:true,
        closeOnConfirm: false,
        inputPlaceholder:"Ingresar Cantidad"
    },
        function (valor) {

            if (valor === false) return false;

            if (valor === "") {
                toastr.warning("", "Se necesita ingresa la cantidad")
                return false;
            }
            if (isNaN(parseInt(valor))) {
                toastr.warning("", "El valor debe ser numerico ")
                return false;
            }
            let articulo = {
                idArticulo: data.id,
                descripcionArticulo: data.text,
                marcaArticulo: data.marca,
                categoriaArticulo: data.categoria,
                unidadmedidaArticulo: data.unidadmedida,
                cantidad:parseInt(valor)
            }
            articuloparasalida.push(articulo)
            mostrararticulos();
            $("#cboBuscarProducto").val("").trigger("change")
            swal.close()
            }
        );

})


function mostrararticulos(){
    $('#tbProducto tbody').html("")
    articuloparasalida.forEach((item) => {
        $("#tbProducto tbody").append(

            $("<tr>").append(
                $("<td>").append(
                    $("<button>").addClass("btn btn-danger btn-eliminar btn-sm").append(
                        $("<i>").addClass("fas fa-trash-alt")
                    ).data("idArticulo",item.idArticulo)
                ),
                $("<td>").text(item.descripcionArticulo),
                $("<td>").text(item.marcaArticulo),
                $("<td>").text(item.unidadmedidaArticulo),
                $("<td>").text(item.cantidad),
            )
        )
    })
}

$(document).on("click","button.btn-eliminar", function () {
    const _idarticulo = $(this).data("idArticulo")
    articuloparasalida = articuloparasalida.filter(p => p.idArticulo != _idarticulo);
    mostrararticulos();
})