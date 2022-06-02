var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/artists/GetAllArtists",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fName", "width": "25%" },
            { "data": "birthDate", "width": "25%" },
            { "data": "isActive", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/artists/Upsert/${data}" class='btn btn-success text-white'
                                    style='cursor:pointer;'> Delete </i></a>
                                    &nbsp;
                                <a onclick=Delete("/artists/Delete/${data}") class='btn btn-danger text-white'
                                    style='cursor:pointer;'> Edit </i></a>
                                <a onclick=Hide("/artist/Hide/${data}") class='btn btn-warning text-white'
                                    style='cursor:pointer;'> Hide </i></a>
                                </div>
                            `;
                }, "width": "30%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
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