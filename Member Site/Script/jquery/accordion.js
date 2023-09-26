
var accordion = true;
var closedCss = 'subnav_close';
varopenedCss = 'subnav_open';
function SetAccordion(ObjectID,ItemID, speed) {
    if ($('#' + ItemID).parent().find("ul").size() != 0) {
        if (accordion) {
            //Do nothing when the list is open
            if (!$('#' + ItemID).parent().find("ul").is(':visible')) {
                parents = $('#' + ItemID).parent().parents("ul");
                visible = $('#' + ObjectID).find("ul:visible");
                visible.each(function (visibleIndex) {
                    var close = true;
                    parents.each(function (parentIndex) {
                        if (parents[parentIndex] == visible[visibleIndex]) {
                            close = false;
                            return false;
                        }
                    });
                    if (close) {
                        if ($('#' + ItemID).parent().find("ul") != visible[visibleIndex]) {
                            $(visible[visibleIndex]).slideUp(speed, function () {

                                if ($('#' + ItemID).find('li').html() != null) {
                                    $('#' + ItemID).parent("li").find("a:first").attr('class', closedCss);
                                }
                            });

                        }
                    }
                });
            }


        }


        if ($('#' + ItemID).parent().find("ul:first").is(":visible")) {

//            $('#' + ItemID).parent().find("ul:first").slideUp(speed, function () {
//                if ($('#' + ItemID).find('li').html() != null) {
//                    $('#' + ItemID).parent("li").find("a:first").delay(speed).attr('class', closedCss);
//                }
//             });


        } else {
            $('#' + ItemID).parent().find("ul:first").slideDown(speed, function () {
                if ($('#' + ItemID).find('li').html() != null) {
                    $('#' + ItemID).parent("li").find("a:first").delay(speed).attr('class', openedCss);
                }
            });

        }

    }
}


function SetCrabwiseMenu(UlID, MaskID, MaskImg) {

    var _menu = $('#' + UlID + ' li>a'),
			_target = $(_menu).filter('.selected'),
			_menuMask = $('#' + MaskID),
			_maskHeight = 0,
			_diffHeight = 3,
			_maskWidth = 0,
			_diffWidth = 0;

    if (_target.length <= 0) {
        _target = $(_menu).eq(0);
    }

    _menu.each(function () {
        $('<img />').attr('src', MaskImg).hide().appendTo('body');
    });

    _menu.click(function () {

        $('.selected').removeClass('selected');

        var _this = $(this).addClass('selected'),
				_link = _this.attr('href');
        _menuMask.css({
            backgroundImage: 'url(' + MaskImg + ')'

        }).stop().animate({
            width: $(this).width() + _maskWidth,
            height: $(this).height() + _maskHeight,
            top: $(this).offset().top - _diffHeight,
            left: $(this).offset().left - _diffWidth
        }, function () {
            $('#' + MaskID).html(_this.text());
            if (!!_link) {
                var _targetAttr = _this.attr('target');
                if (!!_targetAttr) {
                    if (_targetAttr.toLowerCase() == '_blank') {
                        open(_link);
                    } else {
                        $('#' + _targetAttr).attr('src', _link);
                    }
                } else {
                    location = _link;
                }
            }
        });

        return false;
    }).focus(function () {
        $(this).blur();
    });

    $(window).resize(function () {
        moveMenu($(_menu).filter('.selected'));
    });

    function moveMenu(obj) {
        _menuMask.css({
            width: obj.width() + _maskWidth,
            height: obj.height() + _maskHeight,
            top: obj.offset().top - _diffHeight,
            left: obj.offset().left - _diffWidth,
            backgroundImage: 'url(' + MaskImg + ')'

        });
        $('#' + MaskID).html(obj.text());

    }
    moveMenu(_target);

}