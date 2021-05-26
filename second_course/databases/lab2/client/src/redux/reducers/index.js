import { combineReducers } from "redux";
import factories from "./factoryReducer";
import shoes from "./shoeReducer";
import laces from "./laceReducer";
import processes from "./processReducer";
import workers from "./workerReducer";

const rootReducer = combineReducers({
  factories,
  shoes,
  laces,
  processes,
  workers,
});

export default rootReducer;
