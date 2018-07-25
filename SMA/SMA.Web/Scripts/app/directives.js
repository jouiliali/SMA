'use strict';

angular.module('SMAApp.directives', []).
  directive('appVersion', ['version', function (version)
  {
      return function (scope, elm, attrs)
      {
          elm.text(version);
      };
  }]);