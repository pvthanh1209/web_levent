var category = {
    init: function () {
        category.tblCategory();
        $('#btnSearch').click(function () {
            category.tblCategory();
        });
        $('#btnAdd').click(function () {
            $('#titleModal').text("Thêm mới danh mục");
            $('#ipHiddenId').val(0);
            $('#ipName_Cate').val('');
            $('#modalsAddEdit').modal('show');
        });
        $('#btnSubmit').click(function () {
            $.ajax({
                url: '/Category/CreateOrEdit',
                type: 'post',
                dataType: 'json',
                data: $('#frmCategory').serialize(),
                success: function (res) {
                    if (res.status) {
                        toastr.success(res.message);
                        $('#modalsAddEdit').modal('hide');
                        $('#tblCategory').bootstrapTable('refresh');
                    } else {
                        toastr.error(res.message);
                    }
                }
            })
        });
    },
    tblCategory: function () {
        var searchUrl = "/Admin/Category/GetCategory";
        $('#tblCategory').bootstrapTable('destroy');
        $('#tblCategory').bootstrapTable({
            method: 'get',
            url: searchUrl,
            queryParams: function (p) {
                return {
                    search: $('#txtSearch').val()
                };
            },
            formatLoadingMessage: function () {
                return 'Đang tải dữ liệu...';
            },
            formatNoMatches: function () {
                return 'Không có dữ liệu';
            },
            striped: true,
            sidePagination: 'client',
            pagination: true,
            paginationVAlign: 'bottom',
            limit: 20,
            pageSize: 20,
            pageList: [20, 50, 100, 200, 500],
            search: false,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            columns: [
                {
                    field: 'Name_Cate',
                    title: 'Tên danh mục',
                    align: 'center',
                    valign: 'middler',
                },
                {
                    title: "Chức năng",
                    valign: 'middle',
                    align: 'center',
                    formatter: function (value, row, index) {
                        var action = '<a class="btn btn-primary btn-sm mr-2 btnEdit" href="javascript:void(0)" data-bs-toggle="tooltip" title="Sửa"><i class="bx bx-pencil"></i></a>\
                            <a href="javascript:void(0)" class="btn btn-danger btn-sm btnDelete mr-2 " data-bs-toggle="tooltip" title="Xóa"><i class="bx bx-x"></i></a>';
                        return action;
                    },
                    events: {
                        'click .btnEdit': function (e, value, row, index) {
                            $('#titleModal').text("Chỉnh sửa danh mục");
                            $('#ipHiddenId').val(row.ID_Cate);
                            $('#ipName_Cate').val(row.Name_Cate);
                            $('#modalsAddEdit').modal('show');
                        },
                        'click .btnDelete': function (e, value, row, index) {
                            $.confirm({
                                title: 'Xác nhận!',
                                content: 'Bạn có muốn xóa thông tin này!',
                                buttons: {
                                    confirm: function () {
                                        $.ajax({
                                            url: '/Category/Delete',
                                            type: 'post',
                                            dataType: 'json',
                                            data: {
                                                Id: row.ID_Cate
                                            },
                                            success: function (res) {
                                                if (res.status) {
                                                    toastr.success(res.message);
                                                    $('#tblCategory').bootstrapTable('refresh');
                                                } else {
                                                    toastr.error(res.message);
                                                    $('#tblCategory').bootstrapTable('refresh');
                                                }
                                            }
                                        })
                                    },
                                    cancel: function () {

                                    },
                                }
                            });
                        }
                    }
                },
            ],
            onLoadSuccess: function (data) {

            }
        });
    },
}
$(document).ready(function () {
    category.init();
});