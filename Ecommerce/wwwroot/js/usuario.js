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
              
            
            {
                "data": {
                    id: "id", lockoutEnd: "lockoutEnd"
                    },
                "render": function (data) {
                    let hoy = new Date().getTime();
                    let bloqueo = new Date(data.lockoutEnd).getTime();
                    if (bloqueo > hoy) {
                        //Bloqueado
                        return `
                        <div class="text-center">
                            <a onclick=BloquearDesbloquear('${data.id}')
                            class="btn btn-danger text-white style="cursor:pointer; width:120px">
                                <i class="bi bi-unlock-fill"></i> Desbloquear
                            </a>
                        </div>

                        `;
                    } else {
                        //No Bloqueado
                        return `
                        <div class="text-center">
                            <a onclick=BloquearDesbloquear('${data.id}')
                            class="btn btn-success text-white style="cursor:pointer; width:120px">
                                <i class="bi bi-lock-fill"></i> Bloquear
                            </a>
                        </div>

                        `;
                    }

                }
            }
        ]

    });
}


function BloquearDesbloquear(id) {
    swal({
        title: "Esta seguro de bloquear/desbloquear el usuario?",
        text: "Se bloqueará/desbloqueará el usuario",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: '/Admin/Usuario/BloquearDesbloquear',
                data: JSON.stringify(id),
                contentType: "application/json",
                
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