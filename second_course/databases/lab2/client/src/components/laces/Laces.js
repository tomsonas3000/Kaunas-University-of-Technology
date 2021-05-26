import React, { useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as laceActions from "../../redux/actions/laceActions";
import { Link } from "react-router-dom";

const Laces = ({ loadLaces, deleteLace, laces = [] }) => {
  useEffect(() => {
    if (laces.length === 0) loadLacesFromDB();
  });

  function loadLacesFromDB() {
    loadLaces().catch((error) => {
      alert("loading posts failed" + error);
    });
  }

  function handleDelete(id) {
    deleteLace(id).catch((error) => {
      alert("deleting post failed" + error);
    });
  }

  return (
    <div className="col">
      <button className="btn btn-primary float-right mb-2">
        <Link
          to={"/lace/" + "-1"}
          style={{ textDecoration: "none", color: "white" }}
        >
          Create New
        </Link>
      </button>
      <table className="table ">
        <thead className="thead-dark">
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Color</th>
            <th scope="col">Material</th>
            <th scope="col">Length</th>
            <th scope="col">Price</th>
            <th scope="col">Shoe ID</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {laces.map((lace) => {
            return (
              <tr key={lace.lace_id}>
                <th scope="row">{lace.lace_id}</th>
                <td>{lace.color}</td>
                <td>{lace.material}</td>
                <td>{lace.length}</td>
                <td>{lace.price}</td>
                <td>{lace.fk_SHOEshoe_id}</td>
                <td>
                  <button className="btn btn-warning">
                    <Link
                      to={"/lace/" + lace.lace_id}
                      style={{ textDecoration: "none", color: "white" }}
                    >
                      Edit
                    </Link>
                  </button>
                </td>
                <td>
                  <button
                    className="btn btn-danger"
                    onClick={() => handleDelete(lace.lace_id)}
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
    laces: state.laces.laces,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadLaces: bindActionCreators(laceActions.loadLaces, dispatch),
    deleteLace: bindActionCreators(laceActions.deleteLace, dispatch),
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(Laces);
