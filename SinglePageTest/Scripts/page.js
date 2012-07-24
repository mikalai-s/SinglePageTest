// Takes care of loading page modules and history
// Uses History API if it's available

define(
    ["jquery", "knockout", "debug"],
    function ($, ko) {

        var $body = $("#body");

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
                    });
                }, 700);
            }

            // handle click event of all links that have module attribute
            hookSinglePageLinks($(document));

            // initial page load
            loadPage($body.attr("module"));
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

        // load given module into current page
        function loadPage(module) {
            require(
                ["text!" + module + ".htm"],
                function(template) {
                    $body.html(template);

                    $.ajax({
                        url: window.location,
                        type: "post",
                        dataType: "json",
                        success: function (viewModel) {
                            ko.applyBindings(viewModel, document.getElementsByTagName("html")[0]);

                            // handle single page link in given template
                            hookSinglePageLinks($body);

                            // everything is loaded and binded - load module script
                            require([module]);
                        }
                    });
                });
        }

        init();
    }
);