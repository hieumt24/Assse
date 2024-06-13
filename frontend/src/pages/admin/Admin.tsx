
import { Header } from "@/components/Header";
import { CreateUser, ManageUser } from "@/pages/admin/";
import { } from "@radix-ui/react-collapsible";
import { Route, Routes } from "react-router-dom";
import { Sidebar } from "./SideBar";

export const Admin = () => {

  return (
    <div className="h-full items-start">
      
      <Header/>
      <div className="flex flex-grow h-full">
        <Sidebar />  
        <Routes>
          <Route path="user" element={<ManageUser />} />
          <Route path="user/create-user" element={<CreateUser />} />
        </Routes>
      </div>
    </div>
  );
};
