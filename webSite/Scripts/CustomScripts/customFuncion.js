function createCoockie(name,value) {

}

var deleteCookie = function (key) {
  console.log("eliminarCookie: " + key);
  return document.cookie = key + '=;path=/;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

