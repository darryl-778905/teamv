'use strict';

angular.module('TeamVSurveyClient').factory('SurveySingleton', ['$q', 'SurveyRequestFactory', function ($q, SurveyRequestFactory) {
  console.log('SurveyList.start');
  var instance = {};

  instance.list = function () {
    console.log('SurveyList.instance.list');
    var defer = $q.defer();

    if (!angular.isDefined(instance.data)) {
      SurveyRequestFactory.getSurveys().then(function (data) {
        instance.data = data;
        defer.resolve(instance.data);
      });
    } else {
      defer.resolve(instance.data);
    }

    return defer.promise;

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
