$(function () {
    var events = [];

    $('.event').each(function (i, ev) {
        var that = $(this);
        events.push({
            id: that.data('id'),
            from: moment(that.data('from')).startOf('day'),
            day: that.data('day'),
            longdate: that.data('longdate'),
            fromforcalendar: that.data('fromforaddthis'),
            toforcalendar: that.data('toforaddthis'),
            month: that.data('month'),
            year: that.data('year'),
            name: that.data('name'),
            body: that.data('body'),
            location: that.data('location'),
            region: that.data('region'),
            userinevent: that.data('userinevent'),
            userapplied: that.data('userapplied'),
            canuserclickadd: that.data('canuserclickadd')
        });
    });

    addthisevent.settings({
        license: "00000000000000000000",
        mouse: false,
        css: true,
        google: { show: true, text: "Google <em>(online)</em>" },
        yahoo: { show: true, text: "Yahoo <em>(online)</em>" },
        outlookcom: { show: true, text: "Outlook.com <em>(online)</em>" },
        appleical: { show: true, text: "Apple Calendar" },
        dropdown: { order: "outlook,google,appleical" },
        callback: ['AttendEvent()']
    });

    $('#inlineCalendar').datepicker({
        dayNamesMin: ['S', 'M', 'T', 'W', 'T', 'F', 'S'],
        monthNames: [
            "Jan", "Feb", "Mar", "Apr", "May", "Jun",
            "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
        ],
        beforeShowDay: function (date) {

            var dateToCheck = moment(date).startOf('day');
            var event = [false, "", ""];

            for (var i = 0; i < events.length; i++) {

                //convert event date to 'moment', remove time and use date only
                var momentFrom = moment(events[i].from).startOf('day');

                var isEventDay = (dateToCheck.isSame(momentFrom));
                if (isEventDay) {
                    //if already attending event
                    if (events[i].userinevent === 'True') {
                        event = [true, "inEvent", 'attending'];
                    }
                        //if already applied to event
                    else if (events[i].userapplied === 'True') {
                        event = [true, "appliedToEvent", 'awaiting confirmation'];
                    } else {
                        event = [true, "hasEvent", 'view event'];
                    }
                    break;
                }
            }
            return event;
        },

        //on calendar day click, check if there is an event
        onSelect: function (dateText) {
            var date,
                selectedDate = new Date(dateText),
                i = 0,
                event = null;

            //Determine if the user clicked an event
            while (i < events.length && !event) {
                date = events[i].from;

                if (selectedDate.valueOf() === date.valueOf()) {
                    event = events[i];
                }
                i++;
            }
            //if there is an event on selected day
            if (event) {
                $('.eventTitle').text(event.name);
                $('.eventDateTop').text(event.longdate);
                $('.eventDescription').text(event.body);

                $('.eventDay').text(event.day);
                $('.eventMonth').text(event.month);
                $('.eventYear').text(event.year);

                //fields for add to calendar
                $('.start').text(event.fromforcalendar);
                $('.end').text(event.toforcalendar);
                $('.title').text(event.name);

                $('.description').text(event.body);
                $('.location').text(event.location);
                $('.date_format').text("DD/MM/YYYY");

                //hidden storage fields based for selected event
                $('.eventIdCalendar').text(event.id);
                $('.eventRegion').text(event.region);
                $('.canUserClickAdd').text(event.canuserclickadd);
            }
        }
    });

    //initialize addthisevent with the first event
    var defaultEvent = events[0];
    if (defaultEvent > 0) {
        $('.start').text(defaultEvent.fromforcalendar);
        $('.end').text(defaultEvent.toforcalendar);
        $('.title').text(defaultEvent.name);
        $('.description').text(defaultEvent.body);
        $('.location').text(defaultEvent.location);
        $('.region').text(defaultEvent.region);
        $('.date_format').text("DD/MM/YYYY");
        $('.eventIdCalendar').text(defaultEvent.id);
        $('.eventRegion').text(defaultEvent.region);
        $('.canUserClickAdd').text(defaultEvent.canuserclickadd);
    }

    $('.addEventToCalendar').click(function () {
        var $this = $(this);
        $("#background").css({
            "opacity": "0.3"
        }).fadeIn("fast");

        $("#ttipContainer").html(function () {

            $('.ttip').css({
                //left: $this.position() + '20px',
                //top: $this.position() + '50px'
            }).show(100);

        }).fadeIn("fast");
    });

    $('.addEventContainer').on('click', function () {
        //if the user isnt logged in redirect to login
        if (!$('#userId').data('userid')) {
            window.location.href = "/Account/Login";
            return;
        }

        //if the user isn't in the same region as the event then show apply button
        if ($('.canUserClickAdd').text() === 'True') {

            if ($('.eventRegion').text() !== $('#userRegion').data('userregion')) {
                $('.contents').hide();
                $('#cannotApplyContainer').hide();
                $('#applyContainer').show();
            } else {
                $('#applyContainer').hide();
                $('#cannotApplyContainer').hide();
                $('.contents').show();
            }
        } else {
            $('#applyContainer').hide();
            $('.contents').hide();
            $('#cannotApplyContainer').show();
        }
    });

    $('.close').on('click', function () {
        $('.ttip').hide(100);
        $("#background").fadeOut("fast");
        $("#ttipContainer").fadeOut("fast");
    });

    $('#applyButton').on('click', function () {
        AttendEvent(true);
    });
});