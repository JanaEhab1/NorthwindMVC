$(document).on('click', '.delete-btn', function () {
    var regionId = $(this).data('id');

    if (confirm("Are you sure you want to delete this region?")) {
        $.ajax({
            url: '/Region/DeleteRegion',
            type: 'POST',
            data: { id: regionId },
            success: function (response) {
                if (response.success) {
                    alert(response.message);

                    $("button[data-id='" + regionId + "']").closest("tr").remove();
                } else {
                    alert("Error: " + response.message);
                }
            },
            error: function () {
                alert("An error occurred while trying to delete the region.");
            }
        });
    }
});
