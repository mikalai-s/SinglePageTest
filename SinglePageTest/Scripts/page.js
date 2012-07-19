define(
    ["jquery", "knockout"],
    function ($, ko) {
        return {
            load: function (template) {
                $("#body").html(template);

                $.ajax({
                    url: window.location,
                    dataType: 'json',
                    cache: false,
                    success: function (viewModel) {
                        ko.applyBindings(viewModel, document.getElementsByTagName("html")[0]);
                    }
                });
            }
        };
    }
);