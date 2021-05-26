import * as types from "./actionTypes";
import axios from "axios";

const url = "http://localhost:5000/api/processes";

export function loadProcesses() {
  return function (dispatch) {
    return axios
      .get(url + `/getProcesses`)
      .then((processes) => dispatch(loadProcessesSuccess(processes)));
  };
}

export function loadProcessesSuccess(processes) {
  return { type: types.LOAD_PROCESSES_SUCCESS, processes };
}

export function updateProcess({ process_id, name, description, hourly_rate }) {
  return function (dispatch) {
    return axios
      .get(url + `/update`, {
        params: {
          process_id,
          name,
          description,
          hourly_rate,
        },
      })
      .then((process) => dispatch(updateProcessSuccess(process)));
  };
}

export function updateProcessSuccess(process) {
  return { type: types.UPDATE_PROCESS_SUCCESS, process };
}

export function addProcess({ name, description, hourly_rate }) {
  return function (dispatch) {
    return axios
      .get(url + `/add`, {
        params: {
          name,
          description,
          hourly_rate,
        },
      })
      .then((process) => dispatch(addProcessSuccess(process)));
  };
}

export function addMultipleProcesses(processes) {
  return function (dispatch) {
    return axios.get(url + `/addMultiple`, {
      params: {
        processes,
      },
    });
  };
}

export function addProcessSuccess(process) {
  return { type: types.ADD_PROCESS_SUCCESS, process };
}

export function deleteProcess(id) {
  return function (dispatch) {
    return axios
      .get(url + `/delete`, {
        params: {
          id,
        },
      })
      .then((id) => dispatch(deleteProcessSuccess(id)));
  };
}

export function deleteProcessSuccess(id) {
  return { type: types.DELETE_PROCESS_SUCCESS, id };
}
