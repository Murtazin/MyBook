;
(function($) {
    'use strict';
    $.HSCore.components.HSSelectPicker = {
        _baseConfig: {},
        pageCollection: $(),
        init: function(selector, config) {
            this.collection = selector && $(selector).length ? $(selector) : $();
            if (!$(selector).length) return;
            this.config = config && $.isPlainObject(config) ? $.extend({}, this._baseConfig, config) : this._baseConfig;
            this.config.itemSelector = selector;
            this.initSelectPicker();
            return this.pageCollection;
        },
        initSelectPicker: function() {
            var $self = this,
                collection = $self.pageCollection;
            this.collection.each(function(i, el) {
                var $this = $(el);
                $this.selectpicker();
                $this.on('loaded.bs.select', function(e) {
                    var $searchbox = $this.siblings('.dropdown-menu ').find('.bs-searchbox'),
                        searchBoxClasses = $this.data('searchbox-classes');
                    if (!searchBoxClasses) return;
                    $searchbox.addClass(searchBoxClasses);
                });
                collection = collection.add($this);
            });
        }
    };
})(jQuery);