var app = angular.module("lgapp", []);
app.controller("lgctrl", function ($scope, $http) {

    $scope.SigninAction = function (action_btn) {

        if (action_btn === 'signin') {
            ShowProgress();
            if (($scope.Username == undefined && $scope.Password == undefined) || ($scope.Username == '' && $scope.Password == '')) {
                MsgBox('E', 'Error', 'Please provide the Username and Password.');
            }
            else if ($scope.Username == undefined || $scope.Username == '') {
                MsgBox('E', 'Error', 'Please provide the Username.');
            }
            else if ($scope.Password == undefined || $scope.Password == '') {
                MsgBox('E', 'Error', 'Please provide the Password.');
            }
            else if ($scope.clientCaptcha == undefined || $scope.clientCaptcha == '') {
                MsgBox('E', 'Error', 'Please provide the Captcha.');
            }
            else {
                var durl = angular.element(document.getElementById("lgurl")).val();

                $http({
                    method: 'POST',
                    url: durl,
                    data: { Username: $scope.Username, Password: $scope.Password, c_Captcha: $scope.clientCaptcha, }
                }).then(function onSuccess(resp) {
                    HideProgress();
                    if (resp.data.Status.toUpperCase() == "SUCCESS") {
                        window.location.pathname = angular.element(document.getElementById("Hmurl")).val();                      
                    }
                    else {
                        MsgBox('E', 'Error', resp.data.MSG);
                    }
                }, function onFailure(err) {
                    MsgBox('W', 'Alert !', err);
                });
            }
        }
        else {
            $scope.Username = "";
            $scope.Password = "";
            $scope.clientCaptcha = "";
        }
    }

    var validationMsg = $('#ValidationMsg').val();
    if (validationMsg != null && validationMsg != undefined && validationMsg != "") {
        MsgBox('E', 'Error', validationMsg);
    }

    //$scope.openURL = function () {
    //    var shell = new ActiveXObject("WScript.Shell");
    //    shell.run("Chrome http://www.google.com");
    //}
});

app.directive('focus', function () {
    return {
        restrict: 'A',
        link: function ($scope, elem, attrs) {

            elem.bind('keydown', function (e) {
                var code = e.keyCode || e.which;
                if (code === 13) {
                    e.preventDefault();
                    elem.next().focus();
                }
            });
        }
    }
});
app.directive('ngEnter', function () { //a directive to 'enter key press' in elements with the "ng-enter" attribute

    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });
                event.preventDefault();
            }
        });
    };
});
