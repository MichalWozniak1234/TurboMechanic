import { useEffect, useState } from "react";
import { getRepairSummary } from "../api";
import type { RepairSummary } from "../types";

const fallbackSummary: RepairSummary = {
  title: "Sprawdz koszt naprawy zanim wybierzesz warsztat",
  description: "Wpisz VIN, wybierz rodzaj szkody i porownaj mechanikow wedlug ceny, odleglosci oraz oceny.",
  steps: [
    "Pobranie danych auta po numerze VIN",
    "Wybor szkody z przygotowanej listy",
    "Orientacyjny koszt naprawy",
    "Lista mechanikow w poblizu",
  ],
};

export function EstimateView() {
  const [summary, setSummary] = useState<RepairSummary>(fallbackSummary);

  useEffect(() => {
    getRepairSummary().then(setSummary).catch(() => setSummary(fallbackSummary));
  }, []);

  return (
    <div className="page-shell">
      <section className="hero">
        <div className="hero-content">
          <p className="eyebrow">MVP aplikacji webowej</p>
          <h1>{summary.title}</h1>
          <p className="hero-lead">{summary.description}</p>

          <form className="vin-form" onSubmit={(event) => event.preventDefault()}>
            <input type="text" placeholder="Wpisz numer VIN" aria-label="Numer VIN" />
            <button type="submit">Wycen naprawe</button>
          </form>

          <button className="secondary-action" type="button">
            Nie mam numeru VIN
          </button>

          <div className="badge-list">
            <span>VIN</span>
            <span>Marka i model</span>
            <span>Rodzaj szkody</span>
            <span>Lokalizacja</span>
            <span>Ocena warsztatu</span>
          </div>
        </div>

        <div className="hero-visual">
          <ReportPreview />
        </div>
      </section>

      <section className="info-grid" aria-label="Najwazniejsze funkcje">
        {summary.steps.map((step, index) => (
          <article className="info-card" key={step}>
            <span>0{index + 1}</span>
            <h2>{step}</h2>
            <p>Element procesu opisany w zakresie MVP projektu TurboMechanik.</p>
          </article>
        ))}
      </section>
    </div>
  );
}

function ReportPreview() {
  return (
    <div className="report-stack" aria-label="Podglad raportu">
      <article className="report-card report-card-back">
        <span className="status-dot" />
        <strong>Dane pojazdu</strong>
        <p>Rocznik, wersja silnika i podstawowe parametry.</p>
      </article>

      <article className="report-card report-card-mid">
        <span className="status-dot warning" />
        <strong>Szacowany koszt</strong>
        <div className="price-range">
          <span>1 250 zl</span>
          <span>2 100 zl</span>
        </div>
      </article>

      <article className="report-card report-card-front">
        <span className="status-dot success" />
        <strong>Rekomendowane warsztaty</strong>
        <div className="mini-chart">
          <span style={{ height: "38%" }} />
          <span style={{ height: "64%" }} />
          <span style={{ height: "52%" }} />
          <span style={{ height: "80%" }} />
          <span style={{ height: "68%" }} />
        </div>
        <p>Cena, odleglosc i ocena dopasowane do naprawy.</p>
      </article>
    </div>
  );
}
