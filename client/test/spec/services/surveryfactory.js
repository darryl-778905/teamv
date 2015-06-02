'use strict';

describe('Service: SurveryFactory', function () {

  // load the service's module
  beforeEach(module('TeamVSurveyClient'));

  // instantiate service
  var SurveryFactory;
  beforeEach(inject(function (_SurveryFactory_) {
    SurveryFactory = _SurveryFactory_;
  }));

  it('should do something', function () {
    expect(!!SurveryFactory).toBe(true);
  });

});
