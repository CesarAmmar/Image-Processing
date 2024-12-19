import { QueryClient } from "@tanstack/react-query";

export const queryConfig = {
  queries: {
    refetchOnWindowFocus: false,
  },
};

export const queryClient = new QueryClient({
  defaultOptions: queryConfig,
});
