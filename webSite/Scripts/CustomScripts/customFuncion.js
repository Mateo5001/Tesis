function createCoockie(name,value) {
  console.log("crearCookie: " + name);
  document.cookie = name + "=" + value + ";path=/";
}

var deleteCookie = function (key) {
  console.log("eliminarCookie: " + key);
  return document.cookie = key + '=;path=/;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

