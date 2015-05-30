'use strict';

/**
 * @ngdoc service
 * @name angularTestApp.SurveryFactory
 * @description
 * # SurveryFactory
 * Factory in the angularTestApp.
 */
angular.module('angularTestApp')
    .factory('SurveyRequestFactory', ['$http', function($http) {

    var urlBase = 'http://localhost:9000/api/Survey/';

    var dataFactory = {};

    dataFactory.getSurveys = function () {
      console.log('SurveyRequestFactory.getSurveys');
		  return $http.get(urlBase);
    };

    return dataFactory;
}]);
