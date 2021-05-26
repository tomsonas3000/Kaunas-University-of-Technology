import * as types from "../actions/actionTypes";

export default function workerReducer(state = [], action) {
  switch (action.type) {
    case types.LOAD_WORKERS_SUCCESS:
      return { ...state, workers: action.workers.data };
    case types.UPDATE_WORKER_SUCCESS:
      return state.workers.map((worker) =>
        worker.worker_id === action.worker.data.worker_id
          ? action.worker.data
          : worker
      );
    case types.ADD_WORKER_SUCCESS:
      return state.workers.concat(action.worker.data);
    case types.DELETE_WORKER_SUCCESS:
      return state.workers.filter(
        (worker) => worker.worker_id !== action.id.data
      );
    default:
      return state;
  }
}
