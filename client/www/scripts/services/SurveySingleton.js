'use strict';

angular.module('TeamVSurveyClient').factory('SurveySingleton', ['$q', 'SurveyRequestFactory', '$localStorage', function ($q, SurveyRequestFactory, $localStorage) {
  console.log('SurveyList.start');
  var instance = {};

  instance.local = function() {
    if (angular.isDefined(instance.data)) {
      return instance.data;

    } else if (angular.isDefined($localStorage.data)) {
      instance.data = JSON.parse(JSON.stringify($localStorage.data));
      return instance.data;
    }
  }

  instance.list = function () {
    console.log('SurveyList.instance.list');
    var defer = $q.defer();

    //if (!angular.isDefined(instance.data)) {
    SurveyRequestFactory.getSurveys().then(function (data) {
      $localStorage.data = JSON.parse(JSON.stringify(data));
      instance.data = data;
      defer.resolve(instance.data);
    }, function (error) {
      if (angular.isDefined(instance.data)) {
        defer.resolve(instance.data);

      } else if (angular.isDefined($localStorage.data)) {
        instance.data = JSON.parse(JSON.stringify($localStorage.data));
        defer.resolve(instance.data);
      }
    });

    return defer.promise;

  };

  instance.submit = function (survey_id) {
    console.log('SurveySingleton ' + survey_id);

    angular.forEach(instance.data.data, function (dataItem) {
      console.log('SurveySingleton [' + dataItem.Id + ']');
      if (dataItem.Id == survey_id) {
        console.log('SurveySingleton Found ' + survey_id);
        SurveyRequestFactory.postPollResult(dataItem)
          .success(function() {
            var results = [];
            if (angular.isDefined($localStorage.results)) {
              results = $localStorage.results;
            }
            angular.forEach(results, function (resultItem) {
              SurveyRequestFactory.postPollResult(resultItem);
            });
            $localStorage.results = [];
          })
          .error(function() {
            var results = [];
            if (angular.isDefined($localStorage.results)) {
              results = $localStorage.results;
            }
            results.push(JSON.parse(JSON.stringify(dataItem)));
            $localStorage.results = results;
            console.log("ERROR");
          });
      }
    });
  };

  //instance.updateSurveryAnswer = function (survery, answer) {
  //  console.log();
  //  var defer = $q.defer();
  //
  //  instance.list().then(function (data) {
  //    angular.forEach(data.data, function (item) {
  //      console.log(item);
  //      // if it's the right servy do the answer set it bla
  //    });
  //    defer.reslove({status: 'ok'});
  //  });
  //
  //  return defer.promise;

  //
  //instance.saveOffline = function() {
  //  instance.list().then(function(data){
  //    // save to local store
  //  })
  //}

  //}
  return instance;

}]);
