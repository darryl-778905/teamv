'use strict';

/**
 * @ngdoc function
 * @name angularTestApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the angularTestApp
 */
angular.module('angularTestApp')
  .controller('SurveyCtrl', ['$scope', '$location', 'SurveyRequestFactory',
    function ($scope, $location, SurveyRequestFactory) {

      $scope.answer = function(survey, answer){
        SurveyRequestFactory.updateSurveryAnswer(survey, answer).then(function(result){
          console.log(result);
        });
      };

    }
  ]);
