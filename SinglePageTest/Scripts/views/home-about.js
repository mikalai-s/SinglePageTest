define(
    ["page", "text!views/home-about.htm"],
    function (page, template) {
        return {
            load: function () {
                page.load(template);
            }
        };
    }
);