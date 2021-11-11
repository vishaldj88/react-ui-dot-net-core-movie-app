import axios from "axios";

export default axios.create({
  baseURL: "http://localhost:5000/api",
  headers: {
    "Content-type": "application/json",
    "Authiorization": "Bearer SGVsbG86V29ybGQ="
  }
});