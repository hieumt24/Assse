import { AuthRequired } from "@/components/AuthRequired";
import { Admin, Home, Login, NotFound } from "@/pages";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";

export const RouteProvider: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<AuthRequired><Home /></AuthRequired>} />
        <Route path="/admin/*" element={<AuthRequired><Admin /></AuthRequired>} />
        <Route path="*" element={<NotFound />} />
        <Route path="/auth">
          <Route path="login" element={<Login />} />
        </Route>
      </Routes>
    </Router>
  );
};
