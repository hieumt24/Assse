import { FirstTimeForm } from "@/components";
import { Admin, Home, Login, NotFound } from "@/pages";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";

export const RouteProvider: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/first-time" element={<FirstTimeForm />} />
        <Route path="/admin/*" element={<Admin />} />
        <Route path="*" element={<NotFound />} />
        <Route path="/auth">
          <Route path="login" element={<Login />} />
        </Route>
      </Routes>
    </Router>
  );
};
