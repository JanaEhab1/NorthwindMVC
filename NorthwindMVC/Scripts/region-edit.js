$(document).on('click', '.edit-btn', function () {
    var regionId = $(this).data('id');
    console.log("Edit clicked for RegionID:", regionId);

    $.ajax({
        url: '/Region/GetRegion',
        type: 'GET',
        data: { id: regionId },
        success: function (response) {
           
            if (response.success) {
                $('#editRegionId').val(response.data.RegionID);
                $('#editRegionDescription').val(response.data.RegionDescription);

                var myModal = new bootstrap.Modal(document.getElementById('editRegionModal'));
                myModal.show();
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert('Error fetching data');
        }
    });
});

$('#saveEditBtn').click(function () {
    var regionData = {
        RegionId: $('#editRegionId').val(),
        RegionDescription: $('#editRegionDescription').val()
    };

    $.ajax({
        url: '/Region/UpdateRegion',
        type: 'POST',
        data: regionData,
        success: function (response) {
            if (response.success) {
                alert("Region updated successfully!");

                var myModalEl = document.getElementById('editRegionModal');
                var modal = bootstrap.Modal.getInstance(myModalEl);
                modal.hide();

                location.reload();
            } else {
                alert("Something went wrong!");
            }
        },
        error: function () {
            alert("Error while saving changes!");
        }
    });
});
