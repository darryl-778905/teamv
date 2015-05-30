'use strict';

/**
 * @ngdoc function
 * @name angularTestApp.controller:SurveyCtrl
 * @description
 * # SurveyCtrl
 * Controller of the angularTestApp
 */
angular.module('angularTestApp')
  .controller('SurveysCtrl', ['$scope', '$location', 'SurveyList',
    function ($scope, $location, SurveyList) {
			console.log('About to call getSurveys()');

      function getSurveys() {
         SurveyList.list().then(function(data){
           $scope.surveys = data.data;
           console.log(data);
         });
      }

          //.success(function (surveys) {
          //  $scope.surveys = surveys;
          //  console.log($scope.surveys);
          //})
          //.error(function (error) {
          //  console.log(error);
          //});
      //}

      getSurveys();

      $scope.goSurvey = function(survey){
        console.log('going to survey');
        console.log(survey);
        console.log('#/survey/' + survey.Id);

        $location.path('/survey/' + survey.Id);
      };


		}
	]);
