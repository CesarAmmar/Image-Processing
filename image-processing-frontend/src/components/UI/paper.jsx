import { Card, CardContent, Grid2, Typography } from "@mui/material";

export default function Paper({ children, title }) {
  return (
    <Grid2 size={{ xs: 12, sm: 6, md: 4 }}>
      <Card
        variant="outlined"
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <CardContent>
          <Typography
            variant="body1"
            sx={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            {title}
          </Typography>
          <Typography
            variant="h3"
            sx={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            {children}
          </Typography>
        </CardContent>
      </Card>
    </Grid2>
  );
}
