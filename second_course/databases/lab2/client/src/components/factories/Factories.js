import React, { useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as factoryActions from "../../redux/actions/factoryActions";
import { Link } from "react-router-dom";

const Factories = ({ loadFactories, deleteFactory, factories = [] }) => {
  useEffect(() => {
    if (factories.length === 0) loadFactoriesFromDB();
  });

  function loadFactoriesFromDB() {
    loadFactories().catch((error) => {
      alert("loading posts failed" + error);
    });
  }

  function handleDelete(id) {
    deleteFactory(id).catch((error) => {
      alert("deleting post failed" + error);
    });
  }

  return (
    <div className="col">
      <button className="btn btn-primary float-right m-2">
        <Link
          to={"/factory/" + "-1"}
          style={{ textDecoration: "none", color: "white" }}
        >
          Create New
        </Link>
      </button>
      <table className="table ">
        <thead className="thead-dark">
          <tr>
            <th scope="col">ID</th>
            <th scope="col">City</th>
            <th scope="col">Country</th>
            <th scope="col">District</th>
            <th scope="col">Street</th>
            <th scope="col">Telephone number</th>
            <th scope="col">Adress Line</th>
            <th scope="col">Zip Code</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {factories.map((factory) => {
            return (
              <tr key={factory.factory_id}>
                <th scope="row">{factory.factory_id}</th>
                <td>{factory.city}</td>
                <td>{factory.country}</td>
                <td>{factory.district}</td>
                <td>{factory.street}</td>
                <td>{factory.telephone_number}</td>
                <td>{factory.address_line}</td>
                <td>{factory.zip_code}</td>
                <td>
                  <button className="btn btn-warning">
                    <Link
                      to={"/factory/" + factory.factory_id}
                      style={{ textDecoration: "none", color: "white" }}
                    >
                      Edit
                    </Link>
                  </button>
                </td>
                <td>
                  <button
                    className="btn btn-danger"
                    onClick={() => handleDelete(factory.factory_id)}
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
    factories: state.factories.factories,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadFactories: bindActionCreators(factoryActions.loadFactories, dispatch),
    deleteFactory: bindActionCreators(factoryActions.deleteFactory, dispatch),
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(Factories);
