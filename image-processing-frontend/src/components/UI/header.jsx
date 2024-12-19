import * as React from "react";
import { extendTheme } from "@mui/material/styles";
import DashboardIcon from "@mui/icons-material/Dashboard";
import InputIcon from "@mui/icons-material/Input";
import Image from "@mui/icons-material/Image";
import PermMediaIcon from "@mui/icons-material/PermMedia";
import { AppProvider } from "@toolpad/core/AppProvider";
import { DashboardLayout } from "@toolpad/core/DashboardLayout";
import FilterHdrIcon from "@mui/icons-material/FilterHdr";
import { Box } from "@mui/material";
import Dashboard from "@/pages/dashboard";
import Process from "@/pages/process";
import Images from "@/pages/Images";

const NAVIGATION = [
  {
    kind: "header",
    title: "Main",
  },
  {
    segment: "dashboard",
    title: "Dashboard",
    icon: <DashboardIcon />,
  },
  {
    segment: "process",
    title: "Process",
    icon: <InputIcon />,
  },
  {
    kind: "divider",
  },
  {
    kind: "header",
    title: "Gallery",
  },
  {
    segment: "images",
    title: "Images",
    icon: <PermMediaIcon />,
    children: [
      {
        segment: "image1",
        title: "Image1",
        icon: <Image />,
      },
    ],
  },
];

const demoTheme = extendTheme({
  colorSchemes: { light: true, dark: true },
  colorSchemeSelector: "class",
  breakpoints: {
    values: {
      xs: 0,
      sm: 600,
      md: 600,
      lg: 1200,
      xl: 1536,
    },
  },
});

function useDemoRouter(initialPath) {
  const [pathname, setPathname] = React.useState(initialPath);

  const router = React.useMemo(() => {
    return {
      pathname,
      searchParams: new URLSearchParams(),
      navigate: (path) => setPathname(String(path)),
    };
  }, [pathname]);

  return router;
}

const COMPONENT_MAP = {
  "/dashboard": <Dashboard />,
  "/process": <Process />,
  "/images": <Images />,
};

export default function DashboardLayoutBasic(props) {
  // eslint-disable-next-line react/prop-types
  const { window } = props;

  const router = useDemoRouter("/dashboard");

  const demoWindow = window ? window() : undefined;

  const content = COMPONENT_MAP[router.pathname] || <Dashboard />;

  return (
    <AppProvider
      navigation={NAVIGATION}
      router={router}
      theme={demoTheme}
      window={demoWindow}
      branding={{
        title: "Image Processor",
        logo: <FilterHdrIcon sx={{ mt: 0.5, ml: 2, fontSize: "30px" }} />,
      }}
    >
      <DashboardLayout>
        <Box
          sx={{
            p: "30px",
            ml: "50px",
            display: "flex",
            flexDirection: "column",
            gap: "50px",
            height: "100vh",
          }}
        >
          <Box sx={{ fontSize: "30px" }}>{content.type.name}</Box>
          <Box>{React.cloneElement(content, { router })}</Box>
        </Box>
      </DashboardLayout>
    </AppProvider>
  );
}
