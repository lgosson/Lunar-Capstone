(function () {

    window.services = [];

    Renderer = function (canvas) {
        var canvas = $(canvas).get(0)
        var ctx = canvas.getContext("2d");
        var gfx = arbor.Graphics(canvas)
        var particleSystem = null

        //**var cWidth = canvas.width = window.innerWidth;
        //**var cHeight = canvas.height = window.innerHeight;

        var that = {
            init: function (system) {
                particleSystem = system
                particleSystem.screenSize(canvas.width, canvas.height)
                particleSystem.screenPadding(40)

                that.initMouseHandling();
                that.listItemClick();

                //**$(window).resize(this.windowsized);
            },

            initialize: function (system, result) {
                particleSystem = system;
            },

            graphDraw: function (result) {
                var display = [];

                for (i = 0; i < result.length; i++) {
                    if (result[i].selectable == false || result[i].selected == true) {
                        display.push(result[i]);
                        for (j = 0; j < result[i].connected.length; j++) {
                            for (k = 0; k < result.length; k++) {
                                if (result[k].name == result[i].connected[j]) {
                                    display.push(result[k]);
                                }
                            }
                        }

                        //INITIAL WORK on recusively revealing all parents from selected nodes to the base
                        /*
                        var cur = result[i];
                        var go = true;

                        do {
                            go = false;
                            for (p = 0; p < result.length; p++) {
                                if (result[p].name == cur.parent) {
                                    go = true;
                                    display.push(cur);
                                    cur = result[p];
                                }
                            }
                        } while (go);
                        */
                    }
                }

                for (i = 0; i < display.length; i++) {
                    if (particleSystem.getNode(display[i].name) == null) {
                        particleSystem.addNode(display[i].name, {
                            name: display[i].name,
                            label: display[i].label,
                            'desc': display[i].desc,
                            'selectable': display[i].selectable,
                            'selected': display[i].selected,
                            'connected': display[i].connected,
                            'parent': display[i].parent,
                            'color': display[i].color,
                            'shape': display[i].shape
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
                particleSysem.graft();
            },

            updateNodes: function(){
                particleSystem.eachNode(function (node, pt) {
                
                    for(i = 0; i < window.services.length; i++){
                        if(node.data.name == window.services[i].name){
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

            //**windowsized: function () {
            //**    cWidth = (window.innerWidth)*.8;
            //**    cHeight = window.innerHeight;

            //**   particleSystem.screenSize(cWidth, cHeight);
            //**},

            redraw: function () {

                if (!particleSystem) return


                //**ctx.fillRect(0, 0, canvas.width, canvas.height);
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
                var oldmass = 1;

                var lastClick = new Date().getTime();
                var newClick = lastClick;
                var dblClickTolerance = 300;
                var wasDragged = false;

                // set up a handler object that will initially listen for mousedowns then
                // for moves and mouseups while dragging
                var handler = {
                    clicked: function (e) {
                        var pos = $(canvas).offset();
                        _mouseP = arbor.Point(e.pageX - pos.left, e.pageY - pos.top)
                        selected = nearest = dragged = particleSystem.nearest(_mouseP);

                        if (dragged.node !== null) dragged.node.fixed = true

                        newClick = new Date().getTime();
                        if (newClick - lastClick < dblClickTolerance) {
                            if (selected.node.data.selectable == true) handler.doubleclicked(e)
                        }
                        else {
                            $(canvas).bind('mousemove', handler.dragged)
                            $(window).bind('mouseup', handler.dropped)
                        }

                        lastClick = new Date().getTime();
                        return false
                    },

                    clickedUp: function (e) {
                        if (!wasDragged) {
                            if (selected.node.data.selectable == true) handler.singleclicked(e)
                        }
                        else wasDragged = false
                        $(canvas).unbind('mousemove', handler.dragged)
                        $(window).unbind('mouseup', handler.dropped)
                        return false
                    },

                    dragged: function (e) {
                        var old_nearest = nearest && nearest.node._id
                        var pos = $(canvas).offset();
                        var s = arbor.Point(e.pageX - pos.left, e.pageY - pos.top)
                        if (!nearest) return
                        if (dragged !== null && dragged.node !== null) {
                            var p = particleSystem.fromScreen(s)
                            dragged.node.p = p
                        }

                        wasDragged = true;
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
                    },

                    singleclicked: function (e) {
                        $('#sname').html(selected.node.data.label); // Updates service information partial view
                        $('#sdescription').html(selected.node.data.desc);
                        si = document.getElementById('sconnected')
                        si.innerHTML = '';
                        for (var i = 0; i < selected.node.data.connected.length; i++) {
                            si.innerHTML += selected.node.data.connected[i] + '<br/>'
                        }
                        $("#personalplan").trigger("sidebar:open", [{ speed: 350 }]); // Open personal plan sidebar
                    },

                    doubleclicked: function (e) {

                        that.toggleNode(selected);

                        return false
                    }
                }

                $(canvas).mousedown(handler.clicked);
                $(canvas).mouseup(handler.clickedUp);
            },

            toggleNode: function (selected) {
                if(selected.node != null){
                    var n = {
                        name: selected.node.name,
                        selected: selected.node.data.selected
                    };
                    selected = n;
                }
                
                //update list of services to reflect service selection/deselection
                for (i = 0; i < window.services.length; i++) {
                    if (window.services[i].name == selected.name) {
                        window.services[i].selected = !window.services[i].selected;
                    }
                }

                var num = 0;
                for (i = 0; i < window.services.length; i++) {
                    if (window.services[i].selected) num++;
                }

                //TODO: move progress bar and service selection updates to their own functions

                //Updating progress bar
                var onehundredpercentofprogressbar = 100 / (window.services.length - 1);
                var barprogress = num * onehundredpercentofprogressbar;
                $('#pb').progressbar({ value: barprogress });

                // Update how many services the user has selected
                $('#haveselected').html("I have chosen " + num + " out of " + (window.services.length - 1) + " services");
                that.graphDraw(window.services);
            },

            listItemClick: function () {
                $('document').ready(function () {
                    $('li.servicelistitem').click(function () {
                        var liId = this.id;
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

                                    $('#' + window.services[i].name).css("color", "white");
                                    $('#' + window.services[i].name).css("background-color", "#97233F");
                                }
                                else {
                                    $('#' + window.services[i].name).css("color", "black");
                                    $('#' + window.services[i].name).css("background-color", "white");
                                }
                            }
                        }
                        $('#nodeselected').html(window.services.toString());

                        // *** Updating progress bar *** //
                        var onehundredpercentofprogressbar = 100 / (window.services.length - 1);
                        var barprogress = window.services.length * onehundredpercentofprogressbar;
                        $('#pb').progressbar({ value: barprogress });

                        // Update how many services the user has selected
                        $('#haveselected').html("I have chosen " + window.services.length + " out of " + (window.services.length - 1) + " services");
                        that.toggleNode(window.services[num]);
                    })
                });
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