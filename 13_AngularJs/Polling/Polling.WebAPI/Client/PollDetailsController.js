(function () {
    'use strict';

    angular.module('app')
        .controller('PollDetailsController', PollDetailsController);

    PollDetailsController.$inject = ['$routeParams', 'PollsService']; 

    function PollDetailsController($routeParams, pollsService) {
        var vm = this;

        (function activate() {
            pollsService.getById($routeParams.id)
                .then(function(response) {
                    vm.poll = response.data;
                });
        })();
    }
})();
