var allergenApp = angular.module("allergenApp", []); //MODULE

allergenApp.controller("allergenController", function ($scope, $http) {

    $http.get('http://localhost:59845/api/Allergens').then(function (d) {
        $scope.regdata = d.data;
        console.log(d.data);
    }, function (error) {
        alert("failed");
    });

    $scope.Delete = function (Id) {
        if (confirm("Are you sure you want to delete this Allergen?")) {
            var httpreq = {
                method: 'DELETE',
                url: 'http://localhost:59845/api/Allergens/' + Id
            }
            $http(httpreq).then(function () {
                alert("Allergen has been deleted");
            }, function (error) {
                alert("Failed to delete Allergen")
                });
        }
    }

}); //CONTROLLER