import { forwardRef } from "react";
import { FormControl, InputLabel, Select, FormHelperText } from "@mui/material";

// eslint-disable-next-line react/display-name
const Dropdown = forwardRef(
  ({ name, label, value, onChange, children, error }, ref) => {
    return (
      <FormControl fullWidth error={!!error}>
        <InputLabel>{label}</InputLabel>
        <Select
          ref={ref}
          value={value}
          onChange={(event) => onChange(event.target.value, name)}
          label={label}
        >
          {children}
        </Select>
        {error ? <FormHelperText>{error}</FormHelperText> : null}
      </FormControl>
    );
  }
);

export default Dropdown;
