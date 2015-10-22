(function () {
    'use strict';

    angular.module('app')
        .factory('PollsService', PollsService);

    PollsService.$inject = ['$http'];

    function PollsService($http) {

        function getAll() {
            return $http.get('http://localhost:2000/api/polls');
        }
        function getById(id) {
            return $http.get('http://localhost:2000/api/polls/' + id);
        }
        function vote(voteData) {
            return $http.post('http://localhost:2000/api/polls/' + id + '/vote', voteData);
        }


        return {
            getAll: getAll,
            getById: getById
        };

    }
})();