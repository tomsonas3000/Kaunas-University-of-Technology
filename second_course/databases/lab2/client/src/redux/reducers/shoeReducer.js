import * as types from "../actions/actionTypes";

export default function shoeReducer(state = [], action) {
  switch (action.type) {
    case types.LOAD_SHOES_SUCCESS:
      return { ...state, shoes: action.shoes.data };
    case types.LOAD_ONE_SHOE_SUCCESS:
      return { ...state, oneShoe: action.shoe.data };
    case types.UPDATE_SHOE_SUCCESS:
      return state.shoes.map((shoe) =>
        shoe.shoe_id === action.shoe.data.shoe_id ? action.shoe.data : shoe
      );
    case types.ADD_SHOE_SUCCESS:
      return state.shoes.concat(action.shoe.data);
    case types.DELETE_SHOE_SUCCESS:
      console.log(action.id.data);
      debugger;
      return state.shoes.filter((shoe) => shoe.shoe_id !== action.id.data);
    default:
      return state;
  }
}
