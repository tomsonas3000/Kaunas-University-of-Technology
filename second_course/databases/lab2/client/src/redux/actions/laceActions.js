import * as types from "./actionTypes";
import axios from "axios";

const url = "http://localhost:5000/api/laces";

export function loadLaces() {
  return function (dispatch) {
    return axios
      .get(url + `/getLaces`)
      .then((laces) => dispatch(loadLacesSuccess(laces)));
  };
}

export function loadLacesSuccess(laces) {
  return { type: types.LOAD_LACES_SUCCESS, laces };
}

export function updateLace({
  lace_id,
  color,
  material,
  length,
  price,
  fk_SHOEshoe_id,
}) {
  return function (dispatch) {
    return axios
      .get(url + `/update`, {
        params: {
          lace_id,
          color,
          material,
          length,
          price,
          fk_SHOEshoe_id,
        },
      })
      .then((lace) => dispatch(updateLaceSuccess(lace)));
  };
}

export function updateLaceSuccess(lace) {
  return { type: types.UPDATE_LACE_SUCCESS, lace };
}

export function addLace({ color, material, length, price, fk_SHOEshoe_id }) {
  return function (dispatch) {
    return axios
      .get(url + `/add`, {
        params: {
          color,
          material,
          length,
          price,
          fk_SHOEshoe_id,
        },
      })
      .then((lace) => dispatch(addLaceSuccess(lace)));
  };
}

export function addLaceSuccess(lace) {
  return { type: types.ADD_LACE_SUCCESS, lace };
}

export function deleteLace(id) {
  return function (dispatch) {
    return axios
      .get(url + `/delete`, {
        params: {
          id,
        },
      })
      .then((id) => dispatch(deleteLaceSuccess(id)));
  };
}

export function deleteLaceSuccess(id) {
  return { type: types.DELETE_LACE_SUCCESS, id };
}
