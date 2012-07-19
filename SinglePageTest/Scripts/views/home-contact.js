define(
    ["text!views/home-contact.htm", "jquery", "knockout", "module"],
    function (template, $, ko, module) {

        $(/*module.config().pageContent*/"#body").html(template);

        $.ajax({
            url: window.location,
            dataType: 'json',
            cache: false,
            success: function (viewModel) {
                ko.applyBindings(viewModel);
            }
        });

        
    }
);