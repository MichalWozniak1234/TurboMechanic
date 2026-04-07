import React from "react";
import { createRoot } from "react-dom/client";
import { App } from "./App";

const rootElement = document.getElementById("turbo-mechanik-root");

if (rootElement) {
  createRoot(rootElement).render(
    <React.StrictMode>
      <App viewName={rootElement.dataset.view ?? "estimate"} />
    </React.StrictMode>
  );
}
