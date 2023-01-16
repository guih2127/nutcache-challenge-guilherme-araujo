import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

export default function EmployeesTable({ employees }) {
    return (
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
            </TableRow>
          </TableHead>
          <TableBody>
            {employees.map((row) => (
              <TableRow
                key={row.id}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
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
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    );
}