let dataTable;
$(document).ready(() => {
	loadDataTable()
})

function loadDataTable() {
	//DataTable class gets added from cdn reference
	//First calls the api route, gets json format, then builds table
	dataTable = $("#DT_load").DataTable({
		"ajax": {
			"url": "/api/Book",
			"datatype": "json"
		},
		"columns": [
			//follow lowerCase from Book Class ie) First Name = firstName
			{ "data": "name", "width": "20%" },
			{ "data": "author", "width": "20%" },
			{ "data": "isbn", "width": "20%" },
			{
				"data": "id",
				"render": function (id) {
					return `
						<div class="text-center">
						<a class="btn btn-success text-white"  href="/BookList/Edit?id=${id}">Edit</a>

						<a class="btn btn-danger text-white" onClick="DeleteBook('/api/book?id=${id}')">Delete</a>
						
						<div>
					`
				},
				"width": "20%"
			}
		],
		"language": {
			"emptyTable":"not data found"
		},
		"width":"100%"
	})
}

function DeleteBook(url)
{
		//sweet alert
	swal({
		title: "Confirm",
		text: "Once deleted, you wont be able to recover",
		icon: "warning",
		dangerMode: true,
		buttons:true
	}).then((willDelete) => {
		if (willDelete) {
			$.ajax({
				url,
				type: "DELETE",
				success: function (data) {
					if (data.success) {
						toastr.success(data.message)
						dataTable.ajax.reload()
					} else {
						toastr.success(data.error)

					}

				}
			})
		}
	})
}