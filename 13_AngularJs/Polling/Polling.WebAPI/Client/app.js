(function () {
    'use strict';

    var app = angular.module('app', [
        // Angular modules 
        'ngRoute'

        // Custom modules 

        // 3rd Party Modules
        
    ]);
    app.config(function($routeProvider) {
        $routeProvider
            .when('/calc', {
                templateUrl: "/client/calc.html"
            })
            .when('/polls', {
                templateUrl: "/client/pollsIndex.html"
            })
            .when('/poll/create', {
                templateUrl: "/client/pollCreate.html"
            })
            .when('/poll/:id', {
                templateUrl: "/client/pollDetails.html"
            })
            .when('/poll/:id/vote', {
                templateUrl: "/client/pollVote.html"
            })
            .otherwise({ redirectTo: "/polls" });
    });

})();