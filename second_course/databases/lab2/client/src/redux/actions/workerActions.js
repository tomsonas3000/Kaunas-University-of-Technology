import * as types from "./actionTypes";
import axios from "axios";

const url = "http://localhost:5000/api/workers";

export function loadWorkers() {
  return function (dispatch) {
    return axios
      .get(url + `/getWorkers`)
      .then((workers) => dispatch(loadWorkersSuccess(workers)));
  };
}

export function loadWorkersSuccess(workers) {
  return { type: types.LOAD_WORKERS_SUCCESS, workers };
}

export function updateWorker({
  worker_id,
  name,
  surname,
  phone_number,
  e_mail_address,
  birth_date,
  bank_account,
  fk_FACTORYfactory_id,
}) {
  return function (dispatch) {
    return axios
      .get(url + `/update`, {
        params: {
          worker_id,
          name,
          surname,
          phone_number,
          e_mail_address,
          birth_date,
          bank_account,
          fk_FACTORYfactory_id,
        },
      })
      .then((worker) => dispatch(updateWorkerSuccess(worker)));
  };
}

export function updateWorkerSuccess(worker) {
  return { type: types.UPDATE_WORKER_SUCCESS, worker };
}

export function addWorker({
  name,
  surname,
  phone_number,
  e_mail_address,
  bank_account,
  birth_date,
  fk_FACTORYfactory_id,
}) {
  debugger;
  return function (dispatch) {
    return axios
      .get(url + `/add`, {
        params: {
          name,
          surname,
          phone_number,
          e_mail_address,
          birth_date,
          bank_account,
          fk_FACTORYfactory_id,
        },
      })
      .then((worker) => dispatch(addWorkerSuccess(worker)));
  };
}

export function addWorkerSuccess(worker) {
  return { type: types.ADD_WORKER_SUCCESS, worker };
}

export function addMultipleWorkers(workers) {
  return function (dispatch) {
    return axios.get(url + `/addMultiple`, {
      params: {
        workers,
      },
    });
  };
}

export function deleteWorker(id) {
  return function (dispatch) {
    return axios
      .get(url + `/delete`, {
        params: {
          id,
        },
      })
      .then((id) => dispatch(deleteWorkerSuccess(id)));
  };
}

export function deleteWorkerSuccess(id) {
  return { type: types.DELETE_WORKER_SUCCESS, id };
}
