import { Admin, NotFound } from "@/pages";
import { CreateUser, ManageUser } from "@/pages/admin/";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";

export const RouteProvider: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/admin" element={<Admin />}>
          <Route path="user" element={<ManageUser />}></Route>
          <Route path="user/create-user" element={<CreateUser />} />
        </Route>
        <Route path="*" element={<NotFound />}></Route>
      </Routes>
    </Router>
  );
};
