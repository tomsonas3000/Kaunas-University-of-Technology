import * as types from "./actionTypes";
import axios from "axios";

const url = "http://localhost:5000/api/factories";

export function loadFactories() {
  return function (dispatch) {
    return axios
      .get(url + `/getFactories`)
      .then((factories) => dispatch(loadFactoriesSuccess(factories)));
  };
}

export function loadFactoriesSuccess(factories) {
  return { type: types.LOAD_FACTORIES_SUCCESS, factories };
}

export function updateFactory({
  factory_id,
  city,
  country,
  district,
  street,
  telephone_number,
  address_line,
  zip_code,
}) {
  return function (dispatch) {
    return axios
      .get(url + `/update`, {
        params: {
          factory_id,
          city,
          country,
          district,
          street,
          telephone_number,
          address_line,
          zip_code,
        },
      })
      .then((factory) => dispatch(updateFactorySuccess(factory)));
  };
}

export function updateFactorySuccess(factory) {
  return { type: types.UPDATE_FACTORY_SUCCESS, factory };
}

export function addFactory({
  city,
  country,
  district,
  street,
  telephone_number,
  address_line,
  zip_code,
}) {
  return function (dispatch) {
    return axios
      .get(url + `/add`, {
        params: {
          city,
          country,
          district,
          street,
          telephone_number,
          address_line,
          zip_code,
        },
      })
      .then((factory) => dispatch(addFactorySuccess(factory)));
  };
}

export function addFactorySuccess(factory) {
  return { type: types.ADD_FACTORY_SUCCESS, factory };
}

export function deleteFactory(id) {
  return function (dispatch) {
    return axios
      .get(url + `/delete`, {
        params: {
          id,
        },
      })
      .then((id) => dispatch(deleteFactorySuccess(id)));
  };
}

export function deleteFactorySuccess(id) {
  return { type: types.DELETE_FACTORY_SUCCESS, id };
}
