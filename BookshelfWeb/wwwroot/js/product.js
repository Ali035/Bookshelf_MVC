// Grid API: Access to Grid API methods
let gridApi;

function deleteProduct(url) {
  Swal.fire({
    title: "Are you sure?",
    text: "You won't be able to revert this!",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Yes, delete it!",
  }).then((result) => {
    if (result.isConfirmed) {
      $.ajax({
        url: url,
        type: "DELETE",
        success: function (result) {
          // show result message
          if (result.success) {
            toastr.success(result.message);
          } else {
            toastr.error(result.message);
          }

          // refresh product data
          loadProductData();
        },
      });
    }
  });
}

function createActionButtons({ value }) {
  const gui = document.createElement("div");
  gui.innerHTML = `
        <a href="/admin/Product/Edit/${value}" class="btn btn-primary mx-2">
            <i class="bi bi-pencil-square"></i> Edit
        </a>
        <a onClick=deleteProduct('/admin/Product/Delete/${value}') class="btn btn-danger mx-2">
            <i class="bi bi-trash-fill"></i> Delete
        </a>
    `;
  return gui;
}

function loadProductData() {
  $.ajax({
    url: "/admin/product/getAll/",
    success: (result) => gridApi.setGridOption("rowData", result),
  });
}

// Grid Options: Contains all of the grid configurations
function getGridOptions(result) {
  const gridOptions = {
    // Data to be displayed
    rowData: result ?? [],

    // Define column types
    columnTypes: {
      actions: {
        width: 500,
      },
    },

    // Columns to be displayed (Should match rowData properties)
    columnDefs: [
      {
        field: "title",
        flex: 1.2,
        filter: true,
        floatingFilter: true,
        minWidth: 200,
      },
      { field: "isbn", filter: true, floatingFilter: true },
      { field: "price", filter: true, floatingFilter: true },
      { field: "category.name", filter: true, floatingFilter: true },
      {
        field: "id",
        headerName: "Actions",
        cellRenderer: createActionButtons,
        resizable: false,
        sortable: false,
        minWidth: 220,
      },
    ],
    defaultColDef: {
      flex: 1,
      minWidth: 150,
    },
    pagination: true,
    paginationPageSize: 10,
    paginationPageSizeSelector: [10, 20, 50, 100],
    rowHeight: 48,
  };
  return gridOptions;
}

$(function () {
  // Create Grid: Create new grid within the #myGrid div, using the Grid Options object
  gridApi = agGrid.createGrid(
    document.querySelector("#myGrid"),
    getGridOptions([])
  );

  // fetch and update table
  loadProductData();
});
