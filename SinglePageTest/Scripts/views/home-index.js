define(
    ["page", "text!views/home-index.htm"],
    function (page, template) {
        return {
            load: function () {
                page.load(template);
            }
        };
    }
);