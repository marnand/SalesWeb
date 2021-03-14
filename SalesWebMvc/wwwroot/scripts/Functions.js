
function confirmDelete(uniqueId, isDeleteClicked)
{
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else
    {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}

function myfunction(uniqueId) {
    $("#deleteConfirmButton").on("click", function () {
        $("#deleteConfirm").modal("show");
    });
}