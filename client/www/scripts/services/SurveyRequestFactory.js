'use strict';

/**
 * @ngdoc service
 * @name TeamVSurveyClient.SurveryFactory
 * @description
 * # SurveryFactory
 * Factory in the TeamVSurveyClient.
 */
angular.module('TeamVSurveyClient')
    .factory('SurveyRequestFactory', ['$http', function($http) {

    var urlBase = 'http://192.168.0.1:9000/api/Survey/';

    var dataFactory = {};

    dataFactory.getSurveys = function () {
      console.log('SurveyRequestFactory.getSurveys');
		  return $http.get(urlBase);
    };

    return dataFactory;
}]);
