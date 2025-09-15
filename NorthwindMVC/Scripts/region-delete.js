//$(document).on('click', '.delete-btn', function () {
//    var regionId = $(this).data('id');
//    console.log("Delete button clicked for region:", regionId);




//    if (confirm("Are you sure you want to delete this region?")) {
//        $.ajax({
//            url: '/Region/DeleteRegion',
//            type: 'POST',
//            data: { id: regionId },
//            traditional: true,
//            success: function (response) {
//                console.log("Response:", response);
//                if (response.success) {
//                    alert("Region deleted successfully!");
//                    location.reload();
//                } else {
//                    alert(response.message || "Something went wrong!");
//                }
//            },
//            error: function (xhr, status, error) {
//                console.error("Delete failed:", xhr.status, xhr.responseText);
//                alert("Error while deleting region!");
//            }
//        });

//    }
//});