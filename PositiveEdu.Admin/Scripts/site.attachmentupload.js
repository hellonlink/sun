+ function ($) {
    'use strict';

    var attachmentupload = function (element, options) {
        this.jqXHRData;

        this.options = options;
        this.$element = $(element);

        this.$inputfile = this.$element.find('.fup-input');
        this.$btnStart = this.$element.find('.fup-start');
        this.$spanFileName = this.$element.find('.fup-add-name');
        this.$divFile = this.$element.find('.fup-done-file');
        this.$hiddenId = this.$element.find('.fup-done-id');
        this.$progress = this.$element.find('.fup-progress');

        var self = this;

        this.filesCount = 0;

        this.$inputfile.fileupload({
            dataType: 'json',
            singleFileUploads: false,
            limitMultiFileUploads: 10,
            add: function (e, data) {
                self.add(e, data, self);
                self.$progress.css('width', '0%');
            },
            done: function (e, data) {
                self.done(e, data, self);
            },
            fail: function (e, data) {
                self.fail(e, data, self);
            },
            progressall: function (e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                self.$progress.css('width', progress + '%');
            },
        });

        this.$btnStart.on('click', function () {
            if (self.jqXHRData) {
                self.jqXHRData.submit();
            }
            return false;
        });
    }

    attachmentupload.prototype.add = function (e, data, fup) {
        fup.jqXHRData = data;
        var arr = new Array();

        var pass = true;
        var msg = '';

        $(data.files).each(function () {
            //验证大小
            if (fup.options.maxFileSize > 0) {
                var maxBytes = fup.options.maxFileSize * 1024 * 1024;
                if (this.size >= maxBytes) {
                    pass = false;
                    msg = '文件超出大小限制';
                }
            }

            //验证类型
            if (fup.options.fileType.length > 0) {
                var fa = this.name.split('.');
                var ext = fa[fa.length - 1];

                var contains = false
                $(fup.options.fileType).each(function () {
                    if (this.toLowerCase() == ext.toLowerCase())
                        contains = true;
                });

                if (!contains) {
                    pass = false;
                    msg = '文件类型不符合';
                }
            }

            if (fup.options.maxFilesCount != -1) {
                if (fup.filesCount + data.files.length > fup.options.maxFilesCount) {
                    pass = false;
                    msg = '文件数超出限制';
                }
            }

            arr.push(this.name);
        });

        if (pass) {
            var str = arr.join(' | ');
            fup.$spanFileName.html(str);
        }
        else {
            fup.jqXHRData = null;
            fup.$spanFileName.html(msg);
        }
    }

    attachmentupload.prototype.done = function (e, data, fup) {
        if (data.result.isUploaded) {
            fup.loadFile(data.result.content, fup);
        }
        else {
            alert(data.result.message);
        }
    }

    attachmentupload.prototype.fail = function (e, data, fup) {
        if (data.files[0].error) {
            alert(data.files[0].error);
        }
    }

    attachmentupload.prototype.remove = function (event) {
        var fup = event.data;

        var $remove = $(this);
        var $file = $remove.prev().prev();
        var $size = $remove.prev();
        var $holder = $remove.parent();
        $holder.remove();
        //$remove.remove();
        //$file.remove();
        //$size.remove();
        fup.save(fup);
    }

    attachmentupload.prototype.loadFile = function (content, fup) {
        $.each(content, function () {
            var $holder = $('<div></div>');
            var $file = $('<a href="' + this.url + '" target="_blank" class="a-file" style="margin-right:10px;"><i class="glyphicon glyphicon-download-alt"></i> <span>' + this.fileName + '</span></a>');
            $file.data('fup.id', this);
            var $size = $('<span style="margin-right:10px;">(' + (this.size / 1024).toFixed(2) + 'k)</span>');
            var $remove = $('<a href="javascript:;" class="picture-remove text-danger">' + fup.options.linkDeleteFormat + '</a>');

            $remove.bind('click', fup, fup.remove);

            $holder.append($file).append($size).append($remove);
            fup.$divFile.append($holder);
        });
        fup.save(fup);

        fup.jqXHRData = null;
        fup.$spanFileName.html('');
    }

    attachmentupload.prototype.save = function (fup) {
        var arr = new Array();
        fup.$divFile.find('.a-file').each(function () {
            arr.push($(this).data('fup.id'));
        });
        if (arr.length == 0)
            fup.$hiddenId.val('');
        else
            fup.$hiddenId.val(JSON.stringify(arr));

        fup.filesCount = arr.length;
    }

    $.fn.attachmentupload = function (option) {
        return this.each(function () {
            var $this = $(this)
            var data = $this.data('pd.attachmentupload')
            var options = $.extend({}, $.fn.attachmentupload.defaults, $this.data(), typeof option == 'object' && option)

            if (!data)
                $this.data('pd.attachmentupload', (data = new attachmentupload(this, options)));
            if (typeof option == 'string')
                data[option]();
        })
    };

    $.fn.attachmentupload.defaults = {
        linkDeleteFormat: '<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>',
        maxFileSize: -1,    //MB
        fileType: new Array(), //['doc','ppt','xls']
        maxFilesCount: -1,
    };
}(jQuery);