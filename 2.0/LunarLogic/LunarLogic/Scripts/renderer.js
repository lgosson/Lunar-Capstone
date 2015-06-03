(function () {
    window.services = [];

    Renderer = function (canvas) {
        canvas = $(canvas).get(0);
        var ctx = canvas.getContext("2d");
        var gfx = arbor.Graphics(canvas);
        var particleSystem = null;

        var cWidth = canvas.width = window.innerWidth;
        var cHeight = canvas.height = window.innerHeight;

        var that = {
            init: function (system, result) {
                if (particleSystem == null) {
                    particleSystem = system;
                    particleSystem.screenSize(canvas.width, canvas.height);
                    particleSystem.screenPadding(40);

                    //node data
                    for (i = 0; i < result.length; i++) {
                        var s = new service();
                        s.name = result[i].ID;
                        s.label = result[i].Name;
                        s.desc = result[i].Description;
                        s.selectable = result[i].Selectable;
                        s.selected = false;
                        //s.hovered = false;
                        s.connected = result[i].ConnectedServices;
                        s.parent = result[i].ParentService;
                        s.color = 'red';
                        s.shape = 'dot';
                        s.imageurl = result[i].ImageURL;

                        window.services.push(s);
                    }
                    haveSelected(window.services);
                    //listSetup(window.services);

                    that.initMouseHandling();
                    that.listItemHover();
                    that.listItemClick();

                    $(window).resize(that.windowsized);
                    that.windowsized();
                }
            },

            windowsized: function () {
                var c = document.getElementById('container');
                var w = c.offsetWidth;
                var h = c.offsetHeight;

                canvas.width = w;
                canvas.height = h;
                particleSystem.screenSize(w, h);
                that.redraw();
            },

            graphDraw: function (result) {
                var display = [];

                for (i = 0; i < result.length; i++) {
                    if (result[i].selectable == false || result[i].selected == true || result[i].hovered == true) {
                        display.push(result[i]);
                        for (j = 0; j < result[i].connected.length; j++) {
                            for (k = 0; k < result.length; k++) {
                                if (result[k].name == result[i].connected[j]) {
                                    display.push(result[k]);
                                }
                            }
                        }
                    }
                }

                for (i = 0; i < display.length; i++) {
                    if (display[i].hovered == true) {
                        var nd = particleSystem.getNode(display[i].name);
                    }
                }

                var newNodeNames = [];

                for (i = 0; i < display.length; i++) {
                    if (particleSystem.getNode(display[i].name) == null) {
                        particleSystem.addNode(display[i].name, {
                            name: display[i].name,
                            label: display[i].label,
                            'desc': display[i].desc,
                            'selectable': display[i].selectable,
                            'selected': display[i].selected,
                            //'hovered': display[i].hovered,
                            'connected': display[i].connected,
                            //'parent': display[i].parent,
                            'color': display[i].color,
                            'shape': display[i].shape,
                            'imageurl': display[i].imageurl
                        });
                    }
                }

                particleSystem.eachNode(function (node, pt) {
                    var remove = true;

                    for (i = 0; i < display.length; i++) {
                        if (node.name == display[i].name) remove = false;
                    }

                    if (remove) {
                        var edges = particleSystem.getEdgesTo(node);
                        for (e = 0; e < e.length; e++) {
                            particleSystem.pruneEdge(edges[e]);
                        }
                        particleSystem.pruneNode(node);
                    }
                });

                for (i = 0; i < display.length; i++) {
                    for (j = 0; j < display[i].connected.length; j++) {
                        for (k = 0; k < display.length; k++) {
                            if (display[k].name == display[i].connected[j])
                                particleSystem.addEdge(display[i].name, display[i].connected[j]);
                        }
                    }
                }

                that.updateNodes();
                //particleSystem.graft();
            },

            updateNodes: function () {
                particleSystem.eachNode(function (node, pt) {
                    for (i = 0; i < window.services.length; i++) {
                        if (node.data.name == window.services[i].name) {
                            node.data.selected = window.services[i].selected;

                            // Changes color of node
                            if (node.data.selected == true) {
                                node.data.color = 'blue';
                            }
                            else
                                node.data.color = 'red';
                        }
                    }
                });
            },

            redraw: function () {
                if (!particleSystem) return

                //ctx.fillRect(0, 0, canvas.width, canvas.height);
                gfx.clear() // convenience ƒ: clears the whole canvas rect

                // draw the nodes & save their bounds for edge drawing
                var nodeBoxes = {}
                particleSystem.eachNode(function (node, pt) {
                    // node: {mass:#, p:{x,y}, name:"", data:{}}
                    // pt:   {x:#, y:#}  node position in screen coords

                    // determine the box size and round off the coords if we'll be 
                    // drawing a text label (awful alignment jitter otherwise...)
                    var label = node.data.label || ""
                    var w = ctx.measureText("" + label).width + 10
                    if (!("" + label).match(/^[ \t]*$/)) {
                        pt.x = Math.floor(pt.x)
                        pt.y = Math.floor(pt.y)
                    } else {
                        label = null
                    }

                    // draw a rectangle centered at pt
                    if (node.data.color) ctx.fillStyle = node.data.color
                    if (node.data.selected === false) ctx.fillStyle = "rgba(0,0,0,.2)"
                    else ctx.fillStyle = "#97233F";
                    if (node.data.color == 'none') ctx.fillStyle = "white"

                    if (node.data.shape == 'dot') {
                        gfx.oval(pt.x - w / 2, pt.y - w / 2, w, w, { fill: ctx.fillStyle })
                        nodeBoxes[node.name] = [pt.x - w / 2, pt.y - w / 2, w, w]
                    } else {
                        gfx.rect(pt.x - w / 2, pt.y - 10, w, 20, 4, { fill: ctx.fillStyle })
                        nodeBoxes[node.name] = [pt.x - w / 2, pt.y - 11, w, 22]
                    }

                    // draw the text
                    if (label) {
                        ctx.font = "12px Helvetica"
                        ctx.textAlign = "center"
                        ctx.fillStyle = "white"
                        if (node.data.color == 'none') ctx.fillStyle = '#333333'
                        ctx.fillText(label || "", pt.x, pt.y + 4)
                        ctx.fillText(label || "", pt.x, pt.y + 4)
                    }
                })

                // draw the edges
                particleSystem.eachEdge(function (edge, pt1, pt2) {
                    // edge: {source:Node, target:Node, length:#, data:{}}
                    // pt1:  {x:#, y:#}  source position in screen coords
                    // pt2:  {x:#, y:#}  target position in screen coords

                    var weight = edge.data.weight
                    var color = edge.data.color

                    if (!color || ("" + color).match(/^[ \t]*$/)) color = null

                    // find the start point
                    var tail = intersect_line_box(pt1, pt2, nodeBoxes[edge.source.name])
                    var head = intersect_line_box(tail, pt2, nodeBoxes[edge.target.name])

                    ctx.save()
                    ctx.beginPath()
                    ctx.lineWidth = (!isNaN(weight)) ? parseFloat(weight) : 3
                    ctx.strokeStyle = (color) ? color : "#cccccc"
                    ctx.fillStyle = null

                    ctx.moveTo(tail.x, tail.y)
                    ctx.lineTo(head.x, head.y)
                    ctx.stroke()
                    ctx.restore()

                    // draw an arrowhead if this is a -> style edge
                    if (edge.data.directed) {
                        ctx.save()
                        // move to the head position of the edge we just drew
                        var wt = !isNaN(weight) ? parseFloat(weight) : 1
                        var arrowLength = 6 + wt
                        var arrowWidth = 2 + wt
                        ctx.fillStyle = (color) ? color : "#cccccc"
                        ctx.translate(head.x, head.y);
                        ctx.rotate(Math.atan2(head.y - tail.y, head.x - tail.x));

                        // delete some of the edge that's already there (so the point isn't hidden)
                        ctx.clearRect(-arrowLength / 2, -wt / 2, arrowLength / 2, wt)

                        // draw the chevron
                        ctx.beginPath();
                        ctx.moveTo(-arrowLength, arrowWidth);
                        ctx.lineTo(0, 0);
                        ctx.lineTo(-arrowLength, -arrowWidth);
                        ctx.lineTo(-arrowLength * 0.8, -0);
                        ctx.closePath();
                        ctx.fill();
                        ctx.restore()
                    }
                })
            },

            initMouseHandling: function () {
                // no-nonsense drag and drop (thanks springy.js)
                selected = null;
                nearest = null;
                var dragged = null;
                var wasDragged = false;
                var oldmass = 1;
                var hovered = false;
                var oldSelected = null;
                //TODO: Add another hover bool that is for a radius AROUND the node (for triggering reveal of node's connections).
                var hvrTol = 150;
                var dragTol = 20;
                var oldDown = null;

                // set up a handler object that will initially listen for mousedowns then
                // for moves and mouseups while dragging
                var handler = {
                    calcMousePos: function (e) {
                        var pos = $(canvas).offset();
                        _mouseP = arbor.Point(e.pageX - pos.left, e.pageY - pos.top);
                        selected = nearest = dragged = particleSystem.nearest(_mouseP);
                        return _mouseP;
                    },

                    clicked: function (e) {
                        handler.judgeHover(e);
                        if (!hovered) return;

                        if (dragged.node !== null) dragged.node.fixed = true;

                        if (selected.node.data.selectable == true) {
                            that.listUpdate(selected.node.name);
                            that.toggleNode(selected);
                        }
                    },

                    down: function (e) {
                        $(canvas).bind('mousemove', handler.dragged);
                        $(window).bind('mouseup', handler.dropped);
                        oldDown = handler.calcMousePos(e);
                    },

                    up: function (e) {
                        if (!wasDragged) {
                            handler.clicked(e);
                        }
                        wasDragged = false;
                    },

                    moved: function (e) {
                        handler.judgeHover(e);
                    },

                    judgeHover: function (e) {
                        handler.calcMousePos(e);
                        if (oldSelected === null) oldSelected = { node: null };
                        if (selected != null && selected.distance < hvrTol) {
                            hovered = true;
                            if (selected.node != oldSelected.node) {
                                handler.hover(e);
                                oldSelected = selected;
                            }
                        }
                        else hovered = false;
                    },

                    sidebarOpen: false,

                    hover: function (e) {
                        for (i = 0; i < window.services.length; i++) {
                            if (window.services[i].name == selected.node.name) {
                                window.services[i].hovered = true;
                            }
                            else {
                                window.services[i].hovered = false;
                            }
                        }

                        $('#sname').html(selected.node.data.label); // Updates service information partial view
                        $('#sdescription').html(selected.node.data.desc);
                        si = document.getElementById('sconnected')
                        si.innerHTML = '';
                        for (var i = 0; i < selected.node.data.connected.length; i++) {
                            si.innerHTML += selected.node.data.connected[i] + '<br/>'
                        }

                        $('#serviceimage').attr('src', selected.node.data.imageurl);
                        //var t0 = performance.now();
                        if (!handler.sidebarOpen) {
                            $("#sidebar").trigger("sidebar:open", [{ speed: 350 }]); // Open personal plan sidebar
                            handler.sidebarOpen = true;
                        }
                        //var t1 = performance.now();
                        //console.log("Call to doSomething took " + (t1 - t0) + " milliseconds.");

                        
                        that.graphDraw(window.services);
                    },

                    dragged: function (e) {
                        var s = handler.calcMousePos(e);
                        if (!nearest) return
                        if (dragged !== null && dragged.node !== null) {
                            var p = particleSystem.fromScreen(s)
                            dragged.node.p = p;
                            var pos = handler.calcMousePos(e);
                            if (Math.abs(pos.x - oldDown.x) + Math.abs(pos.y - oldDown.y) > dragTol) {
                                wasDragged = true;
                            }
                        }

                        return false
                    },

                    dropped: function (e) {
                        if (dragged === null || dragged.node === undefined) return
                        if (dragged.node !== null) dragged.node.fixed = false
                        dragged.node.tempMass = 50
                        dragged = null
                        selected = null
                        $(canvas).unbind('mousemove', handler.dragged)
                        $(window).unbind('mouseup', handler.dropped)
                        _mouseP = null
                        return false
                    }
                }

                $(canvas).mousedown(handler.down);
                $(canvas).mouseup(handler.up);
                $(canvas).mousemove(handler.moved);
            },

            convertNode: function (selected) {
                if (selected.node != null) {
                    var n = {
                        name: selected.node.name,
                        selected: selected.node.data.selected
                    };
                    selected = n;
                }
                return selected;
            },

            toggleNode: function (selected) {
                selected = that.convertNode(selected);

                //update list of services to reflect service selection/deselection
                for (i = 0; i < window.services.length; i++) {
                    if (window.services[i].name == selected.name) {
                        window.services[i].selected = !window.services[i].selected;
                        // Updates contact form list
                        if (window.services[i].selected == true) {
                            $('#noneselected').remove();
                            $('#contactformlist ul').append('<li class=selectedservice id=c' + window.services[i].name + '>' + window.services[i].label + '</li>');
                        }
                        else {
                            $('#c' + window.services[i].name).remove();
                            if ($('#contactformlist ul li').length == 0) {
                                $('#contactformlist ul').append("<li class=selectedservice id=noneselected>You haven't selected any services yet!</li>")
                            }
                        }
                    }
                }

                var num = 0;
                for (i = 0; i < window.services.length; i++) {
                    if (window.services[i].selected) num++;
                }

                that.progressBarUpdate(num);
                that.graphDraw(window.services);
            },

            listItemClick: function () {
                $('document').ready(function () {
                    $('li.servicelistitem').click(function () {
                        var liId = this.id;
                        var result = that.listUpdate(liId);
                        that.toggleNode(window.services[result]);
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
            },

            progressBarUpdate: function (num) {
                //Updating progress bar
                var onehundredpercentofprogressbar = 100 / (window.services.length - 1);
                var barprogress = num * onehundredpercentofprogressbar;
                $('#pb').progressbar({ value: barprogress });

                // Update how many services the user has selected
                $('#haveselected').html("I have chosen " + num + " out of " + (window.services.length - 1) + " services");
            }
        }

        // helpers for figuring out where to draw arrows (thanks springy.js)
        var intersect_line_line = function (p1, p2, p3, p4) {
            var denom = ((p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y));
            if (denom === 0) return false // lines are parallel
            var ua = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denom;
            var ub = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denom;

            if (ua < 0 || ua > 1 || ub < 0 || ub > 1) return false
            return arbor.Point(p1.x + ua * (p2.x - p1.x), p1.y + ua * (p2.y - p1.y));
        }

        var intersect_line_box = function (p1, p2, boxTuple) {
            var p3 = { x: boxTuple[0], y: boxTuple[1] },
                w = boxTuple[2],
                h = boxTuple[3]

            var tl = { x: p3.x, y: p3.y };
            var tr = { x: p3.x + w, y: p3.y };
            var bl = { x: p3.x, y: p3.y + h };
            var br = { x: p3.x + w, y: p3.y + h };

            return intersect_line_line(p1, p2, tl, tr) ||
                  intersect_line_line(p1, p2, tr, br) ||
                  intersect_line_line(p1, p2, br, bl) ||
                  intersect_line_line(p1, p2, bl, tl) ||
                  false
        }

        return that
    }
})()

//this is not a traditional obj of arbor's system. This obj is used in the global services array, and is what the server-side data model is translated into.
//In turn, this is translated into the object that arbor uses.
function service() {
    name = '';
    label = '';
    desc = '';
    selectable = false;
    selected = false;
    connected = [];
    parent = '';
    color = 'red';
    shape = 'dot';
    imageurl = '';
}