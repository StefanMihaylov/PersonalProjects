'use strict';
var main = {};
var constants = {};
$(document).ready(function () {
    constants = (function () {
        return {
            BLOCK_UI_DURATION: 0
        };
    })();

    main = (function () {
        var getUrl = function (urlExtension) {
            var url = $('.serverUrl').data('url'),
                newUrl;

            if (url.slice(-1) != '/') {
                url += '/';
            }

            newUrl = url + urlExtension;
            return newUrl;
        }

        var changeUrlId = function (url, method, id, separator) {
            separator = separator || '/';
            var lastIndex = url.lastIndexOf(method);
            var end = lastIndex + method.length;
            var newUrl = url.substr(0, end);

            if (id) {
                newUrl = newUrl + separator + id;
            }

            return newUrl;
        }

        // block UI
        $('.block-ui-btn').on('click', function () {
            $('.loader-gr-fixed-backgraund').show(constants.BLOCK_UI_DURATION);
        });

        var indexPageInitialization = function () {
            resizeMainContainer('.scrollable-content');
            $(window).resize(function () {
                resizeMainContainer('.scrollable-content');
            })
        };

        return {
            getUrl: getUrl,
            indexPageInitialization: indexPageInitialization,
            changeUrlId: changeUrlId
        };

        function resizeMainContainer(container) {
            var bodyHeight = parseFloat($('body').height()),
                bodyWidth = parseFloat($('body').width());

            var height = '';
            var childHeight = '';
            var mapHeight = 200;
            if (bodyWidth > 992) {
                height = bodyHeight - 25;
                childHeight = height - 2;
                mapHeight = childHeight - 80;
            }

            $(container).height(height);
            $(container + '-child').height(childHeight);
            $(container + '-child-google-maps').height(mapHeight);
        }
    })();

    //var errorLogger = (function attachEvent() {
    //    window.onerror = function (errorMsg, url, lineNumber, column, errorObj) {
    //        var encodedStack;
    //        if (errorObj) {
    //            var stack = errorObj.hasOwnProperty('stack') ? errorObj.stack : errorObj;
    //            encodedStack = $('<div/>').text(stack.toString()).html();
    //        }

    //        var ajaxData = {
    //            errorMessage: errorMsg,
    //            scriptUrl: url,
    //            lineNumber: lineNumber,
    //            columnNumber: column,
    //            stackTrace: encodedStack,
    //            browserUserAgent: window.navigator.userAgent
    //        };

    //        var checkPrinterUrl = main.getUrl('ClientError/Write');
    //        if (window.XMLHttpRequest) {
    //            var xhr = new XMLHttpRequest();

    //            // serialize the POST params
    //            var params = 'errorMessage=' + ajaxData.errorMessage + '&scriptUrl=' + ajaxData.scriptUrl + '&lineNumber=' + ajaxData.lineNumber +
    //                '&columnNumber=' + ajaxData.columnNumber + '&stackTrace=' + encodeURIComponent(ajaxData.stackTrace) +
    //                '&browserUserAgent=' + ajaxData.browserUserAgent;

    //            // open an asynchronous connection
    //            xhr.open("POST", checkPrinterUrl, true);
    //            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    //            xhr.send(params);
    //        }

    //        // returning false triggers the execution of the built-in error handler
    //        return false;
    //    }
    //})();

    // prevent redirect when clicking disabled anchors(main menu buttons)
    $('.btn-group').on('click', 'a', function (e) {
        if ($(this).closest('li').hasClass('disabled')) {
            e.preventDefault();
        }
    });

    //START disable refersh and allow it after 10 seconds
    //var $refresh = $('#refresh');

    //$(document).on('keydown', function (ev) {
    //    if (ev.keyCode === 116 && $refresh.hasClass('refresh-denied')) {
    //        ev.preventDefault();
    //        return false;
    //    }
    //});

    //setTimeout(function () {
    //    $refresh.removeClass('refresh-denied')
    //            .addClass('refresh-allowed');
    //}, 10000);
    //END disable refersh and allow it after 10 seconds

    // START search input smooth resize logic
    $('#search').on('focus', function () {
        var $this = $(this);
        var $parent = $this.parent();
        $parent.animate({
            width: '300px',
        }, 1000);

        $parent.addClass('focused-search');
    });

    $('#search').on('focusout', function () {
        var $this = $(this);
        var $parent = $this.parent();
        $parent.animate({
            width: '160px'
        }, 1000);

        $parent.removeClass('focused-search');
    });
    // END search input smooth resize logic

    // START menu logic
    $(".side-nav a[href!='#']").on('click', function () {
        var expandedIndexes = [];

        var $currentLi = $(this).parent();
        var $currentUl = $currentLi.parent();
        var liIndex;
        var ulParentIndex;
        var isMainUl = $currentUl.hasClass('side-nav');

        while (!isMainUl) {
            liIndex = $currentLi.index();
            ulParentIndex = $currentUl.index();

            expandedIndexes.push({
                liIndex: liIndex,
                liText: $currentLi.find('a:first').text(),
                ulParentIndex: ulParentIndex,
            });

            $currentLi = $currentUl.parent();
            $currentUl = $currentLi.parent();
            isMainUl = $currentUl.hasClass('side-nav');
        }

        expandedIndexes.push({ liIndex: $currentLi.index(), liText: $currentLi.find('a:first').find('span:first').text() });

        sessionStorage.setItem('menuSelectedLis', JSON.stringify(expandedIndexes));
    });

    $('.logo-anchor').on('click', function () {
        sessionStorage.removeItem('menuSelectedLis');
    });

    $('.logout-btn').on('click', function () {
        sessionStorage.removeItem('menuSelectedLis');
    });

    (function restoreMenuSelection() {
        var expandedIndexes = sessionStorage.getItem('menuSelectedLis');
        var breadcrumbs = [];
        if (expandedIndexes !== null) {
            expandedIndexes = JSON.parse(expandedIndexes);

            var $currentLi;
            var $currentUl = $('#side-nav');

            for (var i = expandedIndexes.length - 1; i >= 0 ; i--) {
                if (i === (expandedIndexes.length - 1)) {
                    $currentUl = $('#side-nav');
                    breadcrumbs.push(expandedIndexes[i].liText);
                } else {
                    $currentUl = $currentLi.children().eq(expandedIndexes[i].ulParentIndex);
                    $currentUl.addClass('in');
                    breadcrumbs.push(expandedIndexes[i].liText);
                }

                $currentLi = $currentUl.children().eq(expandedIndexes[i].liIndex);
                $currentLi.addClass('active');
            }

            populateBreadcrumbs(breadcrumbs);
        }
    }());

    function populateBreadcrumbs(breadcrumbsLiArray) {
        var currentPageHeading = $('#current-page-heading');

        for (var i = 0; i < breadcrumbsLiArray.length; i++) {
            var li = $('<li>');
            var $anchor = $('<a>');
            var currentBreadcrumb = breadcrumbsLiArray[i].trim();
            $anchor.text(currentBreadcrumb)
                   .attr('href', '#');
            li.append($anchor);

            if (i === breadcrumbsLiArray.length - 1) {
                li.addClass('active');
                $('#current-page-heading').text(currentBreadcrumb);
            }

            $('#breadcrumb').append(li);
        }
    }
    // END menu logic

    // START search
    $('#search').on('keypress', function (ev) {
        if (ev.keyCode === 13) {
            search();
        }
    });

    $('#search-btn').on('click', function () {
        if ($('#search').val()) {
            search();
        }
    });

    function search() {
        var query = $("#search").val();
        var url = main.getUrl('Mi6/Search/GetSearchResults') + '?query=' + query;
        window.location.href = url;
    }
    // END search

    // START logo resize
    $('#sidebar-minimalizer').on('click', function () {
        var $wrapper = $('#wrapper');
        var $vivacomLogo = $('#vivacom-logo');
        $vivacomLogo.toggleClass('mini-logo');
        //if ($wrapper.hasClass('mini-sidebar')) {
        //    $vivacomLogo.addClass('mini-logo');

        //} else {
        //    $vivacomLogo.removeClass('mini-logo');
        //}
    });
    // END logo resize
});