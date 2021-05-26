import React, { useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as workerActions from "../../redux/actions/workerActions";
import { Link } from "react-router-dom";

const Workers = ({ loadWorkers, deleteWorker, workers = [] }) => {
  useEffect(() => {
    if (workers.length === 0) loadWorkersFromDB();
  });
  useEffect(() => {
    if (workers.length === 0) loadWorkersFromDB();
  }, []);

  function loadWorkersFromDB() {
    loadWorkers().catch((error) => {
      alert("loading posts failed" + error);
    });
  }

  function handleDelete(id) {
    deleteWorker(id).catch((error) => {
      alert("deleting post failed" + error);
    });
  }

  return (
    <div className="col">
      <button className="btn btn-secondary float-right m-2">
        <Link
          to={"/worker/" + "addMultiple"}
          style={{ textDecoration: "none", color: "white" }}
        >
          Create multiple
        </Link>
      </button>
      <button className="btn btn-primary float-right m-2">
        <Link
          to={"/worker/" + "-1"}
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
            <th scope="col">Surname</th>
            <th scope="col">Phone number</th>
            <th scope="col">E-mail address</th>
            <th scope="col">Birth Date</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {workers.map((worker) => {
            return (
              <tr key={worker.worker_id}>
                <th scope="row">{worker.worker_id}</th>
                <td>{worker.name}</td>
                <td>{worker.surname}</td>
                <td>{worker.phone_number}</td>
                <td>{worker.e_mail_address}</td>
                <td>{worker.birth_date}</td>
                <td>
                  <button className="btn btn-warning">
                    <Link
                      to={"/worker/" + worker.worker_id}
                      style={{ textDecoration: "none", color: "white" }}
                    >
                      Edit
                    </Link>
                  </button>
                </td>
                <td>
                  <button
                    className="btn btn-danger"
                    onClick={() => handleDelete(worker.worker_id)}
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
    workers: state.workers.workers,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadWorkers: bindActionCreators(workerActions.loadWorkers, dispatch),
    deleteWorker: bindActionCreators(workerActions.deleteWorker, dispatch),
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(Workers);
