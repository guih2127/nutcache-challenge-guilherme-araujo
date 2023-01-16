import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import Slide from "@mui/material/Slide";
import employeesService from "../services/employees.service";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

export default function EmployeeDeleteModal({
  open,
  onClose,
  employee,
  retrieveEmployees,
}) {
  const deleteEmployee = async () => {
    await employeesService.deleteEmployee(employee).then((result) => {
      if (!result.data.errors) {
        retrieveEmployees();
        onClose();
      }
    });
  };

  return (
    <>
      <Dialog
        open={open}
        TransitionComponent={Transition}
        keepMounted
        onClose={onClose}
        aria-describedby="alert-dialog-slide-description"
      >
        <DialogTitle id="alert-dialog-title">Delete Employee</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Are you sure you want to delete this employee?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose}>Cancel</Button>
          <Button onClick={deleteEmployee}>Delete</Button>
        </DialogActions>
      </Dialog>
    </>
  );
}
