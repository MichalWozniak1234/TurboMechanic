import { EstimateView } from "./views/EstimateView";
import { WorkshopsView } from "./views/WorkshopsView";

type AppProps = {
  viewName: string;
};

export function App({ viewName }: AppProps) {
  if (viewName === "workshops") {
    return <WorkshopsView />;
  }

  return <EstimateView />;
}
