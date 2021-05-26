import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import * as workerActions from "../../redux/actions/workerActions";
import { bindActionCreators } from "redux";
import { history } from "../common/history";
import axios from "axios";

const AddMultipleWorkers = ({ addMultipleWorkers, loadWorkers }) => {
  const [inputWorkers, setWorkers] = useState([{}]);
  const [formValid, setValidation] = useState(false);
  const [factoriesFK, setFactoriesFK] = useState([]);

  useEffect(() => {
    axios
      .get("http://localhost:5000/api/workers/getFKFactories")
      .then((results) => {
        setFactoriesFK(results.data);
      });
  }, []);

  function handleClick() {
    setWorkers(inputWorkers.concat({}));
  }

  function handleChange(event) {
    event.preventDefault();
    const whichElement = event.target.name.slice(event.target.name.length - 1);
    inputWorkers.map((worker, index) => {
      if (index.toString() === whichElement) {
        const field = event.target.name.slice(0, event.target.name.length - 1);
        worker[field] = event.target.value;
      }
    });
  }

  function handleSave(event) {
    setValidation(false);
    console.log(inputWorkers);
    event.preventDefault();
    inputWorkers.map((worker, index) => {
      if (
        typeof worker.name === "undefined" ||
        typeof worker.surname === "undefined" ||
        typeof worker.phone_number === "undefined" ||
        typeof worker.e_mail_address === "undefined" ||
        typeof worker.birth_date === "undefined" ||
        typeof worker.bank_account === "undefined" ||
        typeof worker.fk_FACTORYfactory_id === "undefined"
      ) {
        setValidation(false);
        alert(`Please fill out all fields for ${index + 1} form`);
      } else {
        const regexFloat = /^[-+]?[0-9]+\.[0-9]+$/g;
        const regexInt = /^[0-9]*$/g;
        const regexPhone = /^[0-9|+]*$/g;
        const regexEmail = /^\S+@\S+\.\S+$/g;

        if (!worker.phone_number.match(regexPhone)) {
          setValidation(false);
          alert(`Please input a correct phone number for ${index + 1} form`);
        } else setValidation(true);

        if (!worker.e_mail_address.match(regexEmail)) {
          setValidation(false);
          alert(`Please input a correct email address for ${index + 1} form`);
        } else setValidation(true);

        if (!worker.bank_account.match(regexInt)) {
          setValidation(false);
          alert(`Please input a correct bank account for ${index + 1} form`);
        } else setValidation(true);
      }
    });
    if (formValid) {
      addMultipleWorkers(inputWorkers).catch((error) => {
        console.log(error + "adding workers failed");
      });
      loadWorkers().catch((error) => {
        console.log(error + "loading workers failed");
      });
      history.push("/workers");
    }
  }

  return inputWorkers.length === 0 ? (
    <h1>Loading...</h1>
  ) : (
    <div className="container">
      <h1>Worker</h1>
      {inputWorkers.map((worker, index) => {
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
                  <span>Surname</span>
                  <input
                    type="text"
                    placeholder="surname"
                    name={`surname${index}`}
                    className="form-control"
                    onChange={handleChange}
                    required
                  />
                </label>
              </div>
              <div>
                <label>
                  <span>Phone Number</span>
                  <input
                    type="text"
                    placeholder="phone_number"
                    name={`phone_number${index}`}
                    className="form-control"
                    onChange={handleChange}
                    required
                  />
                </label>
              </div>
              <div>
                <label>
                  <span>E-mail Address</span>
                  <input
                    type="text"
                    placeholder="e_mail_address"
                    name={`e_mail_address${index}`}
                    className="form-control"
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
                    name={`birth_date${index}`}
                    min="1930-01-01"
                    max="2005-01-01"
                    className="form-control"
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
                    name={`bank_account${index}`}
                    className="form-control"
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
                    name={`fk_FACTORYfactory_id${index}`}
                    required
                  >
                    <option value="">-----------------------</option>
                    {factoriesFK.map((FK) => (
                      <option key={FK.factory_id} value={FK.factory_id}>
                        {FK.factory_id} | {FK.city} | {FK.street}
                      </option>
                    ))}
                  </select>
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

export function getWorkerById(workers, id) {
  return workers.find(
    (worker) => worker.worker_id.toString() === id.toString() || null
  );
}

const mapDispatchToProps = (dispatch) => {
  return {
    addMultipleWorkers: bindActionCreators(
      workerActions.addMultipleWorkers,
      dispatch
    ),
    loadWorkers: bindActionCreators(workerActions.loadWorkers, dispatch),
  };
};

export default connect(null, mapDispatchToProps)(AddMultipleWorkers);
