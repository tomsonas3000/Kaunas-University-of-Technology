import React, { useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as processActions from "../../redux/actions/processActions";
import { Link } from "react-router-dom";

const Processes = ({ loadProcesses, deleteProcess, processes = [] }) => {
  useEffect(() => {
    loadProcessesFromDB();
  }, [deleteProcess]);
  useEffect(() => {
    if (processes.length === 0) loadProcessesFromDB();
  });

  function loadProcessesFromDB() {
    loadProcesses().catch((error) => {
      alert("loading processes failed" + error);
    });
  }

  function handleDelete(id) {
    deleteProcess(id).catch((error) => {
      alert("deleting process failed" + error);
    });
  }

  return (
    <div className="col">
      <button className="btn btn-secondary float-right m-2">
        <Link
          to={"/process/" + "addMultiple"}
          style={{ textDecoration: "none", color: "white" }}
        >
          Create multiple
        </Link>
      </button>
      <button className="btn btn-primary float-right m-2">
        <Link
          to={"/process/" + "-1"}
          style={{ textDecoration: "none", color: "white" }}
        >
          Create New
        </Link>
      </button>
      <table className="table ">
        <thead className="thead-dark">
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Name</th>
            <th scope="col">Description</th>
            <th scope="col">Hourly Rate</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {processes.map((process) => {
            return (
              <tr key={process.process_id}>
                <th scope="row">{process.process_id}</th>
                <td>{process.name}</td>
                <td>{process.description}</td>
                <td>{process.hourly_rate}</td>
                <td>
                  <button className="btn btn-warning">
                    <Link
                      to={"/process/" + process.process_id}
                      style={{ textDecoration: "none", color: "white" }}
                    >
                      Edit
                    </Link>
                  </button>
                </td>
                <td>
                  <button
                    className="btn btn-danger"
                    onClick={() => handleDelete(process.process_id)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
};

const mapStateToProps = (state) => {
  return {
    processes: state.processes.processes,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadProcesses: bindActionCreators(processActions.loadProcesses, dispatch),
    deleteProcess: bindActionCreators(processActions.deleteProcess, dispatch),
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(Processes);
