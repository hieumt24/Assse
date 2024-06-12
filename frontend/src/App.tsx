import { ToastContainer } from "react-toastify";
import { RouteProvider } from "./routes";

function App() {
  return (
    <>
      <RouteProvider />;
      <ToastContainer />
    </>
  );
}

export default App;
