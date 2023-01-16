import * as React from "react";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import { useState } from "react";
import EmployeeModal from "./employee.modal";
import Button from "@mui/material/Button";

export default function EmployeesTable({ employees, retrieveEmployees }) {
  const [employee, setEmployee] = useState();
  const [openModal, setOpenModal] = useState(false);
  const [modalType, setModalType] = useState();

  const handleClickEditar = (employee) => {
    setEmployee(employee);
    setModalType("EDIT");
    setOpenModal(true);
  };

  const onModalClose = () => {
    setEmployee(null);
    setOpenModal(false);
  };

  const onAddEmployeeButtonClick = () => {
    const employee = {
      email: "",
      startDate: "",
      birthDate: "",
      Team: "",
      Gender: "",
      CPF: "",
    };

    setModalType("INSERT");
    setEmployee(employee);
    setOpenModal(true);
  };

  const renderEmployeeModal = () => {
    if (employee) {
      return (
        <EmployeeModal
          open={openModal}
          onClose={onModalClose}
          modalType={modalType}
          employee={employee}
          setEmployee={setEmployee}
          retrieveEmployees={retrieveEmployees}
        />
      );
    }
  };

  return (
    <>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="Employees List">
          <TableHead>
            <TableRow>
              <TableCell align="right">Id</TableCell>
              <TableCell align="right">Email</TableCell>
              <TableCell align="right">Start Date</TableCell>
              <TableCell align="right">Birth Date</TableCell>
              <TableCell align="right">Team</TableCell>
              <TableCell align="right">Gender</TableCell>
              <TableCell align="right">CPF</TableCell>
              <TableCell align="right">Ações</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {employees.map((row) => (
              <TableRow
                key={row.id}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {row.id}
                </TableCell>
                <TableCell align="right">{row.email}</TableCell>
                <TableCell align="right">{row.startDate}</TableCell>
                <TableCell align="right">{row.birthDate}</TableCell>
                <TableCell align="right">{row.team}</TableCell>
                <TableCell align="right">{row.gender}</TableCell>
                <TableCell align="right">{row.cpf}</TableCell>
                <TableCell align="right" onClick={() => handleClickEditar(row)}>
                  Editar
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      {renderEmployeeModal()}
      <Button
        variant="contained"
        component="label"
        onClick={onAddEmployeeButtonClick}
      >
        Add Employee
      </Button>
    </>
  );
}
