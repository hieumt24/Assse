export const ADMIN_NAV_FUNCTIONS = [
  {
    name: "Home",
    path: "/home",
  },
  {
    name: "Manage User",
    path: "/users",
  },
  {
    name: "Manage Asset",
    path: "/assets",
  },
  {
    name: "Manage Assignment",
    path: "/assignments",
  },
  {
    name: "Request for Returning",
    path: "/returning-request",
  },
  {
    name: "Report",
    path: "/reports",
  },
];

export const STAFF_NAV_FUNCTIONS = [
  {
    name: "Home",
    path: "/home",
  },
];

export const BREADCRUMB_COMPONENTS = [
  {
    name: "Create New User",
    path: "/users/create",
    link: "#",
  },
  {
    name: "Edit User",
    path: "/users/edit/",
    link: "#",
  },
  {
    name: "Create New Asset",
    path: "/assets/create",
    link: "#",
  },
  {
    name: "Edit Asset",
    path: "/assets/edit",
    link: "#",
  },
  {
    name: "Create Assignment",
    path: "/assignments/create",
    link: "#",
  },
  {
    name: "Edit Assignment",
    path: "/assignments/edit",
    link: "",
  },
];

export const GENDERS = [
  {
    value: 2,
    label: "Male",
  },
  {
    value: 3,
    label: "Female",
  },
];

export const ROLES = [
  {
    value: 1,
    label: "Admin",
  },
  {
    value: 2,
    label: "Staff",
  },
];

export const LOCATIONS = [
  {
    value: 1,
    label: "Ha Noi",
  },
  {
    value: 2,
    label: "Da Nang",
  },
  {
    value: 3,
    label: "Ho Chi Minh",
  },
];

export const ASSET_STATES = [
  {
    value: 1,
    label: "Available",
  },
  {
    value: 2,
    label: "Not available",
  },
  {
    value: 3,
    label: "Assigned",
  },
  {
    value: 4,
    label: "Waiting for Recycling",
  },
  {
    value: 5,
    label: "Recycled",
  },
];

export const RETURNING_REQUEST_STATES = [
  {
    value: 1,
    label: "Waiting for returning",
  },
  {
    value: 2,
    label: "Completed",
  },
];
