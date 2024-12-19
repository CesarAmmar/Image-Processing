import {
  createMemoryRouter,
  Navigate,
  Outlet,
  RouterProvider,
} from "react-router-dom";
import MainLayout from "../UI/main-layout";
import { dashboard_route } from "@/lib/routes";

export default function Router() {
  const router = createMemoryRouter([
    {
      path: "/",
      element: (
        <MainLayout>
          <Outlet />
        </MainLayout>
      ),
      children: [
        { index: true, element: <Navigate to="/dashboard" /> },
        dashboard_route,
      ],
    },
  ]);
  return <RouterProvider router={router} />;
}
