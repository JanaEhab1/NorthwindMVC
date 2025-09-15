$(document).ready(function () {
    $("#saveAddBtn").click(function () {
        var description = $("#addRegionDescription").val();

        if (!description.trim()) {
            alert("Region Description is required");
            return;
        }

        $.ajax({
            url: '/Region/AddRegion',
            type: 'POST',
            data: { RegionDescription: description },
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    location.reload(); 
                } else {
                    alert("Error: " + response.message);
                }
            },
            error: function () {
                alert("An error occurred while adding the region.");
            }
        });
    });
});
