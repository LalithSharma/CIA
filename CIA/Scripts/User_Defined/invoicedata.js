var app = angular.module('invoicentry', ['simple-autocomplete'])
app.controller('invoiceentry_ctrl', ['$scope', '$http', function ($scope, $http) {
    $scope.add_InvoiceEntry_header = [];
    $scope.invoice_dtls = [];
    $scope.CEsName = [];

    $scope.onSelectDMUValue = function (selection) {
        ShowProgress();
        //$scope.DMUID = selection.DMUID;
        for (var x = 0; x < $scope.CE_details.length; x++) {
            if (selection.ce_id == $scope.CE_details[x].CIclientnon_ID) {
                $scope.cl_org_address = $scope.CE_details[x].CICE_c_adresse;
                $scope.payment_trms = $scope.CE_details[x].CICE_c_pays;
                $scope.telephone = $scope.CE_details[x].CICE_c_mobile_non;
                $scope.mobileno = $scope.CE_details[x].CICE_c_mobile_non;
                $scope.c_courriel = $scope.CE_details[x].CICE_c_courriel;

                $scope.OrganismNom = $scope.CE_details[x].CICE_nom_organisme;
                $scope.OrganismAdress = $scope.CE_details[x].CICE_facture_adresse;
                $scope.O_telephone = $scope.CE_details[x].CICE_o_mobile_non;
                $scope.O_mobileno = $scope.CE_details[x].CICE_o_mobile_non;
                $scope.O_couriel = $scope.CE_details[x].CICE_o_courriel;
            }
        }        
        HideProgress();
    };

    $scope.onSelectMstgenerate_by = function () {
        for (var i = 0; i < $scope.BranchMst_generateBy.length; i++) {
            if ($scope.invoc_genrated == $scope.BranchMst_generateBy[i].CBM_BranchCode) {
                $scope.tva = $scope.BranchMst_generateBy[i].CBM_TVAPer
            }
        }
        
    }

    $scope.onFormLoaded_ = function () {
        ShowProgress();
        var durl = angular.element(document.getElementById("urlCElist")).val();

        $http({
            method: "POST",
            url: durl,
            data: {}
        }).then(function onSuccess(resp) {

            if (resp.data.Status == 'Success') {
                ShowProgress();
                // form load data
                $scope.CEsName = resp.data.obj.clientdrpdwn;
                $scope.CE_details = resp.data.obj.client_details;
                $scope.invoiceNo = resp.data.obj.Invoice_No;
                $scope.BranchMst_generateBy = resp.data.obj.brchMst_details;

                //Invoice preview details
                $scope.BrnName = resp.data.obj.brchMst_details[0].CBM_BranchName;
                $scope.Brnadresse = resp.data.obj.brchMst_details[0].CBM_Address;
                $scope.Brntelephone = resp.data.obj.brchMst_details[0].CBM_Tel;
                $scope.Brnmail = resp.data.obj.brchMst_details[0].CBM_Email;

                $scope.Brngentrms = resp.data.obj.brchMst_details[0].CBM_GeneralTerms;
                $scope.Brndetails = resp.data.obj.brchMst_details[0].CBM_FooterText;
                $scope.BrnRegno = resp.data.obj.brchMst_details[0].CBM_RegNo;
                $scope.BrntvaNo = resp.data.obj.brchMst_details[0].CBM_TVANo;
                //alert($scope.TempLisst);
                $scope.inv_header = resp.data.obj.Grid_listed;
            }
            else {

            }
            HideProgress();
        },
            function onFailure(err) {
                MsgBox('E', 'Error', err);
            });
        // HideProgress();
    }
    $scope.onFormLoaded_();

    //temporary data table storing
    $scope.tempbt_clicked = function (btn_action) {
        $scope.bnt_status = btn_action;
        ShowProgress();
        if (btn_action == 'add_invoice') {
            var s_no = 1;
             var refernce = $scope.refernce;
             var declaration = $scope.declaration;
             var intrv_taxable = $scope.intrv_taxable;
             var intrv_nontaxable = $scope.intrv_nontaxable;
            var at_tva_douane = $scope.at_tva_douane;
            var tva = $scope.tva;

            if ($scope.invc_date == undefined || $scope.invc_date == "") {
                MsgBox('E', 'Error', 'Enter Invoice date.');
            }
            else if ($scope.ce_data == undefined || $scope.ce_data == "") {
                MsgBox('E', 'Error', 'Select Client details.');
            }
            else if ($scope.invoc_genrated == undefined || $scope.invoc_genrated == "") {
                MsgBox('E', 'Error', 'Select Invoice generated form.');
            }
            else if ($scope.refernce == undefined || $scope.refernce == "") {
                MsgBox('E', 'Error', 'Enter Reference.');
            }
            else if ($scope.declaration == undefined || $scope.declaration == "") {
                MsgBox('E', 'Error', 'Enter Declaration.');
            }
            else if ($scope.intrv_taxable == undefined || $scope.intrv_taxable == "") {
                MsgBox('E', 'Error', 'Enter Intervention Taxable .');
            }
            else if ($scope.intrv_nontaxable == undefined || $scope.intrv_nontaxable == "") {
                MsgBox('E', 'Error', 'Enter Intervention Non Taxable.');
            }
            else if ($scope.at_tva_douane == undefined || $scope.at_tva_douane == "") {
                MsgBox('E', 'Error', 'Enter Candidate Current Location.');
            }            
            else {                

                $scope.net_ht = Number($scope.net_ht || 0) + Number($scope.intrv_taxable || 0);
                var perctageCal_val = ($scope.tva / 100) * $scope.net_ht;
                $scope.tva_euro = perctageCal_val;
                $scope.net_non_taxble = Number($scope.net_non_taxble || 0) + Number($scope.intrv_nontaxable || 0);
                $scope.net_douance = Number($scope.net_douance || 0) + Number($scope.at_tva_douane || 0);
                $scope.total_payble = $scope.net_ht + $scope.tva_euro + $scope.net_non_taxble + $scope.net_douance

                $scope.invoice_dtls.push({
                    'refernce': refernce, 'declaration': declaration, 'intrv_taxable': intrv_taxable, 'intrv_nontaxable': intrv_nontaxable,
                    'at_tva_douane': at_tva_douane, 'tva': tva, 'nwrow_tva_euro': $scope.tva_euro
                });
                                
                $scope.refernce = "";
                $scope.declaration = "";
                $scope.intrv_taxable = "";
                $scope.intrv_nontaxable = "";
                $scope.at_tva_douane = "";
                //$scope.tva = "";                                
            }
            
        }
        if (btn_action == 'reset') {

            $scope.en_invno = "";
            $scope.sel_invc_date = "";
            $scope.invc_date = "";
            $scope.ce_data = "";
            $scope.invoc_genrated = "";
            $scope.cl_org_address = "";
            $scope.payment_trms = "";
            $scope.refernce = "";
            $scope.declaration = "";
            $scope.intrv_taxable = "";
            $scope.intrv_nontaxable = "";
            $scope.at_tva_douane = "";
            $scope.tva = "";
        }
        HideProgress();
    }

    //remove invoice details
    $scope.RemoveIVDetails = function (index) {
       
        if ($scope.invoice_dtls.length > 0) {
            $scope.net_ht = $scope.net_ht - $scope.invoice_dtls[index].intrv_taxable;
            var perctageCal_val = ($scope.tva / 100) * $scope.net_ht;
            $scope.tva_euro = perctageCal_val;
            $scope.net_non_taxble = $scope.net_non_taxble - $scope.invoice_dtls[index].intrv_nontaxable;
            $scope.net_douance = $scope.net_douance - $scope.invoice_dtls[index].at_tva_douane;
            $scope.total_payble = $scope.net_ht + $scope.tva_euro + $scope.net_non_taxble + $scope.net_douance

            $scope.invoice_dtls.splice(index, 1)

        }
    }

    // On button clicked action
    $scope.btClk = function (btn_action) {
        $scope.bnt_status = btn_action;
        ShowProgress();
        if (btn_action == 'post_invoice' || btn_action == 'update_invoice') {
                       
            //var en_invno = $scope.en_invno;
            //var sel_invc_date = $scope.sel_invc_date;
            //var invc_date = $scope.invc_date;
            //var invoiceNo = $scope.invoiceNo;
            //var ce_data = $scope.ce_data;
            //var invoc_genrated = $scope.invoc_genrated;
            //var cl_org_address = $scope.cl_org_address;
            //var payment_trms = $scope.payment_trms;
            //var refernce = $scope.refernce;
            //var declaration = $scope.declaration;
            //var intrv_taxable = $scope.intrv_taxable;
            //var intrv_nontaxable = $scope.intrv_nontaxable;
            //var at_tva_douane = $scope.at_tva_douane;
            //var tva = $scope.tva;
            //var net_ht = $scope.net_ht;
            //var tva_euro = $scope.tva_euro;
            //var net_non_taxble = $scope.net_non_taxble;
            //var net_douance = $scope.net_douance;
            //var comments = $scope.comments;
            //var total_payble = $scope.total_payble;            

            //if ($scope.isActive || $scope.isActive == undefined) {
            //    var IE_status = "A";
            //}
            //else {
            //    var IE_status = "I";
            //}            

            //if ($scope.is_EditClk == "ON" && ($scope.en_invno == undefined || $scope.en_invno == "")) {
            //    MsgBox('E', 'Error', 'Enter Invoice no.');
            //}
            //else if ($scope.is_EditClk == "ON" && ($scope.sel_invc_date == undefined || $scope.sel_invc_date == "")) {
            //    MsgBox('E', 'Error', 'Seleect Inovice date.');
            //}
            //else if ($scope.invoice_dtls == undefined || $scope.invoice_dtls.length == 0) {
            //    MsgBox('E', 'Error', 'Should have atleast one row item for the Enter Invoice Detail.');
            //}
            //else {
            //    var durl = angular.element(document.getElementById("req_createIE")).val();
            //    //if ($scope.en_invno || $scope.en_invno == undefined) {
            //    //    $scope.en_invno = 'NULL';
            //    //}
            //    //else {
            //    //    $scope.en_invno = $scope.en_invno;
            //    //}
            //    //if ($scope.sel_invc_date || $scope.sel_invc_date == undefined) {
            //    //    $scope.sel_invc_date = 'NULL';
            //    //}
            //    //else {
            //    //    $scope.sel_invc_date = $scope.sel_invc_date;
            //    //}

            //    $scope.add_InvoiceEntry_header.push({
            //        'en_invno': en_invno, 'sel_invc_date': sel_invc_date, 'invc_date': invc_date, 'invoiceNo': invoiceNo, 'ce_data': ce_data, 'invoc_genrated': invoc_genrated, 'cl_org_address': cl_org_address, 'payment_trms': payment_trms,
            //        'refernce': refernce, 'declaration': declaration, 'intrv_taxable': intrv_taxable, 'intrv_nontaxable': intrv_nontaxable,'at_tva_douane': at_tva_douane, 'tva': tva,
            //        'net_ht': net_ht, 'tva_euro': tva_euro, 'net_non_taxble': net_non_taxble, 'net_douance': net_douance, 'comments': comments, 'total_payble': total_payble, 'IE_status': IE_status
            //    });
            //    $http({
            //        method: 'POST',
            //        url: durl,
            //        data: { InvoiceEntry_header: $scope.add_InvoiceEntry_header, InvoiceEntry_dtls: $scope.invoice_dtls, clicked_state: $scope.bnt_status }
            //    }).then(function onSuccess(resp) {
            //        if (resp.data.Status == 'Success') {
            //            HideProgress();
            //            $scope.is_EditClk = "OFF";
            //           // MsgBox('I', 'Success', 'Do you want to preview Invoice detail ?');
            //            HideInfo_MessageBox();
            //            document.getElementById("myModal").style.display = "block";
            //         }
            //        else {
            //            MsgBox('E', 'Error', resp.data.MSG);
            //        }
            //        HideProgress();
            //    },
            //        function onFailure(err) {
            //            MsgBox('E', 'Error', err);
            //        });
            //}
          //  MsgBox('I', 'Success', 'Do you want to preview Invoice detail ?');
            HideInfo_MessageBox();
            document.getElementById("myModal").style.display = "block";
        }
        else if (btn_action == 'reset') {
            $scope.is_EditClk = "OFF";
            $scope.is_chkedClk = "OFF";

            $scope.en_invno = "";
            $scope.sel_invc_date = "";
            $scope.invc_date = "";
            $scope.ce_data = "";
            $scope.invoc_genrated = "";
            $scope.cl_org_address = "";
            $scope.payment_trms = "";

            $scope.tva = "";
            $scope.net_ht = "";
            $scope.tva_euro = "";
            $scope.net_non_taxble = "";
            $scope.net_douance = "";
            $scope.comments = "";
            $scope.total_payble = "";

            $scope.invoice_dtls = "";
        }
        else {
           // $route.reload();
           
        }
        HideProgress();
    }

    $scope.onIEeditclk = function (IE_id) {
        ShowProgress();
        $scope.selIE_id = IE_id;

        var durl = angular.element(document.getElementById("onIEedit")).val();
        $http({
            method: "POST",
            url: durl,
            data: { selid: $scope.selIE_id }

        }).then(function onSuccess(resp) {
            if (resp.data.Status == 'Success') {
                $scope.fetchedIEList = resp.data.obj.getonEdit_header;
                $scope.fetchedIE_detailList = resp.data.obj.getonEdit_details;
                //$scope.invoiceNo = resp.data.obj.getonEdit_header.ie_invNo;
                //$scope.invc_date1 = resp.data.obj.getonEdit_header.ie_invDdate;
                $scope.en_invno = resp.data.obj.getonEdit_header.ie_invNo;
                $scope.invc_date1 = resp.data.obj.getonEdit_header.ie_invDdate;
                $scope.ce_data = resp.data.obj.getonEdit_header.ie_client;
                $scope.invoc_genrated = resp.data.obj.getonEdit_header.ie_invgenrtdBy;
                $scope.cl_org_address = resp.data.obj.getonEdit_header.ie_clientAdress;
                $scope.payment_trms = resp.data.obj.getonEdit_header.ie_paymntTrms;
                
                $scope.net_ht = resp.data.obj.getonEdit_header.ie_netht;
                $scope.tva_euro = resp.data.obj.getonEdit_header.ie_tvaeuro;
                $scope.net_non_taxble = resp.data.obj.getonEdit_header.ie_netnontaxble;
                $scope.net_douance = resp.data.obj.getonEdit_header.ie_netdouane;
                $scope.total_payble = resp.data.obj.getonEdit_header.ie_totalpay;
                $scope.comments = resp.data.obj.getonEdit_header.ie_comment;
                $scope.isActive = resp.data.obj.getonEdit_header.ie_status;

                $scope.invoice_dtls = $scope.fetchedIE_detailList;
                $scope.tva = $scope.fetchedIE_detailList[0].tvaperc;

                $scope.isActive = false;
             
                $scope.is_EditClk = "ON";
            }
            else {
                MsgBox('E', 'Error', 'An error occurred while processing your request.Please contact admin');
            }
            HideProgress();
        },
            function onFailure(err) {
                HideProgress();
                //  alert(err.data.statusText);
            });
        HideProgress();
    }

    //checkbox tick clicked
    $scope.isActive_clicked = function () {
        if (!$scope.isActive) {
            $scope.is_chkedClk = "OFF";
        }
        else {
            $scope.is_chkedClk = "ON";
        }
    }

    $scope.generatePDF = function () {
        var today = new Date();
        var curr_month = ("0" + (today.getMonth() + 1)).slice(-2);
        var curr_date = ("0" + today.getDate()).slice(-2);
        var curr_year = today.getFullYear();
        var curr_hours = ("0" + today.getHours()).slice(-2);
        var curr_minutes = ("0" + today.getMinutes()).slice(-2);
        var curr_seconds = ("0" + today.getSeconds()).slice(-2);
        var date = curr_year + '' + curr_month + '' + curr_date;
        var time = curr_hours + '' + curr_minutes + '' + curr_seconds;
        var dateTime = date + '' + time;
        kendo.drawing.drawDOM($("#PDFdwn")).then(function (group) {
            kendo.drawing.pdf.saveAs(group, "_" + dateTime + " .pdf");
        });
    }

    window.onkeyup = function (event) {
        if (event.keyCode == 27) {
            document.getElementById("myModal").style.display = "none";
        }
    }

    //$scope.GetClientReport = function () {
    //    ShowProgress();
    //    //$scope.selIE_id = IE_id;

    //    //var pdfView = document.getElementById("#PDFdwn").html();

    //    $scope.myText = angular.element(PDFdwn).html();
    //        //$("input[name='GridHtml']").val($("#PDFdwn").html());

    //    var durl = angular.element(document.getElementById("onIEpdfview")).val();
    //    $http({
    //        method: "POST",
    //        url: durl,
    //        data: { GridHtml: $scope.myText }

    //    }).then(function onSuccess(resp) {
    //        if (resp.data.Status == 'Success') {
    //            $scope.fetchedIEList = resp.data.obj.getonEdit_header;
               
    //        }
    //        else {
    //            MsgBox('E', 'Error', 'An error occurred while processing your request.Please contact admin');
    //        }
    //        HideProgress();
    //    },
    //        function onFailure(err) {
    //            HideProgress();
    //            //  alert(err.data.statusText);
    //        });
    //    HideProgress();
    //}
    //$scope.GetClientReport = function () {
    //    window.open('/InvoiceEntry/getPDFView', "_blank");
    //};

    $scope.Export = function () {
        html2canvas(document.getElementById('PDFdwn'), {
            onrendered: function (canvas) {
                var data = canvas.toDataURL();
                var docDefinition = {
                    content: [{
                        image: data,
                        width: 500
                    }]
                };
                pdfMake.createPdf(docDefinition).download("Table.pdf");
            }
        });
    }

    $scope.onclk_excel = function (tbl_value) {
        ResetClientSideSessionTimers();
        ShowProgress();

        var SaveToExcel_V = angular.element(document.getElementById("SaveToExcelIE"));
       
        $http({
            method: 'POST',
            url: SaveToExcel_V.val(),
            data: { tab_val: tbl_value }
        }).then(function (response) {
            //alert("xx");
            if (response.data.Status == 'Success') {
                // alert("xxx");
                HideProgress();
                ResetClientSideSessionTimers();
                var Download_Excel_V = angular.element(document.getElementById("Download_Excel"));
                window.location.href = Download_Excel_V.val();
            }
            else {
                HideProgress();
                ResetClientSideSessionTimers();
                MsgBox('E', 'Error', response.data.Msg);
                return;
            }
        }, function (error) {
            HideProgress();
            ResetClientSideSessionTimers();
            MsgBox('E', 'Error', error.data);
            return;
        });

    };
}]);