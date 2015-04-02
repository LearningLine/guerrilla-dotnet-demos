/// <reference path="angular.min.js" />

// IIFE
(function () {

    var mod = angular.module("main", []);

    mod.factory("PersonSvc", function ($http) {
        return {
            getPerson: function () {
                return {
                    name: "Brock", age: 5,
                    children: [
                        { name: "Mira" },
                        { name: "Tess" }
                    ]
                };
            },
            update: function (person) {
                console.log("service is updating: ", person);
            }
        };
    });

    mod.controller("PersonCtrl", function ($scope, PersonSvc) {
        var p = PersonSvc.getPerson();

        $scope.person = p;

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
            return person.name && person.age && person.age > 0;
        }

        $scope.update = function (person) {
            PersonSvc.update(person);
        }
    });

})();

