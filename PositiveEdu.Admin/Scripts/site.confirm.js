+ function ($) {
    'use strict';

    var confirm = function (element, options) {
        this.options = options;
        this.$element = $(element);
        this.$modalDialog = $('#' + this.options.modalDialogId);
        this.$modalBody = this.$modalDialog.find('.modal-body');
        this.$modalTitle = this.$modalDialog.find('.modal-title');
        this.$modalConfirm = this.$modalDialog.find('.confirm-do');

        if (this.options.jsonType == 1)
        {
            var json = this.$element.next().val();
            var data = JSON.parse(json);
            if (this.options.url == '')
                this.options.url = data.url;

            if (this.options.title == '')
                this.options.title = data.title;

            if (this.options.content == '')
                this.options.content = data.content;
        }

        this.$element.bind('click', this, this.open);
    }

    confirm.prototype.open = function (event) {
        var cfm = event.data;

        cfm.$modalTitle.html(cfm.options.title);
        cfm.$modalBody.html(cfm.options.content);
        cfm.$modalConfirm.attr('href', cfm.options.url);
        cfm.$modalDialog.modal('show');
    }

    $.fn.confirm = function (option) {
        return this.each(function () {
            var $this = $(this)
            var data = $this.data('pd.confirm')
            var options = $.extend({}, $.fn.confirm.defaults, $this.data(), typeof option == 'object' && option)

            if (!data)
                $this.data('pd.confirm', (data = new confirm(this, options)));
            if (typeof option == 'string')
                data[option]();
        })
    };

    $.fn.confirm.defaults = {
        modalDialogId: 'ModalConfirm',
        title: '',
        content: '',
        url: '',
        jsonType: 1,
    };
}(jQuery);

$(function () {
    $('.confirm-btn').confirm();
});
