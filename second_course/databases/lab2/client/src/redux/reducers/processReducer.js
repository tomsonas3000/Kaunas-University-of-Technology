import * as types from "../actions/actionTypes";

export default function processReducer(state = [], action) {
  switch (action.type) {
    case types.LOAD_PROCESSES_SUCCESS:
      return { ...state, processes: action.processes.data };
    case types.UPDATE_PROCESS_SUCCESS:
      return state.processes.map((process) =>
        process.process_id === action.process.data.process_id
          ? action.process.data
          : process
      );
    case types.ADD_PROCESS_SUCCESS:
      return state.processes.concat(action.process.data);
    case types.DELETE_PROCESS_SUCCESS:
      return state.processes.filter(
        (process) => process.process_id !== action.id.data
      );
    default:
      return state;
  }
}
