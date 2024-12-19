import { styled } from "@mui/material/styles";
import Button from "@mui/material/Button";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";

const VisuallyHiddenInput = styled("input")({
  clip: "rect(0 0 0 0)",
  clipPath: "inset(50%)",
  height: 1,
  overflow: "hidden",
  position: "absolute",
  bottom: 0,
  left: 0,
  whiteSpace: "nowrap",
  width: 1,
});

// eslint-disable-next-line react/prop-types
export default function ImportButton({ children, onFileSelected }) {
  const handleFileChange = (event) => {
    const files = event.target.files;
    if (files && files[0]) {
      const file = files[0];
      const previewUrl = URL.createObjectURL(file);
      onFileSelected(previewUrl);
    }
  };

  return (
    <Button
      component="label"
      variant="contained"
      startIcon={<CloudUploadIcon />}
    >
      {children}
      <VisuallyHiddenInput
        type="file"
        onChange={handleFileChange}
        accept="image/*"
      />
    </Button>
  );
}
