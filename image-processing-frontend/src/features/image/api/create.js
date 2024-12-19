import { useMutation, useQueryClient } from "@tanstack/react-query";
import { API_PREFIX, REACT_QUERY_KEY } from "../constants";
import { publicApi } from "@/lib/api";

export const create = (data) => {
  return publicApi.post(`/${API_PREFIX}`, data);
};

export const useCreateImage = ({ mutationConfig } = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: [REACT_QUERY_KEY],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: create,
  });
};
