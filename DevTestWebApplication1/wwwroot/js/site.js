(function ($) {
    //TO DO fix date display format
    var table = $("#userBillsTable").DataTable({
        "searching": false,
        "retrieve": true,
        "bServerSide": true,
        "ordering": false,
        "info": false,
        "bLengthChange": false,
        "language": {
            "paginate": {
                "previous": "<",
                "next": ">"
            }
        },
        "ajax": {
            "url": "/api/Bills/UserBills",
            "type": "POST",
            "contentType": "application/x-www-form-urlencoded",
            "dataType": "json",
            "data": function (a) { },
            "error": function (a) {
            }
        },
        "columns": [
            { "data": "billDate" },
            { "data": "amount" },
            { "data": "status" },
            { "data": "paidDate" },
            {
                "data": "status",
                "render": function (status) {
                    if (status === "NotPaid") {
                        return "<center><button class='btn btn-primary' click-action='pay' data-toggle='modal' data-target='#paymentModal'>Pay bill</button></center>";
                    }
                    return "";
                }
            }
        ]
    });

    $("#userBillsTable tbody").on("click", "button", function (event) {
        var data = table.row($(this).parents('tr')).data();
        switch ($(this).attr("click-action")) {
            case "pay": fillPayBillDialog(data);
                break;
            default:
                break;
        }
    });

    var fillPayBillDialog = function (data) {
        $("#billId").val(data.billId);
        $("#billDate").text(data.billDate);
        $("#billAmount").text(data.amount);
    }


    //TO DO: validate credit card input
    $("#payBillConfirm").on("click", function () {
        var data = {
            billId: $("#billId").val(),
            creditCard: $("#creditCardInput").val(),
            cardHolder: $("#cardholderNameInput").val(),
            expirationMonth: $("#expirationMonthInput").val()
        }
        $.ajax({
            method: "POST",
            url: "/api/Bills/PayBill",
            data: data
        }).done(function (result) {
            $(".close").trigger("click");
            table.ajax.reload();
        }).fail(function (a) {
            // TO DO implement exception handler for javascript
        });
    });
})(jQuery);
