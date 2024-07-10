import { RouteProvider } from "./routes";

function App() {
  //logout when multiple tabs are open
  window.addEventListener("storage", () => {
    if (!localStorage.getItem("token")) {
      localStorage.setItem("unauthorized", "true");
      window.location.href = "/";
    }
  });
  return (
    <>
      <RouteProvider />
    </>
  );
}

export default App;
