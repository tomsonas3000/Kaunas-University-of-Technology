import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import * as processActions from "../../redux/actions/processActions";
import { bindActionCreators } from "redux";
import { history } from "../common/history";

const EditProcess = ({ addMultipleProcesses }) => {
  const [inputProcesses, setProcesses] = useState([{}]);
  const [formValid, setValidation] = useState(false);

  function handleClick() {
    setProcesses(inputProcesses.concat({}));
  }

  function handleChange(event) {
    event.preventDefault();
    const whichElement = event.target.name.slice(event.target.name.length - 1);
    inputProcesses.map((process, index) => {
      if (index.toString() === whichElement) {
        const field = event.target.name.slice(0, event.target.name.length - 1);
        process[field] = event.target.value;
      }
    });
  }

  function handleSave(event) {
    setValidation(true);
    console.log(inputProcesses);
    event.preventDefault();
    inputProcesses.map((process) => {
      if (
        typeof process.name === "undefined" ||
        typeof process.description === "undefined" ||
        typeof process.hourly_rate === "undefined"
      ) {
        setValidation(false);
        alert("Please fill out all fields");
      } else {
        const regexFloat = /^[-+]?[0-9]+\.[0-9]+$/g;
        const regexInt = /^[0-9]*$/g;
        if (
          process.hourly_rate.match(regexFloat) ||
          process.hourly_rate.match(regexInt)
        ) {
          setValidation(true);
        } else {
          setValidation(false);
          alert("Please input a number into the hourly rate field");
        }
      }
    });
    if (formValid) {
      addMultipleProcesses(inputProcesses).catch((error) => {
        console.log(error + "adding processes failed");
      });
      history.push("/processes");
    }
  }

  return inputProcesses.length === 0 ? (
    <h1>Loading...</h1>
  ) : (
    <div className="container">
      <h1>Process</h1>
      {inputProcesses.map((process, index) => {
        return (
          <>
            <form>
              <div>
                <label>
                  <span>Name</span>
                  <input
                    type="text"
                    placeholder="name"
                    name={`name${index}`}
                    className="form-control"
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
                    name={`description${index}`}
                    className="form-control"
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
                    name={`hourly_rate${index}`}
                    pattern="[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)"
                    className="form-control"
                    onChange={handleChange}
                    required
                  />
                </label>
              </div>
            </form>
            <h1>&nbsp;</h1>
          </>
        );
      })}
      <div className="row">
        <div className="col-lg-8">
          <button className="btn btn-outline-primary" onClick={handleClick}>
            Add more
          </button>
        </div>
        <div className="col-lg-4">
          <button className="btn btn-success" onClick={handleSave}>
            Save
          </button>
        </div>
      </div>
    </div>
  );
};

export function getProcessById(processes, id) {
  return processes.find(
    (process) => process.process_id.toString() === id.toString() || null
  );
}

const mapDispatchToProps = (dispatch) => {
  return {
    addMultipleProcesses: bindActionCreators(
      processActions.addMultipleProcesses,
      dispatch
    ),
  };
};

export default connect(null, mapDispatchToProps)(EditProcess);
