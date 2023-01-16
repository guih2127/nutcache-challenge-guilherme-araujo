import * as React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import Slide from '@mui/material/Slide';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';

const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

export default function EmployeeModal({ open, onClose, employee, modalType }) {
    if (employee) {
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
                    '& .MuiTextField-root': { m: 1, width: '25ch' },
                    }}
                    noValidate
                    autoComplete="off"
                >
                <DialogTitle>{ modalType == "INSERT" ? "Insert employee" : "Edit Employee" }</DialogTitle>
                <DialogContent>
                  <DialogContentText id="alert-dialog-slide-description">
                    <TextField
                        required
                        id="outlined-required"
                        label="Required"
                        defaultValue={employee.email}
                    />
                    <TextField
                        required
                        id="outlined-required"
                        label="Required"
                        defaultValue={employee.startDate}
                    />
                    <TextField
                        required
                        id="outlined-required"
                        label="Required"
                        defaultValue={employee.birthDate}
                    />
                    <TextField
                        required
                        id="outlined-required"
                        label="Required"
                        defaultValue={employee.team}
                    />
                    <TextField
                        required
                        id="outlined-required"
                        label="Required"
                        defaultValue={employee.gender}
                    />
                    <TextField
                        required
                        id="outlined-required"
                        label="Required"
                        defaultValue={employee.cpf}
                    />
                  </DialogContentText>
                </DialogContent>
                <DialogActions>
                  <Button onClick={onClose}>Cancel</Button>
                  <Button onClick={onClose}>Edit</Button>
                </DialogActions>
                </Box>
              </Dialog>
            </>
          );
    }
}