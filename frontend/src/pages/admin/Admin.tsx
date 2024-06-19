import { FirstTimeForm } from "@/components";
import { Header } from "@/components/Header";
import { CreateAsset, CreateUser, ManageUser } from "@/pages/admin/";
import { Route, Routes } from "react-router-dom";
import { Sidebar } from "../../components/SideBar";
import { ManageAsset } from "./manage/asset/ManageAsset";
import { EditUser } from "./manage/user/EditUser";

export const Admin = () => {
  return (
    <div className="flex h-full flex-col items-start">
      <FirstTimeForm />
      <Header />
      <div className="flex w-full flex-grow">
        <Sidebar />
        <Routes>
          <Route path="user" element={<ManageUser />} />
          <Route path="asset" element={<ManageAsset />} />
          <Route path="user/create-user" element={<CreateUser />} />
          <Route path="asset/create-asset" element={<CreateAsset />} />
          <Route path="user/edit/:staffCode" element={<EditUser />} />
        </Routes>
      </div>
    </div>
  );
};
