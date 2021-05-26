import * as types from "./actionTypes";
import axios from "axios";

const url = "http://localhost:5000/api/shoes";

export function loadShoes() {
  return function (dispatch) {
    return axios
      .get(url + `/getShoes`)
      .then((shoes) => dispatch(loadShoesSuccess(shoes)));
  };
}

export function loadShoesSuccess(shoes) {
  return { type: types.LOAD_SHOES_SUCCESS, shoes };
}

export function loadOneShoe(id) {
  return function (dispatch) {
    return axios
      .get(url + `/getOne`, {
        params: {
          id,
        },
      })
      .then((shoe) => dispatch(loadOneShoeSuccess(shoe)));
  };
}

export function loadOneShoeSuccess(shoe) {
  return { type: types.LOAD_ONE_FACTORY_SUCCESS, shoe };
}

export function updateShoe({
  shoe_id,
  welt,
  size,
  weight,
  midsole_price,
  welt_price,
  eyelet_price,
  midsole,
  eyelets,
  type,
}) {
  return function (dispatch) {
    return axios
      .get(url + `/update`, {
        params: {
          shoe_id,
          welt,
          size,
          weight,
          midsole_price,
          welt_price,
          eyelet_price,
          midsole,
          eyelets,
          type,
        },
      })
      .then((shoe) => dispatch(updateShoeSuccess(shoe)));
  };
}

export function updateShoeSuccess(shoe) {
  debugger;
  return { type: types.UPDATE_SHOE_SUCCESS, shoe };
}

export function addShoe({
  welt,
  size,
  weight,
  midsole_price,
  welt_price,
  eyelet_price,
  midsole,
  eyelets,
  type,
}) {
  return function (dispatch) {
    return axios
      .get(url + `/add`, {
        params: {
          welt,
          size,
          weight,
          midsole_price,
          welt_price,
          eyelet_price,
          midsole,
          eyelets,
          type,
        },
      })
      .then((shoe) => dispatch(addShoeSuccess(shoe)));
  };
}

export function addShoeSuccess(shoe) {
  return { type: types.ADD_SHOE_SUCCESS, shoe };
}

export function deleteShoe(id) {
  return function (dispatch) {
    return axios
      .get(url + `/delete`, {
        params: {
          id,
        },
      })
      .then((id) => dispatch(deleteShoeSuccess(id)));
  };
}

export function deleteShoeSuccess(id) {
  return { type: types.DELETE_SHOE_SUCCESS, id };
}
