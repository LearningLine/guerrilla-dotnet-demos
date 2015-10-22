(function () {
    'use strict';

    angular.module('app')
        .controller('PollCreateController', PollCreateController);

    PollCreateController.$inject = ['$location']; 

    function PollCreateController($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'PollCreateController';

        activate();

        function activate() { }
    }
})();
