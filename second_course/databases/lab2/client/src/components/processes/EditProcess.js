import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import * as processActions from "../../redux/actions/processActions";
import { bindActionCreators } from "redux";
import { history } from "../common/history";

const EditProcess = ({
  id,
  addProcess,
  loadProcesses,
  updateProcess,
  processes = [],
  ...props
}) => {
  const [inputProcess, setProcess] = useState({ ...props.process });

  useEffect(() => {
    if (processes.length === 0) {
      loadProcesses().catch((error) => {
        alert("loading processes failed" + error);
      });
    } else {
      setProcess({ ...props.process });
    }
  }, [props.process]);

  function handleChange(event) {
    console.log(event.target.value);
    event.preventDefault();
    const updatedProcess = {
      ...inputProcess,
      [event.target.name]: event.target.value,
    };
    setProcess(updatedProcess);
  }

  function handleSave(event) {
    event.preventDefault();
    if (id !== "-1") {
      updateProcess(inputProcess).catch((error) => {
        console.log(error + "updating process failed");
      });

      history.push("/processes");
    } else {
      addProcess(inputProcess).catch((error) => {
        console.log(error + "updating process failed");
      });
      history.push("/processes");
    }
  }

  return processes.length === 0 ? (
    <h1>Loading...</h1>
  ) : (
    <div className="container">
      <form onSubmit={handleSave}>
        <h2>Process {inputProcess.process_id} </h2>
        <div>
          <label>
            <span>Name</span>
            <input
              type="text"
              placeholder="name"
              name="name"
              className="form-control"
              defaultValue={inputProcess.name}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Description</span>
            <input
              type="text"
              placeholder="description"
              name="description"
              className="form-control"
              defaultValue={inputProcess.description}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Hourly Rate</span>
            <input
              type="text"
              placeholder="hourly_rate"
              name="hourly_rate"
              pattern="[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)"
              className="form-control"
              defaultValue={inputProcess.hourly_rate}
              onChange={handleChange}
              required
            />
          </label>
        </div>

        <button className="btn btn-info" type="submit">
          Save
        </button>
      </form>
    </div>
  );
};

export function getProcessById(processes, id) {
  return processes.find(
    (process) => process.process_id.toString() === id.toString() || null
  );
}

const mapStateToProps = (state, ownProps) => {
  const id = ownProps.match.params.id;
  const process =
    id && state.processes.processes && state.processes.processes.length > 0
      ? getProcessById(state.processes.processes, id)
      : null;
  return {
    id,
    process,
    processes: state.processes.processes,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadProcesses: bindActionCreators(processActions.loadProcesses, dispatch),
    updateProcess: bindActionCreators(processActions.updateProcess, dispatch),
    addProcess: bindActionCreators(processActions.addProcess, dispatch),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(EditProcess);
