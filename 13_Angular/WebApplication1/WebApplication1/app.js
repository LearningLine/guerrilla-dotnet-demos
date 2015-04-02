/// <reference path="angular.min.js" />

// IIFE
(function () {

    var mod = angular.module("main", []);

    mod.factory("PersonSvc", function ($http) {
        return {
            getPersonAsync: function () {
                return $http.get('http://localhost:12968/people/1').then(function (result) {
                    result.data.name = result.data.fullName;
                    return result.data;
                });
            },
            updateAsync: function (person) {
                person.fullName = person.name;
                return $http.put('http://localhost:12968/people/1', person);
            }
        };
    });

    //mod.controller("FooCtrl", ($scope, PersonSvc) => {
    //});

    mod.controller("PersonCtrl", function ($scope, PersonSvc) {
        PersonSvc.getPersonAsync().then(function (person) {
            $scope.person = person;
        });


        //$scope.name = p.name;
        //$scope.age = p.age;

        $scope.addKid = function (person, name) {
            //$scope.newName = "";
            person.children.push({name:name});
        }

        $scope.removeKid = function (person, idx) {
            person.children.splice(idx, 1);
        }

        $scope.isValid = function (person) {
            return person && person.name && person.age && person.age > 0;
        }

        $scope.update = function (person) {
            PersonSvc.updateAsync(person);
        }
    });

})();

