import { ADMIN_NAV_FUNCTIONS, STAFF_NAV_FUNCTIONS } from "@/constants"; // Ensure correct path
import { useAuth } from "@/hooks";
import { Link, useLocation } from "react-router-dom";

export const Sidebar = () => {
  const location = useLocation();
  const { user } = useAuth();

  return (
    <div className="flex flex-col items-center bg-transparent p-6 text-white shadow-md w-[300px]">
      {/* Logo */}
      <Link
        to="/"
        className="mb-6 mt-2 flex flex-col items-start justify-center"
      >
        <div className="h-16 w-16">
          <img src="/logo.svg" alt="Logo" className="object-cover" />
        </div>
        <span className="text-xl font-bold">
          <span className="text-red-600">Online Asset Management</span>
        </span>
      </Link>

      {/* Navigation menu */}
      <div className="mt-4 w-full bg-zinc-100 font-semibold">
        {user.role === "Admin" &&
          ADMIN_NAV_FUNCTIONS.map((item) => (
            <Link
              to={`${item.path}`}
              key={item.path}
              className={`flex w-full items-center gap-3 p-3 transition duration-200 ${
                location.pathname.includes(`${item.path}`)
                  ? "bg-red-600 text-white"
                  : "text-black hover:bg-red-600 hover:text-white"
              }`}
            >
              <span className="text-lg">{item.name}</span>
            </Link>
          ))}
        {user.role === "Staff" &&
          STAFF_NAV_FUNCTIONS.map((item) => (
            <Link
              to={`${item.path}`}
              key={item.path}
              className={`flex w-full items-center gap-3 p-3 transition duration-200 ${
                location.pathname.includes(`${item.path}`)
                  ? "bg-red-600 text-white"
                  : "text-black hover:bg-red-600 hover:text-white"
              }`}
            >
              <span className="text-lg">{item.name}</span>
            </Link>
          ))}
      </div>
    </div>
  );
};
