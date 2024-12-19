import { useMutation, useQueryClient } from "@tanstack/react-query";
import { publicApi } from "@/lib/api";

export const create = (data, filterName, accelerator) => {
  return publicApi.post(`/${accelerator}?filterName=${filterName}`, data);
};

export const useProcessImage = ({ mutationConfig } = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ["process"],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: create,
  });
};
