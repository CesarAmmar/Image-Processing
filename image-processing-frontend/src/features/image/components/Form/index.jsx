import Dropdown from "@/components/UI/dropdown";
import ImportButton from "@/components/UI/importButton";
import { useAccelerators } from "@/features/accelerator/api/getAccelerators";
import { Box, Button, FormLabel, MenuItem, TextField } from "@mui/material";
import { useState } from "react";
import { FILTERS } from "../../constants";
import { useForm, Controller } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useCreateImage } from "../../api/create";
import { useProcessImage } from "@/features/accelerator/api/process";

const schema = z.object({
  name: z.string().min(1, { message: "Name is required" }),
  filter: z.string().min(1, { message: "Filter is required" }),
  accelerator: z.string().min(1, { message: "Accelerator is required" }),
  image: z.any().refine((files) => files && files.length > 0, {
    message: "Image is required",
  }),
});

// eslint-disable-next-line react/prop-types
export default function Form({ router }) {
  const [inputImagePreview, setInputImagePreview] = useState("");
  const [ouputImagePreview, setOutputImagePreview] = useState("");

  const { data: acceleratorsData } = useAccelerators();

  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: zodResolver(schema),
    defaultValues: { name: "", filter: "", accelerator: "", image: null },
  });

  const { mutate: createImage, isPending } = useCreateImage({
    mutationConfig: {
      onSuccess: () => {
        // eslint-disable-next-line react/prop-types
        router.navigate("/images");
      },
    },
  });

  const { mutate: processImage } = useProcessImage();

  const onSubmit = async (data) => {
    try {
      const formData = new FormData();

      formData.append("name", data.name);

      if (data.image?.[0]) {
        const file = data.image[0];

        const reader = new FileReader();

        reader.onloadend = async () => {
          const base64Image = reader.result.split(",")[1];
          formData.append("image", base64Image);

          await processImage(formData, data.filter, data.accelerator);
          await createImage(formData);
        };

        reader.onerror = (error) => {
          console.error("Error converting image to Base64:", error);
        };

        reader.readAsDataURL(file);
      }
    } catch (error) {
      console.error("Failed to create image:", error);
    }
  };

  return (
    <Box
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      sx={{
        display: "flex",
        flexDirection: "column",
        Height: "100vh",
        justifyContent: "center",
        alignItems: "initial",
        gap: "20px",
        mt: "20px",
      }}
    >
      <Box>
        <FormLabel>Insert Image Name</FormLabel>
        <Controller
          name="name"
          control={control}
          render={({ field }) => <TextField fullWidth {...field} />}
        />
        {errors.name && (
          <span style={{ color: "rgb(244, 67, 54)", fontSize: "12px" }}>
            {errors.name.message}
          </span>
        )}
      </Box>
      <Box>
        <FormLabel>Select Filter</FormLabel>
        <Controller
          name="filter"
          control={control}
          render={({ field }) => (
            <Dropdown {...field}>
              {FILTERS.map((obj, index) => (
                <MenuItem key={index} value={obj.id}>
                  {obj.label}
                </MenuItem>
              ))}
            </Dropdown>
          )}
        />
        {errors.filter && (
          <span style={{ color: "rgb(244, 67, 54)", fontSize: "12px" }}>
            {errors.filter.message}
          </span>
        )}
      </Box>
      <Box>
        <FormLabel>Select Accelerator</FormLabel>
        <Controller
          name="accelerator"
          control={control}
          render={({ field }) => (
            <Dropdown
              {...field}
              sx={{
                border: errors.filter ? "1px solid red" : "1px solid #ccc",
              }}
            >
              {acceleratorsData.map((obj, index) => (
                <MenuItem key={index} value={obj}>
                  {obj}
                </MenuItem>
              ))}
            </Dropdown>
          )}
        />
        {errors.accelerator && (
          <span style={{ color: "rgb(244, 67, 54)", fontSize: "12px" }}>
            {errors.accelerator.message}
          </span>
        )}
      </Box>
      <ImportButton>
        import image
        <Controller
          name="image"
          control={control}
          render={({ field }) => (
            <input
              type="file"
              style={{ opacity: "0" }}
              onChange={(e) => {
                const file = e.target.files[0];
                if (file) {
                  const previewUrl = URL.createObjectURL(file);
                  setInputImagePreview(previewUrl);
                }
                field.onChange(e.target.files);
              }}
            />
          )}
        />
      </ImportButton>
      {errors.image && (
        <span style={{ color: "rgb(244, 67, 54)", fontSize: "12px" }}>
          {errors.image.message}
        </span>
      )}
      <Box
        sx={{
          display: "grid",
          gridTemplateColumns: "repeat(auto-fit, minmax(200px, 1fr))",
          gap: "20px",
        }}
      >
        <Box>
          {inputImagePreview && (
            <Box sx={{ width: "100%", height: "100%", maxHeight: "500px" }}>
              <img
                src={inputImagePreview}
                alt="Uploaded Preview"
                style={{ width: "100%", height: "100%", objectFit: "contain" }}
              />
            </Box>
          )}
        </Box>
        <Box>
          {ouputImagePreview && (
            <Box
              sx={{
                width: "100%",
                height: "100%",
                maxHeight: "400px",
                borderStyle: "dotted",
              }}
            >
              <img
                src={ouputImagePreview}
                alt="Uploaded Preview"
                style={{ width: "100%", height: "100%", objectFit: "contain" }}
              />
            </Box>
          )}
        </Box>
      </Box>

      <Box sx={{ display: "flex", justifyContent: "end", mb: "20px" }}>
        <Button
          variant="contained"
          size="large"
          disabled={isPending}
          type="submit"
        >
          {isPending ? "Processing..." : "Process"}
        </Button>
      </Box>
    </Box>
  );
}
