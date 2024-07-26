var app = angular.module('cliententry', [])
app.controller('cliententry_ctrl', function ($scope, $http) {
    $scope.crt_ClientEntry_dtls = [];

    //On page loaded at first
    $scope.Load_grid = function () {
        ShowProgress();
        var durl = angular.element(document.getElementById("urlgrided")).val();
        //ShowProgress();

        $http({
            method: "POST",
            url: durl,
            data: {}

        }).then(function onSuccess(resp) {
            if (resp.data.Status == 'Success') {
                ShowProgress();
                $scope.CEdata_info = resp.data.obj.Grid_listed;
                $scope.clientno = resp.data.obj.ClientNo;
               
            }
            else {
                MsgBox('E', 'Error', 'An error occurred while processing your request.Please contact admin');
            }
            HideProgress();
        },
            function onFailure(err) {
                MsgBox('E', 'Error', err);
            });

       // HideProgress();
    }
    $scope.Load_grid();  
       
    // On button clicked action
    $scope.btClk = function (btn_action) {
        $scope.bnt_status = btn_action;
        ShowProgress();
        if (btn_action == 'add_client' || btn_action == 'update_client') {
            var clientNo = $scope.clientno;
            var client_name = $scope.clientname;
            var adresse = $scope.adresse;
            var codepostale = $scope.codepostale;
            var ville = $scope.ville;
            var pays = $scope.pays;
            var mobileno = $scope.mobileno;
            var courriel = $scope.courriel;
            var ligne_fixno = $scope.ligne_fixno;
            var organismsnom = $scope.organismsnom;
            var numero_professional = $scope.numero_professional;
            var facture_adresse = $scope.facture_adresse;
            var O_codepostale = $scope.O_codepostale;
            var Oville = $scope.Oville;
            var O_pays = $scope.O_pays;
            var Ocorriel = $scope.Ocorriel;
            var Oligne_fix = $scope.Oligne_fix;
            var O_mobileno = $scope.O_mobileno;
            var payment_terms = $scope.payment_terms;
            var comments = $scope.comments;

            if ($scope.isActive == false) {
                var CE_status = "I";
            }
            if ($scope.isActive == true) {
                var CE_status = "A";
            }            

            if ($scope.clientname == undefined || $scope.clientname == "") {
                MsgBox('E', 'Error', 'Enter Candidate Name.');
            }
            else if ($scope.adresse == undefined || $scope.adresse == "") {
                MsgBox('E', 'Error', 'Enter Requisition Number.');
            }
            else if ($scope.codepostale == undefined || $scope.codepostale == "") {
                MsgBox('E', 'Error', 'Enter Highest Education.');
            }
            else if ($scope.ville == undefined || $scope.ville == "") {
                MsgBox('E', 'Error', 'Enter Institute Name.');
            }
            else if ($scope.pays == 0 || $scope.pays == 'Select options') {
                MsgBox('E', 'Error', 'Select Year of Passing.');
            }
            //else if ($scope.courriel == undefined || $scope.courriel == "") {
            //    MsgBox('E', 'Error', 'Enter Work Experience.');
            //}
            else if ($scope.mobileno == undefined || $scope.mobileno == "") {
                MsgBox('E', 'Error', 'Enter Candidate Current Location.');
            }            
            else {
                var durl = angular.element(document.getElementById("req_createCE")).val();

                $scope.crt_ClientEntry_dtls.push({
                    'clientNo': clientNo, 'client_name': client_name, 'adresse': adresse, 'codepostale': codepostale, 'ville': ville, 'pays': pays, 'mobileno': mobileno,
                    'courriel': courriel, 'ligne_fixno': ligne_fixno, 'organismsnom': organismsnom, 'numero_professional': numero_professional, 'facture_adresse': facture_adresse, 'O_codepostale': $scope.O_codepostale,
                    'Oville': Oville, 'O_pays': O_pays, 'Ocorriel': Ocorriel, 'Oligne_fix': Oligne_fix, 'O_mobileno': O_mobileno, 'payment_terms': payment_terms, 'comments': comments, 'CE_status': CE_status
                });
                $http({
                    method: 'POST',
                    url: durl,
                    data: { ClientEntry_list: $scope.crt_ClientEntry_dtls, clicked_state: $scope.bnt_status }
                }).then(function onSuccess(resp) {
                    if (resp.data.Status == 'Success') {
                        HideProgress();
                        MsgBox('S', 'Success', resp.data.MSG);
                    }
                    else {
                        MsgBox('E', 'Error', resp.data.MSG);
                    }
                    HideProgress();
                },
                    function onFailure(err) {
                        MsgBox('E', 'Error', err);
                    });
            }
        }
        else if (btn_action == 'reset') {

        }
        else {

        }
    }

    $scope.oneditclk = function (CE_id) {
        ShowProgress();
        $scope.selCE_id = CE_id;

        var durl = angular.element(document.getElementById("onedit")).val();
        $http({
            method: "POST",
            url: durl,
            data: { selid: $scope.selCE_id }

        }).then(function onSuccess(resp) {
            if (resp.data.Status == 'Success') {
                $scope.fetchedList = resp.data.obj;
                $scope.clientno = resp.data.obj.CIclientnon_ID;
                $scope.clientname = resp.data.obj.CICE_nomclient;
                $scope.adresse = resp.data.obj.CICE_c_adresse;
                $scope.codepostale = resp.data.obj.CICE_c_codepostale;
                $scope.ville = resp.data.obj.CICE_c_ville;
                $scope.pays = resp.data.obj.CICE_c_pays;
                $scope.mobileno = resp.data.obj.CICE_c_mobile_non;
                $scope.courriel = resp.data.obj.CICE_c_courriel;
                $scope.ligne_fixno = resp.data.obj.CICE_c_lignefixe_non;
                $scope.organismsnom = resp.data.obj.CICE_nom_organisme;
                $scope.numero_professional = resp.data.obj.CICE_numero_enregistr_professional;
                $scope.facture_adresse = resp.data.obj.CICE_facture_adresse;
                $scope.O_codepostale = resp.data.obj.CICE_o_codepostale;
                $scope.Oville = resp.data.obj.CICE_o_ville;
                $scope.O_pays = resp.data.obj.CICE_o_pays;
                $scope.Ocorriel = resp.data.obj.CICE_o_courriel;
                $scope.Oligne_fix = resp.data.obj.CICE_o_lignefixe_non;
                $scope.O_mobileno = resp.data.obj.CICE_o_mobile_non;
                $scope.payment_terms = resp.data.obj.CICE_payment_terms;
                $scope.comments = resp.data.obj.CICE_comments;
                $scope.isActive = resp.data.obj.CICE_ActiveStatus;

                if ($scope.isActive == "A") {
                    $scope.isActive = true;
                }
                else {
                    $scope.isActive = false;
                }
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

    $scope.onclk_excel = function (tbl_value) {
        ResetClientSideSessionTimers();
        ShowProgress();

        var SaveToExcel_V = angular.element(document.getElementById("SaveToExcelCE"));

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
});