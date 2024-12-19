import { publicApi } from "@/lib/api";
import { queryOptions, useQuery } from "@tanstack/react-query";
import { API_PREFIX_CORE, REACT_QUERY_KEY_CORE } from "../constants";

const getCores = () => {
  return publicApi.get(`/${API_PREFIX_CORE}`);
};

export const getQueryOptions = () => {
  return queryOptions({
    queryKey: [REACT_QUERY_KEY_CORE],
    queryFn: () => getCores(),
  });
};

export const useCores = ({ queryConfig } = {}) => {
  return useQuery({
    ...getQueryOptions(),
    ...queryConfig,
  });
};
