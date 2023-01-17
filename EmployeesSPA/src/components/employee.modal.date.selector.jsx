import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DesktopDatePicker } from "@mui/x-date-pickers/DesktopDatePicker";
import { AdapterMoment } from "@mui/x-date-pickers/AdapterMoment";
import TextField from "@mui/material/TextField";

export default function EmployeeModalDateSelector({
  label,
  value,
  setValue,
  views,
  inputFormat,
}) {
  const handleChange = (newValue) => {
    setValue(newValue);
  };

  return (
    <LocalizationProvider dateAdapter={AdapterMoment}>
      <DesktopDatePicker
        views={views}
        label={label}
        inputFormat={inputFormat}
        value={value}
        onChange={handleChange}
        renderInput={(params) => <TextField {...params} />}
      />
    </LocalizationProvider>
  );
}
