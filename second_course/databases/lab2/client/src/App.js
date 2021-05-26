import React from "react";
import { Route, Switch, Router } from "react-router-dom";
import { history } from "./components/common/history";
import "./App.css";
import Factories from "./components/factories/Factories";
import EditFactory from "./components/factories/EditFactory";
import Shoes from "./components/shoes/Shoes";
import EditShoe from "./components/shoes/EditShoe";
import Laces from "./components/laces/Laces";
import EditLace from "./components/laces/EditLace";
import Processes from "./components/processes/Processes";
import EditProcess from "./components/processes/EditProcess";
import Workers from "./components/workers/Workers";
import EditWorker from "./components/workers/EditWorker";
import AddMultipleWorkers from "./components/workers/AddMultipleWorkers";
import AddMultipleProcesses from "./components/processes/AddMultipleProcesses";
import NavBar from "./components/common/navigation/Navibar";
import HomePage from "./components/common/HomePage";
import Report from "./components/Report";

function App() {
  return (
    <Router history={history}>
      <NavBar />
      <div className="container-fluid"></div>
      <div className="auth-wrapper">
        <div className="auth-inner">
          <Switch>
            <Route exact path="/" component={HomePage} />
            <Route path="/factory/:id" component={EditFactory} />
            <Route path="/factories" component={Factories} />
            <Route path="/shoe/:id" component={EditShoe} />
            <Route path="/shoes" component={Shoes} />
            <Route path="/lace/:id" component={EditLace} />
            <Route path="/laces" component={Laces} />
            <Route path="/worker/addMultiple" component={AddMultipleWorkers} />
            <Route path="/workers" component={Workers} />
            <Route path="/worker/:id" component={EditWorker} />
            <Route
              path="/process/addMultiple"
              component={AddMultipleProcesses}
            />
            <Route path="/process/:id" component={EditProcess} />
            <Route path="/processes" component={Processes} />
            <Route path="/report" component={Report} />
          </Switch>
        </div>
      </div>
    </Router>
  );
}

export default App;
