import { Separator } from "@/components/ui/separator";
import { ADMIN_NAV_FUNCTIONS } from "@/constants"; // Ensure correct path
import { Link, useLocation } from "react-router-dom";

export const Sidebar = () => {
  const location = useLocation();

  return (
    <div className="flex h-full flex-col items-center bg-red-500 p-6 text-white shadow-lg">
      {/* Logo */}
      <Link to="/" className="mb-6 flex items-center justify-center">
        <div className="h-16 w-16">
          <img src="/logo.svg" alt="Logo" className="object-cover" />
        </div>
        <span className="ml-3 text-xl font-bold">
          <span>Online Asset Management</span>
        </span>
      </Link>

      <Separator className="h-0.5 w-full" />

      {/* Navigation menu */}
      <div className="mt-4 w-full font-semibold">
        {ADMIN_NAV_FUNCTIONS.map((item) => (
          <Link
            to={`/admin${item.path}`}
            key={item.path}
            className={`flex w-full items-center gap-3 rounded-lg p-3 transition duration-200 ${
              location.pathname.includes(`/admin${item.path}`)
                ? "bg-white text-black"
                : "hover:bg-white hover:text-black"
            }`}
          >
            <div className="text-2xl">{item.icon}</div>
            <span className="text-lg">{item.name}</span>
          </Link>
        ))}
      </div>
    </div>
  );
};
