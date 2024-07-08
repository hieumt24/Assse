import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import App from "./App.tsx";
import { AuthProvider } from "./context/AuthContext.tsx";
import { LoadingProvider } from "./context/LoadingContext.tsx";
import "./index.css";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <LoadingProvider>
          <App />
          <ToastContainer />
        </LoadingProvider>
      </AuthProvider>
    </BrowserRouter>
  </React.StrictMode>,
);
