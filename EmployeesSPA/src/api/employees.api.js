import axios from "axios";

const client = axios.create({
   baseURL: "https://localhost:7164/api/employee" 
}); // TODO - PASSAR PARA .env

export default client;