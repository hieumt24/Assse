import { FaUsers } from "react-icons/fa";
export const ADMIN_NAV_FUNCTIONS = [
  {
    name: "Manage User",
    path: "/user",
    icon: <FaUsers size={24} />,
  },
];

export const GENDERS = [
  {
    value: 0,
    label: "Female",
  },
  {
    value: 1,
    label: "Male",
  },
  {
    value: 2,
    label: "Other",
  },
];

export const ROLES = [
  {
    value: 0,
    label: "Admin",
  },
  {
    value: 1,
    label: "Staff",
  },
];

export const LOCATIONS = [
  {
    value: 0,
    label: "Hanoi",
  },
  {
    value: 1,
    label: "Danang",
  },
  {
    value: 2,
    label: "HCM",
  },
];
