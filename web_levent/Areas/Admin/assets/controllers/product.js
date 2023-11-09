var product = {
    init: function () {
        product.tblProduct();
        var url = window.location.href;
        if (url.indexOf("Detail") != -1) {
            product.tblProductDetail();
            $('#btnAddĐetail').click(function () {
                $('#ipID_Detail').val(0);
                $('#ipName_Pro').val('');
                $('#ipImg_pro').val('');
                $('#ipPrice_Pro').val(0);
                $('#ipQuantity_Pro').val(0);
                $('#ipColorPr_Name').val('');
                $('#ipImg_ProColor').val('');
                $('#ipSize_Name').val('');
                $('#titleModal').text('Thêm chi tiết sản phẩm')
                $('#modalsDetailAddEdit').modal('show');
            });
        }
        $('#btnSearch').click(function () {
            product.tblProduct();
            product.tblProductDetail();
        });
        $('#btnAdd').click(function () {
            $('#ipHiddenId').val(0);
            $('#ipName_Pro').val('');
            $('#ipID_Cate').val(0);
            $('#ipImg_pro').val('');
            $('#ipPrice_Pro').val(0);
            $('#titleModal').text("Thêm mới sản phẩm");
            $('#modalsAddEdit').modal('show');
        });
        $('#btnSubmit').click(function () {
            var formData = new FormData();
            formData.append("ID_Pro", $('#ipHiddenId').val());
            formData.append("Name_Pro", $('#ipName_Pro').val());
            formData.append("ID_Cate", $('#ipID_Cate').val());
            formData.append("Price_Pro", $('#ipPrice_Pro').val());
            var fileInput = document.getElementById('ipImg_pro');
            formData.append("fileUpLoad", fileInput.files[0]);
            $.ajax({
                url: '/Product/CreateOrEdit',
                type: 'post',
                processData: false,
                contentType: false,
                data: formData,
                success: function (res) {
                    if (res.status) {
                        toastr.success(res.message);
                        $('#modalsAddEdit').modal('hide');
                        $('#tblProduct').bootstrapTable('refresh');
                    } else {
                        toastr.error(res.message);
                    }
                }
            })
        });
        $('#btnSubmitĐetail').click(function () {
            var formData = new FormData();
            formData.append("ID_Detail", $('#ipID_Detail').val());
            formData.append("ID_Pro", $('#ipID_Pro').val());
            formData.append("Name_Pro", $('#ipName_Pro').val());
            formData.append("Price_Pro", $('#ipPrice_Pro').val());
            formData.append("Quantity_Pro", $('#ipQuantity_Pro').val());
            var fileInput = document.getElementById('ipImg_pro');
            formData.append("fileUpLoad", fileInput.files[0]);
            formData.append("ColorPr_Name", $('#ipColorPr_Name').val());
            var fileInputColor = document.getElementById('ipImg_ProColor');
            formData.append("fileUpLoadColor", fileInputColor.files[0]);
            formData.append("ID_Cate", $('#ipCateId').val());
            formData.append("Size_Name", $('#ipSize_Name').val());
            $.ajax({
                url: '/Product/CreateDetail',
                type: 'post',
                processData: false,
                contentType: false,
                data: formData,
                success: function (res) {
                    if (res.status) {
                        toastr.success(res.message);
                        $('#modalsDetailAddEdit').modal('hide');
                        $('#tblProductDetail').bootstrapTable('refresh');
                    } else {
                        toastr.error(res.message);
                    }
                }
            })
        });
    },
    tblProduct: function () {
        var searchUrl = "/Admin/Product/GetProduct";
        $('#tblProduct').bootstrapTable('destroy');
        $('#tblProduct').bootstrapTable({
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
                    field: 'ProductName',
                    title: 'Tên sản phẩm',
                    align: 'center',
                    valign: 'middler',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value != null && value != undefined && value != '') {
                            html = '<a href="/Admin/Product/Detail/' + row.Id + '" title="Xem chi tiết">' + value + '</a>';
                        }
                        return html;
                    }
                },
                {
                    field: 'CateName',
                    title: 'Danh mục',
                    align: 'center',
                    valign: 'middler',
                },
                {
                    field: 'ImageProduct',
                    title: 'Ảnh',
                    align: 'center',
                    valign: 'middler',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value != null && value != undefined && value != '') {
                            html = '<img src="' + value + '" alt="Ảnh sản phẩm" width=50 height=50 />'
                        }
                        return html;
                    }
                },
                {
                    field: 'PriceProduct',
                    title: 'Số tiền',
                    align: 'center',
                    valign: 'middler',
                    formatter: function (value, row, index) {
                        return accounting.formatMoney(value, "", 0, ".", ",", "%v%s");
                    }
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
                            $('#ipHiddenId').val(row.Id);
                            $('#ipName_Pro').val(row.ProductName);
                            $('#ipID_Cate').val(row.CateId);
                            $('#ipImg_pro').val('');
                            $('#ipPrice_Pro').val(row.PriceProduct);
                            $('#titleModal').text("Chỉnh sửa sản phẩm");
                            $('#modalsAddEdit').modal('show');
                        },
                        'click .btnDelete': function (e, value, row, index) {
                            $.confirm({
                                title: 'Xác nhận!',
                                content: 'Bạn có muốn xóa thông tin này!',
                                buttons: {
                                    confirm: function () {
                                        $.ajax({
                                            url: '/Product/Delete',
                                            type: 'post',
                                            dataType: 'json',
                                            data: {
                                                Id: row.Id
                                            },
                                            success: function (res) {
                                                if (res.status) {
                                                    toastr.success(res.message);
                                                    $('#tblProduct').bootstrapTable('refresh');
                                                } else {
                                                    toastr.error(res.message);
                                                    $('#tblProduct').bootstrapTable('refresh');
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
    tblProductDetail: function () {
        var searchUrl = "/Admin/Product/GetProductDetail";
        $('#tblProductDetail').bootstrapTable('destroy');
        $('#tblProductDetail').bootstrapTable({
            method: 'get',
            url: searchUrl,
            queryParams: function (p) {
                return {
                    productId: $('#productId').val(),
                    search: $('#txtSearch').val(),
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
                    field: 'NamePro',
                    title: 'Tên sản phẩm',
                    align: 'center',
                    valign: 'middler',
                },
                {
                    field: 'PricePro',
                    title: 'Số tiền',
                    align: 'center',
                    valign: 'middler',
                    formatter: function (value, row, index) {
                        return accounting.formatMoney(value, "", 0, ".", ",", "%v%s");
                    }
                },
                {
                    field: 'ImgPro',
                    title: 'Ảnh',
                    align: 'center',
                    valign: 'middler',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value != null && value != undefined && value != '') {
                            html = '<img src="' + value + '" alt="Ảnh sản phẩm" width=50 height=50 />'
                        }
                        return html;
                    }
                },
                {
                    field: 'ColorName',
                    title: 'Màu',
                    align: 'center',
                    valign: 'middler',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (value != null && value != undefined && value != '') {
                            html = '<button type="button" class="btn btn-danger btn-sm btnColor">' + value + '</button>';
                        }
                        return html;
                    },
                    events: {
                        'click .btnColor': function (e, value, row, index) {
                            var html = '';
                            html += '<div class="swiper-slide"><img src="' + row.ImgColor + '" alt="" class="img-fluid"  /></div>';
                            $('#showImage').html(html);
                            $('#addModelShowImage').modal('show');
                        }
                    }
                },
                {
                    field: 'SizeName',
                    title: 'Size',
                    align: 'center',
                    valign: 'middler',
                },
                {
                    field: 'QuantityPro',
                    title: 'Số lượng',
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
                            $('#ipID_Detail').val(row.Id);
                            $('#ipName_Pro').val(row.NamePro);
                            $('#ipImg_pro').val('');
                            $('#ipPrice_Pro').val(row.PricePro);
                            $('#ipQuantity_Pro').val(row.QuantityPro);
                            $('#ipColorPr_Name').val(row.ColorName);
                            $('#ipImg_ProColor').val('');
                            $('#ipSize_Name').val(row.SizeName);
                            $('#titleModal').text('Chỉnh sửa chi tiết sản phẩm')
                            $('#modalsDetailAddEdit').modal('show');
                        },
                        'click .btnDelete': function (e, value, row, index) {
                            $.confirm({
                                title: 'Xác nhận!',
                                content: 'Bạn có muốn xóa thông tin này!',
                                buttons: {
                                    confirm: function () {
                                        $.ajax({
                                            url: '/Product/DeleteDetail',
                                            type: 'post',
                                            dataType: 'json',
                                            data: {
                                                Id: row.Id
                                            },
                                            success: function (res) {
                                                if (res.status) {
                                                    toastr.success(res.message);
                                                    $('#tblProductDetail').bootstrapTable('refresh');
                                                } else {
                                                    toastr.error(res.message);
                                                    $('#tblProductDetail').bootstrapTable('refresh');
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
    product.init();
});