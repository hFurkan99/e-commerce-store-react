import { Divider, Paper, Typography } from "@mui/material";
import { useLocation } from "react-router-dom";

export default function ServerError() {
  const { state } = useLocation();
  console.log(state);
  return (
    <Paper>
      {state.error ? (
        <>
          <Typography
            gutterBottom
            variant="h3"
            sx={{ px: 4, pt: 2 }}
            color="secondary"
          >
            Internal Server Error
          </Typography>
          <Divider />
          <Typography variant="body1" sx={{ p: 4 }}>
            {Array.isArray(state.error)
              ? state.error.map((err: string, index: number) => (
                  <div key={index}>{err}</div>
                ))
              : state.error}
          </Typography>
        </>
      ) : (
        <Typography variant="h5" gutterBottom>
          Internal Server Error
        </Typography>
      )}
    </Paper>
  );
}
