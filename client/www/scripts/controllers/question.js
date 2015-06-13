'use strict';

/**
 * @ngdoc function
 * @name TeamVSurveyClient.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the TeamVSurveyClient
 */
angular.module('TeamVSurveyClient')
  .controller('QuestionCtrl', ['$scope', '$location', 'SurveySingleton', '$routeParams',
    function ($scope, $location, SurveySingleton, $routeParams) {
      console.log('survey_id:   ' + $routeParams.survey_id);
      console.log('question_id: ' + $routeParams.question_id);

      function forwardPage() {
        if ($scope.index < $scope.questions.length) {
            $location.path('/question/' + $routeParams.survey_id + '&' + $scope.questions[$scope.index].QuestionNumber);
        } else {
          console.log("SUBMITTING" + $routeParams.survey_id);
          SurveySingleton.submit($routeParams.survey_id);
          $location.path('/complete/' + $routeParams.survey_id);
        }
      };

      function getSurvey() {
        console.log('Loading surveys');
        SurveySingleton.list().then(function (data) {
          $scope.surveys = data.data;

          console.log($scope.surveys);

          var survey = {};
          console.log('Searching for survey [' + $routeParams.survey_id + '] in surveys ' + $scope.surveys.length);
          angular.forEach($scope.surveys, function(item){
            if (item.Id == $routeParams.survey_id) {
              console.log('Found Survey. Size [' + item.Questions.length + ']');
              $scope.questions = item.Questions;
              var index;
              for (index = 0; index < item.Questions.length; index++) {
                  console.log('Comparing [' + item.Questions[index].QuestionNumber + '] with [' + $routeParams.question_id + ']');
                  if (item.Questions[index].QuestionNumber == $routeParams.question_id) {
                  $scope.question = item.Questions[index];
                  $scope.index = index + 1;
                  $scope.count = item.Questions.length;
                  console.log('Found question' + $scope.question);
                }
              }
            }
          });
        });
      }

      getSurvey();

      $scope.checkboxStyles = ['default', 'primary', 'success', 'danger', 'warning', 'info'];

      $scope.yn = function(answer) {
        //Update Answer
        console.log('Answer ' + $scope.index);
        console.log('YN Answer ' + answer);

        $scope.question.Answers = [answer];

        forwardPage();

      };

      $scope.mc = function(answers) {
        console.log('Answer ' + $scope.index);
        console.log('Freetext Answer ' + answers);

        forwardPage();
      }

      $scope.freeText;

      $scope.ft = function(){
        //Update Answer
        console.log('Answer ' + $scope.index);
        console.log('Freetext Answer ' + $scope.freeText);

        $scope.question.Answers = [$scope.freeText];

        //forwardPage();

      };

    }
  ]);
