(function () {
    'use strict';

    angular.module('app')
        .controller('PollVoteController', PollVoteController);

    PollVoteController.$inject = ['$routeParams', 'PollsService'];

    function PollVoteController($routeParams, pollsService) {
        var vm = this;
        vm.choiceId = null;

        vm.vote = function() {
            pollsService.vote(new {
                PollId: $routeParams.id,
                ChoiceId: vm.choiceId
            });
        };

        (function activate() {
            pollsService.getById($routeParams.id)
                .then(function (response) {
                    vm.poll = response.data;
                });
        })();
    }
})();
