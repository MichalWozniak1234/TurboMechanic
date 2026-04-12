//(function () {
//    const rootElement = document.getElementById("turbo-mechanik-root");

//    if (!rootElement || !window.React || !window.ReactDOM) {
//        return;
//    }

//    const h = React.createElement;

//    function BadgeList() {
//        const items = ["VIN", "Marka i model", "Rodzaj szkody", "Lokalizacja", "Ocena warsztatu"];

//        return h("div", { className: "badge-list" },
//            items.map((item) => h("span", { key: item }, item))
//        );
//    }

//    function ReportPreview() {
//        return h("div", { className: "report-stack", "aria-label": "Podglad raportu" },
//            h("article", { className: "report-card report-card-back" },
//                h("span", { className: "status-dot" }),
//                h("strong", null, "Dane pojazdu"),
//                h("p", null, "Rocznik, wersja silnika i podstawowe parametry.")
//            ),
//            h("article", { className: "report-card report-card-mid" },
//                h("span", { className: "status-dot warning" }),
//                h("strong", null, "Szacowany koszt"),
//                h("div", { className: "price-range" },
//                    h("span", null, "1 250 zl"),
//                    h("span", null, "2 100 zl")
//                )
//            ),
//            h("article", { className: "report-card report-card-front" },
//                h("span", { className: "status-dot success" }),
//                h("strong", null, "Rekomendowane warsztaty"),
//                h("div", { className: "mini-chart" },
//                    h("span", { style: { height: "38%" } }),
//                    h("span", { style: { height: "64%" } }),
//                    h("span", { style: { height: "52%" } }),
//                    h("span", { style: { height: "80%" } }),
//                    h("span", { style: { height: "68%" } })
//                ),
//                h("p", null, "Cena, odleglosc i ocena dopasowane do naprawy.")
//            )
//        );
//    }

//    function EstimateView() {
//        const benefits = [
//            "Pobranie danych auta po numerze VIN",
//            "Wybor szkody z przygotowanej listy",
//            "Orientacyjny koszt naprawy",
//            "Lista mechanikow w poblizu"
//        ];

//        return h("div", { className: "page-shell" },
//            h("section", { className: "hero" },
//                h("div", { className: "hero-content" },
//                    h("p", { className: "eyebrow" }, "MVP aplikacji webowej"),
//                    h("h1", null, "Sprawdz koszt naprawy zanim wybierzesz warsztat"),
//                    h("p", { className: "hero-lead" }, "Wpisz VIN, wybierz rodzaj szkody i porownaj mechanikow wedlug ceny, odleglosci oraz oceny."),
//                    h("form", { className: "vin-form", onSubmit: (event) => event.preventDefault() },
//                        h("input", { type: "text", placeholder: "Wpisz numer VIN", "aria-label": "Numer VIN" }),
//                        h("button", { type: "button" }, "Wycen naprawe")
//                    ),
//                    h("button", { className: "secondary-action", type: "button" }, "Nie mam numeru VIN"),
//                    h(BadgeList)
//                ),
//                h("div", { className: "hero-visual" }, h(ReportPreview))
//            ),
//            h("section", { className: "info-grid", "aria-label": "Najwazniejsze funkcje" },
//                benefits.map((item, index) =>
//                    h("article", { className: "info-card", key: item },
//                        h("span", null, "0" + (index + 1)),
//                        h("h2", null, item),
//                        h("p", null, "Element procesu opisany w zakresie MVP projektu TurboMechanik.")
//                    )
//                )
//            )
//        );
//    }

//    function WorkshopsView() {
//        const workshops = [
//            ["Auto Serwis Centrum", "1.8 km", "4.9", "Od 1 250 zl"],
//            ["Mechanika Plus", "3.4 km", "4.7", "Od 1 420 zl"],
//            ["Turbo Garage", "5.1 km", "4.8", "Od 1 390 zl"]
//        ];

//        return h("div", { className: "page-shell" },
//            h("section", { className: "workshop-hero" },
//                h("p", { className: "eyebrow" }, "Widok rekomendacji"),
//                h("h1", null, "Warsztaty dopasowane do naprawy"),
//                h("p", null, "Lista startowa pokazuje dane potrzebne w MVP: cene, odleglosc, ocene i szybki wybor mechanika.")
//            ),
//            h("section", { className: "workshop-layout" },
//                h("aside", { className: "filter-panel" },
//                    h("h2", null, "Kryteria"),
//                    h("label", null, "Rodzaj szkody", h("select", null,
//                        h("option", null, "Uklad hamulcowy"),
//                        h("option", null, "Silnik"),
//                        h("option", null, "Zawieszenie")
//                    )),
//                    h("label", null, "Lokalizacja", h("input", { type: "text", defaultValue: "Nowy Sacz" })),
//                    h("button", { type: "button" }, "Odswiez wyniki")
//                ),
//                h("div", { className: "workshop-list" },
//                    workshops.map(([name, distance, rating, price]) =>
//                        h("article", { className: "workshop-card", key: name },
//                            h("div", null,
//                                h("h2", null, name),
//                                h("p", null, "Naprawa zgodna z wybranym typem szkody")
//                            ),
//                            h("dl", null,
//                                h("div", null, h("dt", null, "Odleglosc"), h("dd", null, distance)),
//                                h("div", null, h("dt", null, "Ocena"), h("dd", null, rating)),
//                                h("div", null, h("dt", null, "Cena"), h("dd", null, price))
//                            ),
//                            h("button", { type: "button" }, "Wybierz")
//                        )
//                    )
//                )
//            )
//        );
//    }

//    const views = {
//        estimate: EstimateView,
//        workshops: WorkshopsView
//    };

//    const View = views[rootElement.dataset.view] || EstimateView;
//    ReactDOM.createRoot(rootElement).render(h(View));
//})();
