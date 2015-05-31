
var plan = {
    renderer: null,

    init: function (r) {
        renderer = r;
        plan.listItemClick();
        plan.listItemHover();

        //$(canvas).mouseup(handler.up);
        //renderer.initMouseHandling.click(plan.listItemClick);
        $(renderer).mouseup(plan.listItemClick);
    },

    listItemClick: function () {
        $('document').ready(function () {
            $('li.servicelistitem').click(function () {
                var liId = this.id;
                var result = plan.listUpdate(liId);

                // *** Updating progress bar *** //
                var onehundredpercentofprogressbar = 100 / (window.services.length - 1);
                var barprogress = window.services.length * onehundredpercentofprogressbar;
                $('#pb').progressbar({ value: barprogress });

                // Update how many services the user has selected
                $('#haveselected').html("I have chosen " + window.services.length + " out of " + (window.services.length - 1) + " services");

                renderer.toggleNode(window.services[result]);
            })
        });
    },

    listItemHover: function () {
        $('document').ready(function () {
            $('li.servicelistitem').hover(function () {
                var liId = this.id;
                for (i = 0; i < window.services.length; i++) {
                    if (window.services[i].name == liId) {
                        num = i;
                        $('#sname').html(window.services[i].label);
                        $('#sdescription').html(window.services[i].desc);
                        si = document.getElementById('sconnected')
                        si.innerHTML = '';
                        for (j = 0; j < window.services[i].connected.length; j++) {
                            si.innerHTML += window.services[i].connected[j] + '<br/>'
                        };
                        $('#serviceimage').attr('src', window.services[i].imageurl);
                    }
                }
            })
        });
    },

    listUpdate: function (liId) {
        var num;
        for (i = 0; i < window.services.length; i++) {
            if (window.services[i].name == liId) {
                num = i;
                if (window.services[i].selected == false) {
                    $('#sname').html(window.services[i].label);
                    $('#sdescription').html(window.services[i].desc);
                    si = document.getElementById('sconnected')
                    si.innerHTML = '';
                    for (j = 0; j < window.services[i].connected.length; j++) {
                        si.innerHTML += window.services[i].connected[j] + '<br/>'
                    };

                    // Change CSS properties for selected or deselected list items
                    $('#' + window.services[i].name).removeClass("servicelistitem");
                    $('#' + window.services[i].name).addClass("selecteditem");
                    $('a').css("text-decoration", "none");
                    $('a:visited:active').css("text-decoration", "none");
                }
                else {
                    $('#' + window.services[i].name).removeClass("selecteditem");
                    $('#' + window.services[i].name).addClass("servicelistitem");
                }
            }
        }
        return num;
    }
}
