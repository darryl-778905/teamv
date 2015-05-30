'use strict';

describe('Service: Surveys', function () {

  // load the service's module
  beforeEach(module('angularTestApp'));

  // instantiate service
  var Surveys;
  beforeEach(inject(function (_Surveys_) {
    Surveys = _Surveys_;
  }));

  it('should do something', function () {
    expect(!!Surveys).toBe(true);
  });

});
