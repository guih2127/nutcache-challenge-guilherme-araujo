import * as React from "react";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";

const EmployeeModalSelect = ({ label, options, setValue, value }) => {
  const renderValues = () =>
    options.map((option) => {
      return <MenuItem value={option.value}>{option.label}</MenuItem>;
    });

  return (
    <FormControl sx={{ m: 1, minWidth: 120 }}>
      <InputLabel id="demo-simple-select-helper-label">{label}</InputLabel>
      <Select
        labelId="demo-simple-select-helper-label"
        id="demo-simple-select-helper"
        value={value}
        label={label}
        onChange={(e) => setValue(e.target.value)}
        error={!value}
      >
        {renderValues()}
      </Select>
    </FormControl>
  );
};

export default EmployeeModalSelect;
