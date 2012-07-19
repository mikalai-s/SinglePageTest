define(
    ["config", "text!views/home-index.htm", "jquery", "knockout"],
    function (config, template, $, ko) {

        $(config.pageContent).html(template);

        $.ajax({
            url: window.location,
            dataType: 'json',
            cache: false,
            success: function (viewModel) {
                ko.applyBindings(viewModel);
            },
            error: function () {
                debugger;
            }
        });

        
    }
);