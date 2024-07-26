/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2016 Aditya Sharat
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
 *
 * Use this directive to convert drop downs into chosen drop downs.
 * http://harvesthq.github.io/chosen/
 * http://adityasharat.github.io/angular-chosen/
 */
//(function (angular) {
//  var AngularChosen = angular.module('angular.chosen', []);


//  function chosen($timeout) {
//    var EVENTS, scope, linker, watchCollection;

//    /*
//     * List of events and the alias used for binding with angularJS
//     */
//    EVENTS = [{
//      onChange: 'change'
//    }, {
//      onReady: 'chosen:ready'
//    }, {
//      onMaxSelected: 'chosen:maxselected'
//    }, {
//      onShowDropdown: 'chosen:showing_dropdown'
//    }, {
//      onHideDropdown: 'chosen:hiding_dropdown'
//    }, {
//      onNoResult: 'chosen:no_results'
//    }];

//    /*
//     * Items to be added in the scope of the directive
//     */
//    scope = {
//      options: '=', // the options array
//      ngModel: '=', // the model to bind to,,
//      ngDisabled: '='
//    };

//    /*
//     * initialize the list of items
//     * to watch to trigger the chosen:updated event
//     */
//    watchCollection = [];
//    Object.keys(scope).forEach(function (scopeName) {
//      watchCollection.push(scopeName);
//    });

//    /*
//     * Add the list of event handler of the chosen
//     * in the scope.
//     */
//    EVENTS.forEach(function (event) {
//      var eventNameAlias = Object.keys(event)[0];
//      scope[eventNameAlias] = '=';
//    });

//    /* Linker for the directive */
//    linker = function ($scope, iElm, iAttr) {
//      var maxSelection = parseInt(iAttr.maxSelection, 10),
//        searchThreshold = parseInt(iAttr.searchThreshold, 10);

//      if (isNaN(maxSelection) || maxSelection === Infinity) {
//        maxSelection = undefined;
//      }

//      if (isNaN(searchThreshold) || searchThreshold === Infinity) {
//        searchThreshold = undefined;
//      }

//      var allowSingleDeselect = iElm.attr('allow-single-deselect') !== undefined ? true : false;
//      var noResultsText = iElm.attr('no-results-text') !== undefined ? iAttr.noResultsText : "No results found.";
//      var disableSearch = iElm.attr('disable-search') !== undefined ? JSON.parse(iAttr.disableSearch) : false;
//      var placeholderTextSingle = iElm.attr('placeholder-text-single') !== undefined ? iAttr.placeholderTextSingle : "Select an Option";
//      var placeholderTextMultiple = iElm.attr('placeholder-text-multiple') !== undefined ? iAttr.placeholderTextMultiple : "Select Some Options";
//      var displayDisabledOptions = iElm.attr('display-disabled-options') !== undefined ? JSON.parse(iAttr.displayDisabledOptions) : true;
//      var displaySelectedOptions = iElm.attr('display-selected-options') !== undefined ? JSON.parse(iAttr.displaySelectedOptions) : true;

//      iElm.chosen({
//        width: '100%',
//        max_selected_options: maxSelection,
//        disable_search_threshold: searchThreshold,
//        search_contains: true,
//        allow_single_deselect: allowSingleDeselect,
//        no_results_text: noResultsText,
//        disable_search: disableSearch,
//        placeholder_text_single: placeholderTextSingle,
//        placeholder_text_multiple: placeholderTextMultiple,
//        display_disabled_options: displayDisabledOptions,
//        display_selected_options: displaySelectedOptions
//      });

//      iElm.on('change', function () {
//        iElm.trigger('chosen:updated');
//      });

//      $scope.$watchGroup(watchCollection, function () {
//        $timeout(function () {
//          iElm.trigger('chosen:updated');
//        }, 100);
//      });

//      // assign event handlers
//      EVENTS.forEach(function (event) {
//        var eventNameAlias = Object.keys(event)[0];

//        if (typeof $scope[eventNameAlias] === 'function') { // check if the handler is a function
//          iElm.on(event[eventNameAlias], function (event) {
//            $scope.$apply(function () {
//              $scope[eventNameAlias](event);
//            });
//          }); // listen to the event triggered by chosen
//        }
//      });
//    };

//    // return the directive
//    return {
//      name: 'chosen',
//      scope: scope,
//      restrict: 'A',
//      priority: 1,
//      link: linker
//    };
//  }
//  AngularChosen.directive('chosen', ['$timeout', chosen]);
//}(angular));
/*
 * Use this directive to convert drop downs into chosen drop downs.
 * http://harvesthq.github.io/chosen/
 * http://adityasharat.github.io/angular-chosen/
 */
(function (angular) {
    var AngularChosen = angular.module('angular.chosen', []);


    function chosen($timeout) {
        var EVENTS, scope, linker, watchCollection;

        /*
         * List of events and the alias used for binding with angularJS
         */
        EVENTS = [{
            onChange: 'change'
        }, {
            onReady: 'chosen:ready'
        }, {
            onMaxSelected: 'chosen:maxselected'
        }, {
            onShowDropdown: 'chosen:showing_dropdown'
        }, {
            onHideDropdown: 'chosen:hiding_dropdown'
        }, {
            onNoResult: 'chosen:no_results'
        }];

        /*
         * Items to be added in the scope of the directive
         */
        scope = {
            options: '=', // the options array
            ngModel: '=', // the model to bind to,,
            ngDisabled: '='
        };

        /*
         * initialize the list of items
         * to watch to trigger the chosen:updated event
         */
        watchCollection = [];
       
        Object.keys(scope).forEach(function (scopeName) {
            watchCollection.push(scopeName);
        });

        /*
         * Add the list of event handler of the chosen
         * in the scope.
         */
        EVENTS.forEach(function (event) {
            var eventNameAlias = Object.keys(event)[0];
            scope[eventNameAlias] = '=';
        });

        /* Linker for the directive */
        linker = function ($scope, iElm, iAttr) {
            var maxSelection = parseInt(iAttr.maxSelection, 10),
              searchThreshold = parseInt(iAttr.searchThreshold, 10);

            if (isNaN(maxSelection) || maxSelection === Infinity) {
                maxSelection = undefined;
            }

            if (isNaN(searchThreshold) || searchThreshold === Infinity) {
                searchThreshold = undefined;
            }

            var allowSingleDeselect = iElm.attr('allow-single-deselect') !== undefined ? true : false;

            iElm.chosen({
                width: '100%',
                max_selected_options: maxSelection,
                disable_search_threshold: searchThreshold,
                search_contains: true,
                allow_single_deselect: allowSingleDeselect
            });


           

            iElm.on('change', function () {
               iElm.trigger('chosen:updated');
               
            });

            $scope.$watchGroup(watchCollection, function () {
                $timeout(function () {
                    iElm.trigger('chosen:updated');
                  
                }, 100);
            });
           
           
            //ends

            // assign event handlers
            EVENTS.forEach(function (event) {
                var eventNameAlias = Object.keys(event)[0];

                if (typeof $scope[eventNameAlias] === 'function') { // check if the handler is a function
                    iElm.on(event[eventNameAlias], function (event) {
                        $scope.$apply(function () {
                            $scope[eventNameAlias](event);
                        });
                    }); // listen to the event triggered by chosen
                }
            });
        };

        // return the directive
        return {
            name: 'chosen',
            scope: scope,
            restrict: 'A',
            priority: 1,
            link: linker
        };
    }
    AngularChosen.directive('chosen', ['$timeout', chosen]);
}(angular));

//Localyst Chosen ****************************************************


//// Generated by CoffeeScript 1.8.0
//(function () {
//    var __indexOf = [].indexOf || function (item) { for (var i = 0, l = this.length; i < l; i++) { if (i in this && this[i] === item) return i; } return -1; };

//    angular.module('localytics.directives', []);

//    angular.module('localytics.directives').directive('chosen', [
//      '$timeout', function ($timeout) {
//          var CHOSEN_OPTION_WHITELIST, NG_OPTIONS_REGEXP, isEmpty, snakeCase;
//          NG_OPTIONS_REGEXP = /^\s*(.*?)(?:\s+as\s+(.*?))?(?:\s+group\s+by\s+(.*))?\s+for\s+(?:([\$\w][\$\w]*)|(?:\(\s*([\$\w][\$\w]*)\s*,\s*([\$\w][\$\w]*)\s*\)))\s+in\s+(.*?)(?:\s+track\s+by\s+(.*?))?$/;
//          CHOSEN_OPTION_WHITELIST = ['noResultsText', 'allowSingleDeselect', 'disableSearchThreshold', 'disableSearch', 'enableSplitWordSearch', 'inheritSelectClasses', 'maxSelectedOptions', 'placeholderTextMultiple', 'placeholderTextSingle', 'searchContains', 'singleBackstrokeDelete', 'displayDisabledOptions', 'displaySelectedOptions', 'width'];
//          snakeCase = function (input) {
//              return input.replace(/[A-Z]/g, function ($1) {
//                  return "_" + ($1.toLowerCase());
//              });
//          };
//          isEmpty = function (value) {
//              var key;
//              if (angular.isArray(value)) {
//                  return value.length === 0;
//              } else if (angular.isObject(value)) {
//                  for (key in value) {
//                      if (value.hasOwnProperty(key)) {
//                          return false;
//                      }
//                  }
//              }
//              return true;
//          };
//          return {
//              restrict: 'A',
//              require: '?ngModel',
//              priority: 1,
//              link: function (scope, element, attr, ngModel) {
//                  var chosen, defaultText, disableWithMessage, empty, initOrUpdate, match, options, origRender, removeEmptyMessage, startLoading, stopLoading, valuesExpr, viewWatch;
//                  element.addClass('localytics-chosen');
//                  options = scope.$eval(attr.chosen) || {};
//                  angular.forEach(attr, function (value, key) {
//                      if (__indexOf.call(CHOSEN_OPTION_WHITELIST, key) >= 0) {
//                          return options[snakeCase(key)] = scope.$eval(value);
//                      }
//                  });
//                  startLoading = function () {
//                      return element.addClass('loading').attr('disabled', true).trigger('chosen:updated');
//                  };
//                  stopLoading = function () {
//                      return element.removeClass('loading').attr('disabled', false).trigger('chosen:updated');
//                  };
//                  chosen = null;
//                  defaultText = null;
//                  empty = false;
//                  initOrUpdate = function () {
//                      if (chosen) {
//                          return element.trigger('chosen:updated');
//                      } else {
//                          chosen = element.chosen(options).data('chosen');
//                          return defaultText = chosen.default_text;
//                      }
//                  };
//                  removeEmptyMessage = function () {
//                      empty = false;
//                      return element.attr('data-placeholder', defaultText);
//                  };
//                  disableWithMessage = function () {
//                      empty = true;
//                      return element.attr('data-placeholder', chosen.results_none_found).attr('disabled', true).trigger('chosen:updated');
//                  };
//                  if (ngModel) {
//                      origRender = ngModel.$render;
//                      ngModel.$render = function () {
//                          origRender();
//                          return initOrUpdate();
//                      };
//                      if (attr.multiple) {
//                          viewWatch = function () {
//                              return ngModel.$viewValue;
//                          };
//                          scope.$watch(viewWatch, ngModel.$render, true);
//                      }
//                  } else {
//                      initOrUpdate();
//                  }
//                  attr.$observe('disabled', function () {
//                      return element.trigger('chosen:updated');
//                  });
//                  if (attr.ngOptions && ngModel) {
//                      match = attr.ngOptions.match(NG_OPTIONS_REGEXP);
//                      valuesExpr = match[7];
//                      scope.$watchCollection(valuesExpr, function (newVal, oldVal) {
//                          var timer;
//                          return timer = $timeout(function () {
//                              if (angular.isUndefined(newVal)) {
//                                  return startLoading();
//                              } else {
//                                  if (empty) {
//                                      removeEmptyMessage();
//                                  }
//                                  stopLoading();
//                                  if (isEmpty(newVal)) {
//                                      return disableWithMessage();
//                                  }
//                              }
//                          });
//                      });
//                      return scope.$on('$destroy', function (event) {
//                          if (typeof timer !== "undefined" && timer !== null) {
//                              return $timeout.cancel(timer);
//                          }
//                      });
//                  }
//              }
//          };
//      }
//    ]);

//}).call(this);