// Follow native scrolling without layout-changing animations or delayed reveals.
(function () {
    var root = document.getElementById("tl-root");
    if (!root) return;

    var rows = Array.prototype.slice.call(root.querySelectorAll(".tl-row"));
    var lit = document.getElementById("tl-lit");
    var car = document.getElementById("tl-car");
    if (!rows.length || !lit || !car) return;

    var motion = window.matchMedia("(prefers-reduced-motion: reduce)");
    var frame = null;
    var dirty = true;
    var rootTop = 0;
    var roadHeight = 0;
    var maxScroll = 0;
    var carHalfHeight = 0;
    var markers = [];
    var rowTops = [];
    var revealed = 0;
    var lastY = null;
    var snapRadius = 32;
    var snapHold = 6;

    function measure() {
        // Read geometry together, and only when layout changes (not each scroll).
        var bounds = root.getBoundingClientRect();
        rootTop = bounds.top + window.scrollY;
        roadHeight = bounds.height;
        maxScroll = Math.max(0, document.documentElement.scrollHeight - window.innerHeight);
        carHalfHeight = car.getBoundingClientRect().height / 2;
        rowTops = rows.map(function (row) {
            return row.getBoundingClientRect().top - bounds.top;
        });
        markers = rows.map(function (row) {
            var dot = row.querySelector(".tl-dot");
            // offsetTop/offsetHeight are unaffected by the dot's reveal transform.
            return row.offsetTop + dot.offsetTop + dot.offsetHeight / 2;
        });
        dirty = false;
        lastY = null;
    }

    function update() {
        frame = null;
        if (motion.matches) return;
        if (dirty) measure();

        var viewportY = window.scrollY - rootTop;
        // At the top of the page, start exactly on the first (2018) dot even
        // when the viewport's tracking line is already farther down the road.
        var startScroll = Math.max(0, rootTop + markers[0] - window.innerHeight * 0.55);
        var lastMarker = markers[markers.length - 1];
        var endScroll = Math.min(maxScroll, rootTop + lastMarker - window.innerHeight * 0.55);
        var progress = endScroll > startScroll
            ? Math.max(0, Math.min(1, (window.scrollY - startScroll) / (endScroll - startScroll)))
            : 0;
        var y = markers[0] + (lastMarker - markers[0]) * progress;

        // A small magnetic stop, not scroll hijacking: blend continuously into
        // the dot and hold its center briefly, in either direction of travel.
        for (var k = 0; k < markers.length; k++) {
            var delta = y - markers[k];
            var distance = Math.abs(delta);
            if (distance < snapRadius) {
                var t = Math.max(0, (distance - snapHold) / (snapRadius - snapHold));
                var blend = t * t * (3 - 2 * t);
                y = markers[k] + delta * blend;
                break;
            }
        }

        if (y !== lastY) {
            car.style.transform = "translate3d(-50%, " + (y - carHalfHeight) + "px, 0)";
            // Clip the full-size gradient so its dashes never stretch or trigger layout.
            lit.style.clipPath = "inset(0 0 " + Math.max(roadHeight - y, 0) + "px 0)";
            lastY = y;
        }

        var revealLine = viewportY + window.innerHeight * 0.85;
        while (revealed < rows.length && rowTops[revealed] <= revealLine) {
            rows[revealed++].classList.add("is-visible");
        }
    }

    function schedule() {
        if (!motion.matches && frame === null) frame = window.requestAnimationFrame(update);
    }

    function invalidate() {
        dirty = true;
        schedule();
    }

    function setMotion() {
        root.classList.toggle("js", !motion.matches);
        if (motion.matches) {
            rows.forEach(function (row) { row.classList.add("is-visible"); });
            revealed = rows.length;
            lit.style.clipPath = "none";
        }
        invalidate();
    }

    window.addEventListener("scroll", schedule, { passive: true });
    window.addEventListener("resize", invalidate);
    window.addEventListener("pageshow", invalidate);
    motion.addEventListener("change", setMotion);

    if ("ResizeObserver" in window) {
        var observer = new ResizeObserver(invalidate);
        observer.observe(root);
        // Navigation/content above the timeline can move it without resizing it.
        observer.observe(document.body);
    }
    if (document.fonts) document.fonts.ready.then(invalidate);

    setMotion();
})();
