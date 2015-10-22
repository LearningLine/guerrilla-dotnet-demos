(function () {
    'use strict';

    angular.module('app')
        .controller('PollsIndexController', PollsIndexController);

    PollsIndexController.$inject = ['$location', 'PollsService'];

    function PollsIndexController($location, pollsService) {
        var vm = this;

        (function activate() {
            pollsService.getAll()
                .then(function(response) {
                    vm.polls = response.data;
                });
        })();
    }
})();
