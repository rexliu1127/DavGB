TableThing = function (TableID) {
    settings = {
        table: $('#' + TableID),
        thead: []
    };

    this.fixThead = function () {
        // empty our array to begin with
        settings.thead = [];
        // loop over the  row of td's in thead and get the widths of individual
        $('thead th', settings.table).each(function (i, v) {
            settings.thead.push($(v).width());
        });

        // now loop over our array setting the widths we've got to the 
//        for (i = 0; i < settings.thead.length; i++) {
//            $('thead th:eq(' + i + ')', settings.table).width(settings.thead[i]);
//        }

        // here we attach to the scroll, adding the class 'fixed' to the thead 
        $(window).scroll(function () {
            var windowTop = $(window).scrollTop();

            if (windowTop > settings.table.offset().top) {
                $("thead", settings.table).addClass("fixed");
            }
            else {
                $("thead", settings.table).removeClass("fixed");
            }
        });
    }
}
