app.controller('CustomerController', function ($scope, CustomerService) {
    function getAllergensList() {
        CustomerService.getAllergens().then(function (aler) {
            $scope.AllergenList = aler.data;
        },function (error) {
            alert('Failed to fetch data');
        });
    }
});