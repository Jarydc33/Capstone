app.service('CustomerService', function ($http) {
    this.getAllergens = function () {
        return $http.get('/Customers/GetAll');
    }
});