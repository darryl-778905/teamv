'use strict';

/**
 * @ngdoc function
 * @name angularTestApp.controller:LoginCtrl
 * @description
 * # LoginCtrl
 * Controller of the angularTestApp
 */
angular.module('angularTestApp')
  .controller('LoginCtrl', function ($scope, $location) {
	$scope.userName = '';
	$scope.password = '';
	$scope.messages = [];

	$scope.login = function() {
	  console.log('About to log into application');
	  $scope.messages = [];
	  /*if(!$scope.userName) {
		  $scope.messages.push('Your user name cannot be empty');
	  }
	  if(!$scope.password) {
		  $scope.messages.push('Your password cannot be empty');
	  }
	  if($scope.messages.length == 0) {
	  //do the actual login
	  */
		$location.url('#/surveys');
	//}
  };
  });
