/* eslint-disable react/prop-types */
import Paper from "@/components/UI/paper";
import { Box, Button, Grid2, Typography } from "@mui/material";
import ExitToAppIcon from "@mui/icons-material/ExitToApp";
import { useAccelerators } from "@/features/accelerator/api/getAccelerators";
import { useCores } from "@/features/accelerator/api/getCores";

export default function Dashboard({ router }) {
  const { data: acceleratorsData } = useAccelerators();
  const acceleratorsCount = acceleratorsData?.length || 0;
  const { data: coresData } = useCores();

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        gap: "50px",
      }}
    >
      <Grid2
        sx={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          gap: "10px",
        }}
      >
        <Typography sx={{ fontWeight: "bold" }} variant="h5">
          Efficient Image Processing Made Simple
        </Typography>
        <Typography>
          Experience the power of advanced image editing with Image Processor
          Pro, the ultimate tool for sharpening, adjusting brightness, and
          enhancing your photos. Choose your preferred accelerator OpenCL, CUDA,
          or CPU for lightning-fast performance tailored to your hardware.
          Whether you&apos;re a professional editor or a casual user, our
          intuitive interface ensures that your images come to life with just a
          few clicks. Save your edited masterpieces directly to your gallery for
          quick access and sharing.
        </Typography>
      </Grid2>
      <Grid2
        container
        justifyContent="center"
        alignItems="center"
        spacing={0}
        sx={{
          display: "flex",
          flexWrap: "nowrap",
          gap: "40px",
        }}
      >
        <Paper title="Cores">{coresData || 0}</Paper>
        <Paper title="Accelerators">{acceleratorsCount}</Paper>
      </Grid2>
      <Grid2 container justifyContent="center" alignItems="center">
        <Button variant="contained" onClick={() => router.navigate("/process")}>
          {" "}
          <ExitToAppIcon />
          Get started
        </Button>
      </Grid2>
    </Box>
  );
}
