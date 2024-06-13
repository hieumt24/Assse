import { ADMIN_NAV_FUNCTIONS } from "@/constants"; // Ensure correct path
import { Link, useLocation } from "react-router-dom";

export const Sidebar = () => {
  const location = useLocation();

  return (
    <div className="flex h-full flex-col items-center bg-transparent p-6 text-white shadow-md">
      {/* Logo */}
      <Link to="/" className="mb-6 flex flex-col items-start justify-center">
        <div className="h-16 w-16">
          <img src="/logo.svg" alt="Logo" className="object-cover" />
        </div>
        <span className="text-xl font-bold">
          <span className="text-red-600">Online Asset Management</span>
        </span>
      </Link>

      {/* Navigation menu */}
      <div className="mt-4 w-full bg-zinc-100 font-semibold">
        {ADMIN_NAV_FUNCTIONS.map((item) => (
          <Link
            to={`/admin${item.path}`}
            key={item.path}
            className={`flex w-full items-center gap-3 p-3 transition duration-200 ${
              location.pathname.includes(`/admin${item.path}`)
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
