import { publicApi } from "@/lib/api";
import { queryOptions, useQuery } from "@tanstack/react-query";
import {
  API_PREFIX_ACCELERATOR,
  REACT_QUERY_KEY_ACCELERATOR,
} from "../constants";

const getAccelerators = () => {
  return publicApi.get(`/${API_PREFIX_ACCELERATOR}`);
};

export const getQueryOptions = () => {
  return queryOptions({
    queryKey: [REACT_QUERY_KEY_ACCELERATOR],
    queryFn: () => getAccelerators(),
  });
};

export const useAccelerators = ({ queryConfig } = {}) => {
  return useQuery({
    ...getQueryOptions(),
    ...queryConfig,
  });
};
