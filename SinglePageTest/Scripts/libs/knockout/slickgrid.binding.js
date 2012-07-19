define(['jquery', 'knockout'], function ($, ko) {
    ko.bindingHandlers.slickGrid = {
        init: function (element, valueAccessor) {
            debug.time('bind slickgrid');

            var settings = valueAccessor();
            var columns = ko.utils.unwrapObservable(settings.columns);
            var options = ko.utils.unwrapObservable(settings.options) || {};
            var data = ko.utils.unwrapObservable(settings.data);

            var grid = new Slick.Grid(element, data, columns, options);

            $(window).resize(function () {
                grid.resizeCanvas();
            });

            ko.utils.domData.set(element, 'grid', grid);
        },
        update: function (element, valueAccessor, allBindingAccessor, viewModel) {
            var settings = valueAccessor();
            var data = ko.utils.unwrapObservable(settings.data);
            var grid = ko.utils.domData.get(element, 'grid');
            debug.log('update slickgrid ' + data.length + ' rows');
            grid.setData(data);
            grid.render();
            debug.timeEnd('bind slickgrid');
        }
    };
});