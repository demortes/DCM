$(document).ready(function () {
    $('.dataTable').dataTable({
        "columnDefs": [
            {"type": "date", "tagets":1}
        ]
    });
});