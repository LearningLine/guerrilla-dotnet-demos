(function () {
    'use strict';

    angular.module('app')
        .controller('PollCreateController', PollCreateController);

    PollCreateController.$inject = ['$location', 'PollsService']; 

    function PollCreateController($location, pollsService) {
        var vm = this;

        vm.addChoice = function() {
            vm.poll.choices.push({ choiceText: '' });
        };

        vm.create = function() {
            // strip the empty choices
            for (var index = vm.poll.choices.length - 1; index >= 0; index--) {
                var choice = vm.poll.choices[index];
                if (choice.choiceText.length === 0) {
                    vm.poll.choices.splice(index+1, 1);
                }
            }
            //validate we have at least 2
            if (vm.poll.choices.length > 1) {
                pollsService.create(vm.poll);
            }
        };

        (function activate() {
            vm.poll = { questionText: '', choices: [] };
            vm.addChoice();
            vm.addChoice();
        })();
    }
})();
