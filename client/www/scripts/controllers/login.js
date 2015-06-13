'use strict';

/**
 * @ngdoc function
 * @name TeamVSurveyClient.controller:LoginCtrl
 * @description
 * # LoginCtrl
 * Controller of the TeamVSurveyClient
 */
angular.module('TeamVSurveyClient')
  .controller('LoginCtrl', function ($scope, $location) {
    $scope.userName = '';
    $scope.password = '';
    $scope.messages = [];

    console.log('Login controller');

    $scope.login = function () {
      console.log('About to log into application');
      $scope.messages = [];
      if (!$scope.userName) {
        $scope.messages.push('Your user name cannot be empty');
      }
      if (!$scope.password) {
        $scope.messages.push('Your password cannot be empty');
      }
      if ($scope.messages.length > -1) {
        console.log('About to change URL to /surveys');
        $location.path('/surveys');

      }
    }
  });
