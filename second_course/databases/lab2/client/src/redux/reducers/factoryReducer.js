import * as types from "../actions/actionTypes";

export default function factoryReducer(state = [], action) {
  switch (action.type) {
    case types.LOAD_FACTORIES_SUCCESS:
      return { ...state, factories: action.factories.data };
    case types.UPDATE_FACTORY_SUCCESS:
      return state.factories.map((factory) =>
        factory.factory_id === action.factory.data.factory_id
          ? action.factory.data
          : factory
      );
    case types.ADD_FACTORY_SUCCESS:
      return state.factories.concat(action.factory.data);
    case types.DELETE_FACTORY_SUCCESS:
      return state.factories.filter(
        (factory) => factory.factory_id !== action.id.data
      );
    default:
      return state;
  }
}
