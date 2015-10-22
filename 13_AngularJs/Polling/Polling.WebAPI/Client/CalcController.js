(function () {
    'use strict';

    angular.module('app')
        .controller('CalcController', CalcController);

    function CalcController() {
        var vm = this;
        vm.firstNum = 40;
        vm.secondNum = 2;
    }
})();
