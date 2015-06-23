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

    var urlBase = 'http://msi:9000/api/';

    var dataFactory = {};

    dataFactory.getSurveys = function () {

      console.log('SurveyRequestFactory.getSurveys');
		  return $http.get(urlBase +'Survey/');
    };

    dataFactory.postPollResult = function (dataItem) {
		console.log('SurveyRequestFactory.postPollResult:' + JSON.stringify(dataItem));
		return $http.post(urlBase + 'PollResult/', dataItem);
    };

    return dataFactory;
}]);
