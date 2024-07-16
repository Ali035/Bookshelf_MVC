$(function () {
  loadProductData();
});

function loadProductData() {
  let data = $.ajax({
    url: "/admin/product/getAll/",
    success: onSuccess,
  });
}
function onSuccess(result) {
  // Grid API: Access to Grid API methods
  let gridApi;

  // Grid Options: Contains all of the grid configurations
  const gridOptions = {
    // Data to be displayed
    rowData: result,

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

  console.log(gridOptions);
  // Create Grid: Create new grid within the #myGrid div, using the Grid Options object
  gridApi = agGrid.createGrid(document.querySelector("#myGrid"), gridOptions);
}

function createActionButtons({ value }) {
  const gui = document.createElement("div");
  gui.innerHTML = `
        <a href="/admin/Product/Edit/${value}" class="btn btn-primary mx-2">
            <i class="bi bi-pencil-square"></i> Edit
        </a>
        <a href="/admin/Product/Delete/${value}" class="btn btn-danger mx-2">
            <i class="bi bi-trash-fill"></i> Delete
        </a>
    `;
  return gui;
}
