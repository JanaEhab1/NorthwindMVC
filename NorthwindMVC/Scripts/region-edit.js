$(document).on('click', '.edit-btn', function () {
    var regionId = $(this).data('id');
    

    $.ajax({
        url: '/Region/GetRegion',
        type: 'GET',
        data: { id: regionId },
        success: function (response) {
           
            if (response.success) { 

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

$(document).ready(function () {
    $("#saveEditBtn").click(function () {
        var regionId = $("#editRegionId").val();
        var description = $("#editRegionDescription").val().trim();

        if (description=="") {
            alert("Region Description cannot be empty");
            return;
        }

        $.ajax({
            url: '/Region/UpdateRegion',
            type: 'POST',
            data: { RegionId: regionId, RegionDescription: description },
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    location.reload();
                } else {
                    alert("Error: " + response.message);
                }
            },
            error: function () {
                alert("An error occurred while updating the region.");
            }
        });
    });
});





