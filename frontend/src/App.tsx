import { ToastContainer } from "react-toastify";
import { RouteProvider } from "./routes";

function App() {
  window.addEventListener("storage", () => {
    window.location.reload();
  });
  return (
    <>
      <RouteProvider />
      <ToastContainer />
    </>
  );
}

export default App;
