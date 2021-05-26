// include nact module
const { start, dispatch, spawnStateless, spawn, stop } = require("nact");
// include file system module
const fs = require("fs");
// include action types
const actionTypes = require("./actionTypes.js");
// include random-item module for selecting random element from array
const randomItem = require("random-item");
// include easy-table module for neat printing
const Table = require("easy-table");
// include JSON file with data
const players = require("./IFF-8-11_JorudasT_dat_3.json");

// start actor system
const system = start();

const numberOfWorkerActors = 5;

// spawn the manager actor
const manager = spawn(
  // "parent" of the actor
  system,
  // initial state, message passed to the actor
  (state = { counter: 0 }, msg) => {
    // distributes messages
    switch (msg.type) {
      case actionTypes.GET_FROM_MAIN:
        // send player to worker actor
        dispatch(randomItem(workerArray), {
          player: msg.player,
        });
        return state;
      case actionTypes.GET_FROM_WORKER:
        // in case you want to check from which worker the data came
        // console.log(`Which worker: ${msg.worker.name}`);
        // send player to results actor
        dispatch(results, { player: msg.player });
        return state;
      case actionTypes.INCREASE_COUNTER:
        if (state.counter + 1 === players.length) {
          // if all of the players are filtered, send message to results actor
          dispatch(results, { type: actionTypes.SEND_RESULTS_TO_MANAGER });
        }
        // update counter of how many players were checked
        return { ...state, counter: state.counter + 1 };
      case actionTypes.GET_RESULTS:
        // send the results to printer actor
        dispatch(printer, { results: msg.results });
        return state;
      default:
        return state;
    }
  },
  //name of the actor
  "manager"
);

// create an array of worker actors
const workerArray = new Array(numberOfWorkerActors)
  // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/fill
  .fill(0, 0, numberOfWorkerActors)
  .map((_, index) => {
    // spawn n of worker actors
    return spawnStateless(
      // "parent of the actor"
      manager,
      // message passed to the actor, information about the actor
      (msg, ctx) => {
        // calculate points per shot
        const pps = msg.player.ppg / msg.player.shotsTaken;
        msg.player.pointsPerShot = pps;
        // check if the player passes the condition
        if (pps > 5) {
          // send player to manager actor
          dispatch(manager, {
            type: actionTypes.GET_FROM_WORKER,
            player: msg.player,
            worker: ctx.self,
          });
        }
        // send message to manager actor that a player was checked
        dispatch(manager, {
          type: actionTypes.INCREASE_COUNTER,
        });
      },
      // name of the actor
      `worker${index + 1}`
    );
  });

// spawn results actor
const results = spawn(
  // "parent" of the actor
  manager,
  // initial state, message passed to the actor
  (state = [], msg) => {
    // check whether all of the players were checked, send the results to manager actor
    if (msg.type === actionTypes.SEND_RESULTS_TO_MANAGER) {
      dispatch(manager, { type: actionTypes.GET_RESULTS, results: state });
      return state;
    }
    // extract points per shot from players array
    const ppsArray = state.map((player) => player.pointsPerShot);
    // find the index where player should be inserted
    const insertIndex = sortedIndex(ppsArray, msg.player.pointsPerShot);
    // insert the player into results array
    state.splice(insertIndex, 0, msg.player);
    return state;
  },
  // actor name
  "results"
);

//spawn printer actor
const printer = spawnStateless(
  // "parent" of the actor
  manager,
  // message passed to the actor
  (msg) => {
    // printing stuff
    const tableInitial = new Table();
    players.forEach((p, index) => {
      tableInitial.cell("Index", index + 1);
      tableInitial.cell("Name", p.name);
      tableInitial.cell("Shot taken", p.shotsTaken.toFixed(2));
      tableInitial.cell("Pointer per game", p.ppg.toFixed(2));
      tableInitial.newRow();
    });
    const tableResults = new Table();
    msg.results.forEach((p, index) => {
      tableResults.cell("Index", index + 1);
      tableResults.cell("Name", p.name);
      tableResults.cell("Shot taken", p.shotsTaken.toFixed(2));
      tableResults.cell("Pointer per game", p.ppg.toFixed(2));
      tableResults.cell("Points per shot", p.pointsPerShot.toFixed(2));
      tableResults.newRow();
    });
    const stringToPrint = "Initial data:\n".concat(
      tableInitial.toString(),
      "\nResults:\n",
      tableResults.toString()
    );
    // writing to file
    fs.writeFile("IFF-8-11_JorudasT_rez.txt", stringToPrint, (err) => {
      if (err) throw err;
    });
  },
  // actor name
  "printer"
);

// find where value should be inserted
const sortedIndex = (array, value) => {
  let low = 0,
    high = array.length;

  while (low < high) {
    let mid = (low + high) >>> 1;
    if (array[mid] < value) low = mid + 1;
    else high = mid;
  }
  return low;
};

// send each player to manager actor
players.forEach((player) => {
  dispatch(manager, {
    type: actionTypes.GET_FROM_MAIN,
    player: player,
  });
});
