let dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#dataTable').DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Ningun Registro",
            "info": "Mostrar Pagina _PAGE_ de _PAGES_",
            "infoEmpty": "no hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "ajax": {
            "url": "/Admin/Usuario/ObtenerTodos"
        },
        "columns": [
            { "data": "email"  },
            { "data": "nombres" },
            { "data": "apellidos" },
            { "data": "phoneNumber" },
            { "data": "rol" },
              
            
            //{
            //    "data": "id",
            //    "render": function (data) {
            //        return `
            //            <div class="text-center">
            //                <a href="/Admin/Categoria/Upsert/${data}"
            //                class="btn btn-dark text-white" style="cursor:pointer"> 
            //                    <i class="bi bi-pencil-square"></i>
            //                </a>
            //                <a onclick=Delete("/Admin/Categoria/Delete/${data}")
            //                class="btn btn-danger text-white" style="cursor:pointer">
            //                    <i class="bi bi-trash3"></i>
            //                </a>
            //            </div>
            //        `;
            //    }, "width": "20%"
            //}
        ]

    });
}


function Delete(url) {
    swal({
        title: "Esta seguro de eliminar la categoria?",
        text: "este registro no se podrá recuperar",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message)
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            
            });
        }
    });
}