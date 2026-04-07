import type { RepairSummary, Workshop } from "./types";

async function getJson<T>(url: string): Promise<T> {
  const response = await fetch(url);

  if (!response.ok) {
    throw new Error(`Request failed: ${response.status}`);
  }

  return response.json() as Promise<T>;
}

export function getRepairSummary(): Promise<RepairSummary> {
  return getJson<RepairSummary>("/api/repair/summary");
}

export function getWorkshops(): Promise<Workshop[]> {
  return getJson<Workshop[]>("/api/repair/workshops");
}
