// Offsets the fixed .grid-bg pattern as the page scrolls.
(function () {
    var grid = document.querySelector(".grid-bg");
    if (!grid) return;

    window.addEventListener("scroll", function () {
        var scrollPosition = window.pageYOffset || document.documentElement.scrollTop;
        grid.style.backgroundPositionY = (-(scrollPosition / 8)) + "px";
    });
})();

// Toggles the mobile navigation menu.
(function () {
    var toggle = document.getElementById("nav-toggle");
    var links = document.getElementById("nav-links");
    var iconOpen = document.getElementById("nav-icon-open");
    var iconClose = document.getElementById("nav-icon-close");
    if (!toggle || !links || !iconOpen || !iconClose) return;

    function setOpen(isOpen) {
        links.classList.toggle("hidden", !isOpen);
        iconOpen.classList.toggle("hidden", isOpen);
        iconClose.classList.toggle("hidden", !isOpen);
        toggle.setAttribute("aria-expanded", String(isOpen));
    }

    toggle.addEventListener("click", function () {
        setOpen(links.classList.contains("hidden"));
    });

    links.querySelectorAll("a").forEach(function (link) {
        link.addEventListener("click", function () {
            setOpen(false);
        });
    });
})();

// Highlights the nav link for whichever section is currently in view.
(function () {
    var navLinks = document.getElementById("nav-links");
    if (!navLinks) return;

    var activeClasses = (navLinks.dataset.navActiveClass || "").split(/\s+/).filter(Boolean);
    var inactiveClasses = (navLinks.dataset.navInactiveClass || "").split(/\s+/).filter(Boolean);
    if (!activeClasses.length) return;

    var sections = Array.prototype.slice.call(navLinks.querySelectorAll("[data-section]"))
        .map(function (link) {
            return { link: link, section: document.getElementById(link.dataset.section) };
        })
        .filter(function (entry) { return entry.section; });
    if (!sections.length) return;

    function setActive(sectionId) {
        sections.forEach(function (entry) {
            var isActive = entry.section.id === sectionId;
            activeClasses.forEach(function (c) { entry.link.classList.toggle(c, isActive); });
            inactiveClasses.forEach(function (c) { entry.link.classList.toggle(c, !isActive); });
        });
    }

    var observer = new IntersectionObserver(function (entries) {
        entries.forEach(function (entry) {
            if (entry.isIntersecting) {
                setActive(entry.target.id);
            }
        });
    }, { rootMargin: "-45% 0px -45% 0px" });

    sections.forEach(function (entry) { observer.observe(entry.section); });
})();

// Displays the contact message's character allowance, matching its maxlength.
(function () {
    var textarea = document.getElementById("message");
    var counter = document.getElementById("message-counter");
    if (!textarea || !counter) return;

    var maxLength = textarea.maxLength;
    if (maxLength <= 0) return;
    var WARNING_THRESHOLD = 100;

    function update() {
        var remaining = Math.max(0, maxLength - textarea.value.length);
        counter.textContent = remaining.toLocaleString() + (remaining === 1 ? " character remaining" : " characters remaining");

        counter.classList.remove("text-amber-600", "dark:text-amber-400", "text-red-600", "dark:text-red-400");

        if (remaining <= WARNING_THRESHOLD) {
            if (remaining === 0) {
                counter.classList.add("text-red-600", "dark:text-red-400");
            } else {
                counter.classList.add("text-amber-600", "dark:text-amber-400");
            }
        }
    }

    textarea.addEventListener("input", update);
    window.addEventListener("pageshow", update);
    if (textarea.form) {
        textarea.form.addEventListener("reset", function () { window.requestAnimationFrame(update); });
    }
    update();
})();
