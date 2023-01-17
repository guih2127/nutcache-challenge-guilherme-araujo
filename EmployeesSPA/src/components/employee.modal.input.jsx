import { TextField } from "@mui/material";

const EmployeeModalInput = ({ label, setValue, value }) => {
  return (
    <TextField
      required
      id="outlined-required"
      label={label}
      onChange={(e) => setValue(e.target.value)}
      value={value}
      error={!value}
    />
  );
};

export default EmployeeModalInput;
