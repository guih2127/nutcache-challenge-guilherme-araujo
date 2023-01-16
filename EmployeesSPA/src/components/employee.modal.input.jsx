import { TextField } from "@mui/material";

const EmployeeModalInput = ({ label, setValue, value }) => {
  return (
    <TextField
      required
      id="outlined-required"
      label={label}
      onChange={(e) => setValue(e.target.value)}
      value={value}
    />
  );
};

export default EmployeeModalInput;
