'use strict';

/**
 * @ngdoc function
 * @name TeamVSurveyClient.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the TeamVSurveyClient
 */
angular.module('TeamVSurveyClient')
  .controller('QuestionnaireCtrl', ['$scope', '$location', 'SurveySingleton', '$routeParams',
    function ($scope, $location, SurveySingleton, $routeParams) {

      function getSurvey() {
        console.log('Loading surveys');
        SurveySingleton.list().then(function (data) {
          $scope.surveys = data.data;

          console.log($scope.surveys);

          var survey = {};
          console.log('Searching for survey [' + $routeParams.survey_id + '] in surveys ' + $scope.surveys.length);
          angular.forEach($scope.surveys, function(item){
            if (item.Id == $routeParams.survey_id) {
              $scope.questionnaire = item;
              console.log('Found questionnaire' + $scope.questionnaire);
            }
          });
        });
      }

      getSurvey();

      $scope.start = function () {
        console.log($routeParams.survey_id);
        console.log($scope.questionnaire.Questions[0].Id);
        $location.path('/question/' + $routeParams.survey_id + '&' + $scope.questionnaire.Questions[0].Id);
      }

      $scope.cancel = function () {
        $location.path('/surveys');
      }

    }
  ]);
