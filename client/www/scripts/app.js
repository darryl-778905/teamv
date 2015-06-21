'use strict';

/**
 * @ngdoc overview
 * @name TeamVSurveyClient
 * @description
 * # TeamVSurveyClient
 *
 * Main module of the application.
 */
angular
  .module('TeamVSurveyClient', [
    'ngAnimate',
    'ngAria',
    'ngCookies',
    'ngMessages',
    'ngResource',
    'ngRoute',
    'ngSanitize',
    'ngTouch',
    'ngStorage'
  ])
  .config(function ($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'views/main.html'
        /*,
         controller: 'MainCtrl'
         */
      })
      .when('/login', {
        templateUrl: 'views/login.html',
        controller: 'LoginCtrl'
      })
      .when('/surveys', {
        templateUrl: 'views/surveys.html',
        controller: 'SurveysCtrl'
      })
      .when('/question/:survey_id&:question_id', {
        templateUrl: 'views/question.html',
        controller: 'QuestionCtrl'
      })
      .when('/survey/:survey_id', {
        templateUrl: 'views/questionnaire.html',
        controller: 'QuestionnaireCtrl'
      })
      .when('/complete/:survey_id', {
        templateUrl: 'views/complete.html',
        controller: 'CompleteCtrl'
      })
      .otherwise({
        redirectTo: '/'
      });
  })
  .config(function ($httpProvider) {
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
  });
/*
  .config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
  });
*/
