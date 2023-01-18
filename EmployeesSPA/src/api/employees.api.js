import axios from "axios";

const employeesApiUrl = import.meta.env.VITE_API_URL;

const client = axios.create({
  baseURL: `${employeesApiUrl}/api/employee`,
});

export default client;
