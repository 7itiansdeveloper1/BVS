function MakeOnlinePayment(erpno, amount, duedate) {
    $.ajax({
        url: '../PaymentGateway/MakeOnlinepayment',
        data: { paymentMode: "Online", erpno: erpno, amount: amount, duedate: duedate },
        type: 'get',
        beforeSend: function () {
            spinnerShow();
        },
        success: (function (response) {
            if (response.status == 'success') {
                spinnerHide();
                //window.open(response.url, "_self");
            }
            else {
                spinnerHide();
                OpenMessegeAutoHideDiv(response.Msg, response.Color);
            }
        })
    });
};


