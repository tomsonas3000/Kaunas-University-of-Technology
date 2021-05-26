import * as types from "../actions/actionTypes";

export default function laceReducer(state = [], action) {
  switch (action.type) {
    case types.LOAD_LACES_SUCCESS:
      return { ...state, laces: action.laces.data };
    case types.UPDATE_LACE_SUCCESS:
      return state.laces.map((lace) =>
        lace.lace_id === action.lace.data.lace_id ? action.lace.data : lace
      );
    case types.ADD_LACE_SUCCESS:
      return state.laces.concat(action.lace.data);
    case types.DELETE_LACE_SUCCESS:
      return state.laces.filter((lace) => lace.lace_id !== action.id.data);
    default:
      return state;
  }
}
