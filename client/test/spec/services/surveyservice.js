'use strict';

describe('Service: SurveyService', function () {

  // load the service's module
  beforeEach(module('TeamVSurveyClient'));

  // instantiate service
  var SurveyService;
  beforeEach(inject(function (_SurveyService_) {
    SurveyService = _SurveyService_;
  }));

  it('should do something', function () {
    expect(!!SurveyService).toBe(true);
  });

});
