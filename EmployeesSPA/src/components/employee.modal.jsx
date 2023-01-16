import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Slide from "@mui/material/Slide";
import Box from "@mui/material/Box";
import EmployeeModalInput from "./employee.modal.input";
import employeesService from "../services/employees.service";
import { useState } from "react";

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

export default function EmployeeModal({
  open,
  onClose,
  employee,
  modalType,
  retrieveEmployees,
}) {
  const [email, setEmail] = useState(employee.email);
  const [birthDate, setBirthDate] = useState(employee.birthDate);
  const [startDate, setStartDate] = useState(employee.startDate);
  const [gender, setGender] = useState(employee.gender);
  const [team, setTeam] = useState(employee.team);
  const [cpf, setCpf] = useState(employee.cpf);

  const insertEmployee = async (newEmployee) => {
    await employeesService.insertEmployee(newEmployee).then((result) => {
      if (!result.data.errors) {
        retrieveEmployees();
        onClose();
      }
    });
  };

  const updateEmployee = async (newEmployee) => {
    await employeesService.updateEmployee(newEmployee).then((result) => {
      if (!result.data.errors) {
        retrieveEmployees();
        onClose();
      }
    });
  };

  const saveButtonOnClick = () => {
    const newEmployee = {
      email: email,
      birthDate: new Date(),
      startDate: startDate,
      gender: gender,
      team: team,
      cpf: cpf,
    };

    if (modalType === "INSERT") {
      insertEmployee(newEmployee);
    } else if (modalType === "UPDATE") {
      const updatedEmployee = {
        id: employee.id,
        email: email,
        birthDate: new Date(),
        startDate: startDate,
        gender: gender,
        team: team,
        cpf: cpf,
      };
      updateEmployee(updatedEmployee);
    }
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
        <Box
          component="form"
          sx={{
            "& .MuiTextField-root": { m: 1, width: "25ch" },
          }}
          noValidate
          autoComplete="off"
        >
          <DialogTitle>
            {modalType == "INSERT" ? "Insert employee" : "Edit Employee"}
          </DialogTitle>
          <DialogContent>
            <DialogContentText id="alert-dialog-slide-description">
              <EmployeeModalInput
                label="Email"
                value={email}
                setValue={setEmail}
              />
              <EmployeeModalInput
                label="Start Date"
                value={startDate}
                setValue={setStartDate}
              />
              <EmployeeModalInput
                label="Birth Date"
                value={birthDate}
                setValue={setBirthDate}
              />
              <EmployeeModalInput
                label="Team"
                value={team}
                setValue={setTeam}
              />
              <EmployeeModalInput
                label="Gender"
                value={gender}
                setValue={setGender}
              />
              <EmployeeModalInput label="CPF" value={cpf} setValue={setCpf} />
            </DialogContentText>
          </DialogContent>
          <DialogActions>
            <Button onClick={onClose}>Cancel</Button>
            <Button onClick={saveButtonOnClick}>
              {modalType == "INSERT" ? "Insert" : "Edit"}
            </Button>
          </DialogActions>
        </Box>
      </Dialog>
    </>
  );
}
