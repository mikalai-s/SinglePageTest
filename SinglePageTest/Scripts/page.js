﻿// Takes care of loading page modules and history
// Uses History API if it's available

define(
    ["jquery", "knockout", "debug"],
    function ($, ko) {

        var $page = $("#page");

        // checks whether given browser support History API
        function supportsHistoryApi() {
            return history.pushState;
        }

        // initialize current page
        function init() {
            // handle popstate event only if browser supports History API
            if (supportsHistoryApi()) {
                // Hook popstate event in 1 second to prevent Chrome's popstate on page load
                // Ugly but effective
                window.setTimeout(function () {
                    // hook browser's back button click
                    $(window).on('popstate', function () {
                        var module = history.state && history.state.module;
                        if (module) {
                            loadPage(module);
                        }
                        else {
                            history.go();
                        }
                    });
                }, 700);
            }

            // handle click event of all links that have module attribute
            hookSinglePageLinks($(document));

            // initial page load
            //loadPage($page.attr("module"));
        }

        // handle all single page links on the page
        function hookSinglePageLinks(container) {
            // hook single page links only if browser supports History API
            if (supportsHistoryApi()) {
                container.find("a[module]")
                    .on("click", function () {
                        var module = $(this).attr("module");

                        history.pushState({ module: module }, 'Entry', this.href);

                        loadPage(module);

                        return false;
                    });
            }
        }

        function initPage($page, module, html, viewModel) {
            $page.html(html);

            ko.applyBindings(viewModel, document.getElementsByTagName("html")[0]);

            // handle single page link in given template
            hookSinglePageLinks($page);

            // everything is loaded and binded - load module script
            require([module]);
        }

        // load given module into current page
        function loadPage(module) {
            $.ajax({
                url: window.location.pathname,
                type: "get",
                cache: false,
                success: function (data, status, xhr) {
                    var title = xhr.getResponseHeader("page-title") || window.location.pathname,
                        requiresTemplate = xhr.getResponseHeader("requires-template");

                    if (requiresTemplate) {
                        require(["text!" + module + ".htm"], function (template) {
                            initPage($page, module, template, $.extend(true, data, { title: title }));
                        });
                    }
                    else {
                        initPage($page, module, data, { title: title });
                    }
                }
            });
        }

        init();
    }
);
