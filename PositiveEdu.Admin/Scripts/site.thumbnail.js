+ function ($) {
    'use strict';

    var thumbnail = function (element, options) {
        this.jqXHRData;

        this.options = options;
        this.$element = $(element);

        this.$inputfile = this.$element.find('.tmb-input');
        this.$btnStart = this.$element.find('.tmb-start');
        this.$spanFileName = this.$element.find('.tmb-add-name');
        this.$divThumb = this.$element.find('.tmb-done-thumb');
        this.$hiddenId = this.$element.find('.tmb-done-id');

        var self = this;

        this.$inputfile.fileupload({
            dataType: 'json',
            singleFileUploads: false,
            limitMultiFileUploads: 10,
            add: function (e, data) {
                self.add(e, data, self);
            },
            done: function (e, data) {
                self.done(e, data, self);
            },
            fail: function (e, data) {
                sel.fail(e, data, self);
            }
        });

        this.$btnStart.on('click', function () {
            if (self.jqXHRData) {
                self.jqXHRData.submit();
            }
            return false;
        });
    }

    thumbnail.prototype.add = function (e, data, pup) {
        pup.jqXHRData = data;
        var arr = new Array();
        $(data.files).each(function () {
            arr.push(this.name);
        })
        var str = arr.join(' | ');
        pup.$spanFileName.html(str);
    }

    thumbnail.prototype.done = function (e, data, pup) {
        if (data.result.isUploaded) {
            pup.loadPic(data.result.content, pup);
        }
        else {
            alert(data.result.message);
        }
    }

    thumbnail.prototype.fail = function (e, data, pup) {
        if (data.files[0].error) {
            alert(data.files[0].error);
        }
    }

    thumbnail.prototype.loadPic = function (content, pup) {
        var $thumb = $('<div class="picture-thumb"></div>');
        var $img = $('<img src="' + content + '" alt="..." class="img-thumbnail" />');

        $thumb.append($img);
        pup.$divThumb.empty().append($thumb);

        pup.$hiddenId.val(content);

        pup.jqXHRData = null;
        pup.$spanFileName.html('');

        var $del = $('<a href="javascript:;">删除</a>');
        pup.$divThumb.append($del);

        $del.on("click", null, pup, function (event) {
            pup.$divThumb.empty();
            pup.$hiddenId.val("");
        });
    }

    thumbnail.prototype.save = function (pup) {
    }

    $.fn.thumbnail = function (option) {
        return this.each(function () {
            var $this = $(this)
            var data = $this.data('pd.thumbnail')
            var options = $.extend({}, $.fn.thumbnail.defaults, $this.data(), typeof option == 'object' && option)

            if (!data)
                $this.data('pd.thumbnail', (data = new thumbnail(this, options)));
            if (typeof option == 'string')
                data[option]();
        })
    };

    $.fn.thumbnail.defaults = {
    };
}(jQuery);