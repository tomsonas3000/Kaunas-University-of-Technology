import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import * as workerActions from "../../redux/actions/workerActions";
import { bindActionCreators } from "redux";
import { history } from "../common/history";
import axios from "axios";

const EditWorker = ({
  id,
  addWorker,
  loadWorkers,
  updateWorker,
  workers = [],
  ...props
}) => {
  const [inputWorker, setWorker] = useState({ ...props.worker });
  const [factoriesFK, setFactoriesFK] = useState([]);

  useEffect(() => {
    if (workers.length === 0) {
      loadWorkers().catch((error) => {
        alert("loading worker failed" + error);
      });
    } else {
      setWorker({ ...props.worker });
    }
  }, [props.worker]);

  useEffect(() => {
    axios
      .get("http://localhost:5000/api/workers/getFKFactories")
      .then((results) => {
        setFactoriesFK(results.data);
      });
  }, []);

  function handleChange(event) {
    event.preventDefault();
    console.log(event.target.value);
    const updatedWorker = {
      ...inputWorker,
      [event.target.name]: event.target.value,
    };
    setWorker(updatedWorker);
  }

  function handleSave(event) {
    event.preventDefault();
    debugger;
    if (id !== "-1") {
      updateWorker(inputWorker).catch((error) => {
        console.log(error + "updating worker failed");
      });

      history.push("/workers");
    } else {
      addWorker(inputWorker).catch((error) => {
        console.log(error + "updating worker failed");
      });
      history.push("/workers");
    }
  }

  return workers.length === 0 ? (
    <h1>Loading...</h1>
  ) : (
    <div className="container">
      <form onSubmit={handleSave}>
        <h2>Worker {inputWorker.worker_id} </h2>
        <div>
          <label>
            <span>Name</span>
            <input
              type="text"
              placeholder="name"
              name="name"
              className="form-control"
              defaultValue={inputWorker.name}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Surname</span>
            <input
              type="text"
              placeholder="surname"
              name="surname"
              className="form-control"
              defaultValue={inputWorker.surname}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Phone Number</span>
            <input
              type="tel"
              placeholder="phone_number"
              name="phone_number"
              pattern="^[0-9|+]*$"
              className="form-control"
              defaultValue={inputWorker.phone_number}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>E-mail Address</span>
            <input
              type="email"
              placeholder="e_mail_address"
              name="e_mail_address"
              className="form-control"
              defaultValue={inputWorker.e_mail_address}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Birth Date</span>
            <input
              type="date"
              placeholder="birth_date"
              name="birth_date"
              min="1930-01-01"
              max="2005-01-01"
              className="form-control"
              defaultValue={inputWorker.birth_date}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Bank Account</span>
            <input
              type="text"
              placeholder="bank_account"
              name="bank_account"
              pattern="^[0-9]*$"
              className="form-control"
              defaultValue={inputWorker.bank_account}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Factory INFO</span>
            <select
              onChange={handleChange}
              className="form-control"
              name="fk_FACTORYfactory_id"
              required
            >
              {id !== "-1" ? null : (
                <option value="">-----------------------</option>
              )}
              {factoriesFK.map((FK) => (
                <option key={FK.factory_id} value={FK.factory_id}>
                  {FK.factory_id} | {FK.city} | {FK.street}
                </option>
              ))}
            </select>
          </label>
        </div>
        <button className="btn btn-info" type="submit">
          Save
        </button>
      </form>
    </div>
  );
};

export function getWorkerById(workers, id) {
  return workers.find(
    (worker) => worker.worker_id.toString() === id.toString() || null
  );
}

const mapStateToProps = (state, ownProps) => {
  const id = ownProps.match.params.id;
  const worker =
    id && state.workers.workers && state.workers.workers.length > 0
      ? getWorkerById(state.workers.workers, id)
      : null;
  return {
    id,
    worker,
    workers: state.workers.workers,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadWorkers: bindActionCreators(workerActions.loadWorkers, dispatch),
    updateWorker: bindActionCreators(workerActions.updateWorker, dispatch),
    addWorker: bindActionCreators(workerActions.addWorker, dispatch),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(EditWorker);
