
(function(){

    var sys = arbor.ParticleSystem(500, 1000, 1);
    sys.parameters({ gravity: true });
    sys.renderer = Renderer("#viewport");
    var data = {
        nodes: {
            stars: { 'color': 'red', 'shape': 'dot', label: 'TITLE HERE', },
            planet1: { 'color': 'green', 'shape': 'dot', 'label': 'Planet' },
            planet2: { 'color': 'blue', 'shape': 'dot', 'label': 'Planet' },
            planet3: { 'color': 'blue', 'shape': 'dot', 'label': 'Planet' },
            planet4: { 'color': 'blue', 'shape': 'dot', 'label': 'Planet' }

        },
        edges: {
            stars: { planet1: {}, planet2: {} },
            planet1: { planet3: {}, planet4: {} }
        }
    };
    sys.graft(data);

    //Events
    $(viewport).mousedown(function (e) {
        alert('Yo');
        return true;
    });

})()