define(
    ["page", "text!views/home-contact.htm"],
    function (page, template) {
        return {
            load: function () {
                page.load(template);
            }
        };
    }
);