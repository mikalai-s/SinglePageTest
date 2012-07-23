define(
    ["jquery", "knockout", "debug"],
    function ($, ko) {

        // handle all single page links on the page
        function hookSinglePageLinks(container) {
            // hook single page links only if browser supports History API
            if (history.pushState) {
                container.find("a[module]")
                    .on("click", function () {
                        var module = $(this).attr("module");

                        history.pushState({ module: module }, 'Entry', this.href);

                        require([module], function (pageModule) {
                            pageModule.load();
                        });

                        return false;
                    });
            }
        }

        // handle popstate event only if browser supports History API
        if (history.pushState) {
            // Hook popstate event in 1 second to prevent Chrome's popstate on page load
            // Ugly but effective
            window.setTimeout(function () {
                // hook browser's back button click
                $(window).on('popstate', function () {
                    var module = history.state && history.state.module;
                    if (module) {
                        require([module], function (pageModule) {
                            pageModule.load();
                        });
                    }
                });
            }, 700);
        }

        // handle click event of all links that have module attribute
        hookSinglePageLinks($(document));

        return {
            load: function (template) {

                var $body = $("#body").html(template);

                $.ajax({
                    url: window.location,
                    type: "post",
                    dataType: "json",
                    success: function (viewModel) {
                        ko.applyBindings(viewModel, document.getElementsByTagName("html")[0]);

                        // handle single page link in given template
                        hookSinglePageLinks($body);
                    }
                });
            }
        };
    }
);