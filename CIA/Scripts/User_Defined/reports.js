var app = angular.module('Myreport', ['simple-autocomplete'])
app.controller('report_Controller', ['$scope', '$http', function ($scope, $http) {
    $scope.CEsName = [];

    $scope.onSelectdwnrpt = function (selection) {
        ShowProgress();
        //$scope.DMUID = selection.DMUID;
       
        $scope.clientName = selection.ce_name;
        
        HideProgress();
    };
    $scope.onFormLoaded_ = function () {
        ShowProgress();
        var durl = angular.element(document.getElementById("urlrptLoad")).val();

        $http({
            method: "POST",
            url: durl,
            data: {}
        }).then(function onSuccess(resp) {

            if (resp.data.Status == 'Success') {
                ShowProgress();
                $scope.BranchMst_generateBy = resp.data.obj.brchMst_details;
                $scope.BrnName = resp.data.obj.brchMst_details[0].CBM_BranchName;
                $scope.Brnacode = resp.data.obj.brchMst_details[0].CBM_BranchCode;

                $scope.ClitName = resp.data.obj.clientdrpdwn;
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

    $scope.btClk = function (btClkd) {
        ResetClientSideSessionTimers();
        ShowProgress();

        if (btClkd == 'view') {
            if ($scope.dwn_report == undefined || $scope.dwn_report == "-Select-") {
                MsgBox('E', 'Error', 'Please select Report.');
            }
            else if ($scope.invoc_genrated == undefined || $scope.invoc_genrated == "-Select-") {
                MsgBox('E', 'Error', 'Please select Branch.');
            }
            //else if ($scope.frmdate == undefined || $scope.frmdate == "") {
            //    MsgBox('E', 'Error', 'Please select Start Date.');
            //}
            //else if ($scope.enddate == undefined || $scope.enddate == "") {
            //    MsgBox('E', 'Error', 'Please select End date.');
            //}
            else if ($scope.dwn_report == 2 && report_opts == 'a' && ($scope.clientName == undefined || $scope.clientName == "")) {
                MsgBox('E', 'Error', 'Please select Client Name.');
            }
            else if ($scope.dwn_report == 2 && ($scope.report_opts == undefined || $scope.report_opts == "-Select-")) {
                MsgBox('E', 'Error', 'Please select Invoice sub-report.');
            }
            //else if ($scope.dwn_report == 2 && ($scope.report_typ == undefined || $scope.report_typ == "-Select-" || $scope.report_typ == "")) {
            //    //alert($scope.dwn_report);
            //    MsgBox('E', 'Error', 'Please select the Assessment Type.');
            //}
            else {
                var btnclk_V = angular.element(document.getElementById("onViewClicked"));

                $http({
                    method: 'POST',
                    url: btnclk_V.val(),
                    data: {
                        report_typ: $scope.dwn_report, seldbranch: $scope.invoc_genrated, frmDate: $scope.frmdate, endDate: $scope.enddate, clientName: $scope.clientName,
                        report_options: $scope.report_opts
                    }
                }).then(function onSuccess(resp) {

                    if (resp.data.Status == 'Success') {
                        ShowProgress();
                        // form load data
                        $scope.Clientdetails = resp.data.obj.client_details;
                        $scope.clientrpt = 'on';
                        //$scope.Brngentrms = resp.data.obj.brchMst_details[0].CBM_GeneralTerms;
                        //$scope.Brndetails = resp.data.obj.brchMst_details[0].CBM_FooterText;
                        //$scope.BrnRegno = resp.data.obj.brchMst_details[0].CBM_RegNo;
                        //$scope.inv_header = resp.data.obj.Grid_listed;
                    }
                    else {

                    }
                    HideProgress();
                },
                    function onFailure(err) {
                        MsgBox('E', 'Error', err);
                    });
            }
        }
        else {

        }
    }

}]);