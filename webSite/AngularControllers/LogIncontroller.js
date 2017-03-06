var LogInApp = angular.module("logInApp", []);
LogInApp.controller("LogIncontroller", function ($scope) {
  $scope.Register = function () {
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
  $scope.logIn = function () {
    var user = $scope.user;

    $.ajax({
      type: "POST",
      url: 'http://localhost:45778/api/Account/Login',
      dataType: 'json',
      data: user,
      success: function (data) {
        alert(data);
        document.cookie = "loginKey=" + data + ";path=/";
      }
    });

  }
  $scope.HolaMundo = function () {
    var user = $scope.user;
    var name = "loginKey" + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    
    var loginKey;
    for(var i = 0; i <ca.length; i++) {
      var c = ca[i];
      while (c.charAt(0) == ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
        loginKey = c.substring(name.length, c.length);
      }
    }
    alert(loginKey);
    $.ajax({
      type: "GET",
      headers: {
        'loginKey': loginKey
      },
      url: 'http://localhost:45778/api/Account/holamundo',
      success: function (data) {
        alert(data);
      }
    });

  }
  $scope.logOut = function () {
    for (var it in $.cookie())
      $.removeCookie(it);
  }
})