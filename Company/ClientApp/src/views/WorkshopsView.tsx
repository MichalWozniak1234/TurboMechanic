import { useEffect, useState } from "react";
import { getWorkshops } from "../api";
import type { Workshop } from "../types";

const fallbackWorkshops: Workshop[] = [
  { id: 1, name: "Auto Serwis Centrum", description: "Naprawa zgodna z wybranym typem szkody", distance: "1.8 km", rating: "4.9", priceFrom: "Od 1 250 zl" },
  { id: 2, name: "Mechanika Plus", description: "Naprawa zgodna z wybranym typem szkody", distance: "3.4 km", rating: "4.7", priceFrom: "Od 1 420 zl" },
  { id: 3, name: "Turbo Garage", description: "Naprawa zgodna z wybranym typem szkody", distance: "5.1 km", rating: "4.8", priceFrom: "Od 1 390 zl" },
];

export function WorkshopsView() {
  const [workshops, setWorkshops] = useState<Workshop[]>(fallbackWorkshops);

  useEffect(() => {
    getWorkshops().then(setWorkshops).catch(() => setWorkshops(fallbackWorkshops));
  }, []);

  return (
    <div className="page-shell">
      <section className="workshop-hero">
        <p className="eyebrow">Widok rekomendacji</p>
        <h1>Warsztaty dopasowane do naprawy</h1>
        <p>Lista startowa pokazuje dane potrzebne w MVP: cene, odleglosc, ocene i szybki wybor mechanika.</p>
      </section>

      <section className="workshop-layout">
        <aside className="filter-panel">
          <h2>Kryteria</h2>

          <label>
            Rodzaj szkody
            <select>
              <option>Uklad hamulcowy</option>
              <option>Silnik</option>
              <option>Zawieszenie</option>
            </select>
          </label>

          <label>
            Lokalizacja
            <input type="text" defaultValue="Nowy Sacz" />
          </label>

          <button type="button">Odswiez wyniki</button>
        </aside>

        <div className="workshop-list">
          {workshops.map((workshop) => (
            <WorkshopCard key={workshop.id} workshop={workshop} />
          ))}
        </div>
      </section>
    </div>
  );
}

function WorkshopCard({ workshop }: { workshop: Workshop }) {
  return (
    <article className="workshop-card">
      <div>
        <h2>{workshop.name}</h2>
        <p>{workshop.description}</p>
      </div>

      <dl>
        <div>
          <dt>Odleglosc</dt>
          <dd>{workshop.distance}</dd>
        </div>
        <div>
          <dt>Ocena</dt>
          <dd>{workshop.rating}</dd>
        </div>
        <div>
          <dt>Cena</dt>
          <dd>{workshop.priceFrom}</dd>
        </div>
      </dl>

      <button type="button">Wybierz</button>
    </article>
  );
}
