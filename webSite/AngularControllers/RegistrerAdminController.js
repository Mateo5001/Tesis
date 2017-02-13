var Register = angular.module("Register", []);
Register.controller("RegistrerAdminController", function ($scope) {
  $scope.Register = function () {
    var Registrer= $scope.Registrer;


    $.ajax({
      type: "POST",
      url: 'http://localhost:45778/api/Account/RegisterAdmin',
      dataType: 'json',
      data: Registrer,
      success: function () {
        alert('sii');
      }
    });

  }

})
