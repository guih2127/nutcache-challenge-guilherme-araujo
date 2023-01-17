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
import ButtonGroup from "@mui/material/ButtonGroup";
import EmployeeDeleteModal from "./employee.delete.modal";
import moment from "moment/moment";
import options from "./options/select.options";

export default function EmployeesTable({ employees, retrieveEmployees }) {
  const [employee, setEmployee] = useState();
  const [saveEmployeeModal, setOpenSaveEmployeeModal] = useState(false);
  const [modalType, setModalType] = useState();
  const [deleteEmployeeModal, setDeleteEmployeeModal] = useState(false);

  const onEditEmployeeButtonClick = (employee) => {
    setEmployee(employee);
    setModalType("UPDATE");
    setOpenSaveEmployeeModal(true);
    setDeleteEmployeeModal(false);
  };

  const setModalClose = () => {
    setEmployee(null);
    setOpenDeleteEmployeeModal(false);
    setOpenSaveEmployeeModal(false);
  };

  const onAddEmployeeButtonClick = () => {
    const employee = {
      email: "",
      startDate: moment().toDate(),
      birthDate: moment().toDate(),
      team: options.teamOptions[0].value,
      gender: options.genderOptions[0].value,
      CPF: "",
    };

    setModalType("INSERT");
    setEmployee(employee);
    setOpenSaveEmployeeModal(true);
    setDeleteEmployeeModal(false);
  };

  const onDeleteEmployeeButtonClick = (employee) => {
    setEmployee(employee);
    setDeleteEmployeeModal(true);
    setOpenSaveEmployeeModal(false);
  };

  const renderSaveEmployeeModal = () => {
    if (employee) {
      return (
        <EmployeeModal
          open={saveEmployeeModal}
          onClose={setModalClose}
          modalType={modalType}
          employee={employee}
          setEmployee={setEmployee}
          retrieveEmployees={retrieveEmployees}
        />
      );
    }
  };

  const renderDeleteEmployeeModal = () => {
    if (employee) {
      return (
        <EmployeeDeleteModal
          open={deleteEmployeeModal}
          onClose={setModalClose}
          retrieveEmployees={retrieveEmployees}
          employee={employee}
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
                <TableCell align="right">
                  {moment(row.birthDate).format("DD/MM/YYYY")}
                </TableCell>
                <TableCell align="right">
                  {options.teamOptions.find((x) => x.value === row.team).label}
                </TableCell>
                <TableCell align="right">
                  {
                    options.genderOptions.find((x) => x.value === row.gender)
                      .label
                  }
                </TableCell>
                <TableCell align="right">{row.cpf}</TableCell>
                <TableCell align="right">
                  <ButtonGroup
                    variant="contained"
                    aria-label="outlined primary button group"
                  >
                    <Button onClick={() => onEditEmployeeButtonClick(row)}>
                      Edit
                    </Button>
                    <Button onClick={() => onDeleteEmployeeButtonClick(row)}>
                      Delete
                    </Button>
                  </ButtonGroup>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      {renderSaveEmployeeModal()}
      {renderDeleteEmployeeModal()}
      <Button
        variant="contained"
        component="label"
        onClick={onAddEmployeeButtonClick}
        style={{ marginTop: "10px" }}
      >
        Add Employee
      </Button>
    </>
  );
}
