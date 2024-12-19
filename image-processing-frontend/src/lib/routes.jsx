import Dashboard from "@/pages/dashboard";

export const dashboard_route = {
  path: "dashboard",
  children: [
    {
      path: "",
      element: <Dashboard />,
    },
  ],
};
