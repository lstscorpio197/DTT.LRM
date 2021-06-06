$(document).ready(function () {
    $('#myTable').DataTable({
        "language": {
            "lengthMenu": "Hiện thị _MENU_ bản ghi",
            "zeroRecords": "Không có bản ghi",
            "infoEmpty": "Bản ghi không tồn tại",
            "info": "Hiển thị _START_ đến _END_ của _TOTAL_ entries",
            "infoFiltered": "(Được lọc từ _MAX_ tổng số bản ghi)",
            "search": "Tìm kiếm", "paginate": {
                "previous": "Trang trước", "next": "Trang sau"
            }
        }
    });
});