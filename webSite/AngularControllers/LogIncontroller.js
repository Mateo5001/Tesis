var LogInApp = angular.module("logInApp", []);
LogInApp.controller("LogIncontroller", function ($scope) {
  $scope.logIn = function () {
    var user = $scope.user;
    
    $.ajax({
      type: "POST",
      url: 'http://localhost:45778/api/Account/RegisterUser',
      dataType: 'json',
      data:  user,
      success: function () {
        alert('sii');
      }
    });

  }

})