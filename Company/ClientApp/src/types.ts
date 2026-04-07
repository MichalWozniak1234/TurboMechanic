export type RepairSummary = {
  title: string;
  description: string;
  steps: string[];
};

export type Workshop = {
  id: number;
  name: string;
  description: string;
  distance: string;
  rating: string;
  priceFrom: string;
};
