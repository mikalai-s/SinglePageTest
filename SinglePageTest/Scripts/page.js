define(
    ["jquery", "knockout"],
    function ($, ko) {

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


        // handle all single page links on the page
        function hookSinglePageLinks() {
            $("a[hook]")
                .removeAttr("hook")
                .on("click", function () {
                    var module = $(this).attr("module");

                    history.pushState({ module: module }, 'Entry', this.href);

                    require([module], function (pageModule) {
                        pageModule.load();
                    });

                    return false;
                });
        }

        return {
            load: function (template) {

                $("#body").html(template);

                $.ajax({
                    url: window.location,
                    type: "post",
                    dataType: "json",
                    success: function (viewModel) {
                        ko.applyBindings(viewModel, document.getElementsByTagName("html")[0]);

                        hookSinglePageLinks();
                    }
                });
            }
        };
    }
);