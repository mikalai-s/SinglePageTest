define(['knockout'], function(ko) {
    ko.bindingHandlers.viewer = {
        init: function(element, valueAccessor) {
            var win,
                updateText = function(content) {
                    var el = win.document.getElementById("output");
                    el['innerText' in el ? 'innerText' : 'textContent'] = content;
                };

            ko.utils.registerEventHandler(element, "click", function(event) {
                win = win || window.open();
                win.document.writeln("<pre id='output'></pre>");
                updateText(ko.toJSON(valueAccessor(), null, 2));
            });

            //set up a throttled computed observable to handle updates
            ko.computed({
                read: function() {
                    //make sure we have a subscription to all observables in valueAccessor()
                    var content = ko.toJSON(valueAccessor(), null, 2);
                    if (win) {
                        if (!win.closed) {
                            updateText(content);
                        } else {
                            //clear it out
                            win = null;
                        }
                    }
                },
                disposeWhenNodeIsRemoved: element
            }).extend({ throttle: 1 });
        }
    };
});