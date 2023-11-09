var order = {
    init: function () {
        order.tblOrder();
        order.tblOrderDetail();
        $('#btnSearch').click(function () {
            order.tblOrder();
        });
    },
    tblOrder: function () {
        var searchUrl = "/Admin/Order/GetOrder";
        $('#tblOrder').bootstrapTable('destroy');
        $('#tblOrder').bootstrapTable({
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
                    field: 'OrderCode',
                    title: 'Mã đơn hàng',
                    align: 'center',
                    valign: 'middler',
                    formatter: function (value, row, index) {
                        var html = '<a href="/Admin/Order/OrderDetail?orderId=' + row.Id + '">' + value + '</a>';
                        return html;
                    }
                },
                {
                    field: '',
                    title: 'Khách hàng',
                    align: 'center',
                    valign: 'middler',
                    formatter: function (value, row, index) {
                        var html = '';
                        if (row.FullName != null) {
                            html += '<p>Họ tên: ' + row.FullName + '</p>';
                        }
                        if (row.Phone != null) {
                            html += '<p>SĐT: ' + row.Phone+'</p>'
                        }
                        if (row.Address != null) {
                            html += '<p>Đ/C: ' + row.Address + '</p>'
                        }
                        return html;
                    }
                    
                },
                {
                    field: 'CreateDate',
                    title: 'Ngày tạo',
                    align: 'center',
                    valign: 'middler',
                    formatter: function (value, row, index) {
                        if (value != null && value != undefined) {
                            return value.substring(0, 10);
                        } else {
                            return '';
                        }
                    }
                },
                {
                    field: 'Status',
                    title: 'Trạng thái',
                    align: 'center',
                    valign: 'middler',
                    formatter: function (value, row, index) {
                        var html = '';
                        switch (value) {
                            case 0:
                                html += "Chờ xử lý";
                                break;
                            case 1:
                                html += "Đã xử lý";
                                break;
                            case 2:
                                html += "Đang giao hàng";
                                break;
                            case 3:
                                html += "Giao hàng thành công";
                                break;
                            case 4:
                                html += "Hủy"
                                break;
                        }
                        return html;
                    }
                },
            ],
            onLoadSuccess: function (data) {

            }
        });
    },
    tblOrderDetail: function () {
        var searchUrl = "/Admin/Order/GetOrderDetail";
        $('#tblOrderDetail').bootstrapTable('destroy');
        $('#tblOrderDetail').bootstrapTable({
            method: 'get',
            url: searchUrl,
            queryParams: function (p) {
                return {
                    orderId: $('#ipOrderId').val()
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
                },
                {
                    field: 'ColorName',
                    title: 'Màu sắc',
                    align: 'center',
                    valign: 'middler',
                },
                {
                    field: 'SizeName',
                    title: 'Size',
                    align: 'center',
                    valign: 'middler',
                },
                {
                    field: 'Quantity',
                    title: 'Số lượng',
                    align: 'center',
                    valign: 'middler',
                },
                {
                    field: 'Price',
                    title: 'Số tiền',
                    align: 'center',
                    valign: 'middler',
                    formatter: function (value, row, index) {
                        return accounting.formatMoney(value, "", 0, ".", ",", "%v%s");
                    }
                },
            ],
            onLoadSuccess: function (data) {
                var foot = $("#tblOrderDetail").find('tfoot');
                foot = "";
                $("#tblOrderDetail").find('tfoot').remove();
                if (foot.length == 0) foot = $('<tfoot style="background-color:#f7f494;">').appendTo("#tblOrderDetail");
                foot.append($('<td></td><td><b>Tổng tiền</b></td><td></td>'
                    + '<td></td><td style="text-align:center;" >' + accounting.formatMoney(data.totalPrice, "", 0, ".", ",", "%v%s")+'</td>'
                ));
            }
        });
    },
}
$(document).ready(function () {
    order.init();
});