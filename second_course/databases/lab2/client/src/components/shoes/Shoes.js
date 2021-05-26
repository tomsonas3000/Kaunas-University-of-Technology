import React, { useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as shoeActions from "../../redux/actions/shoeActions";
import { Link } from "react-router-dom";

const Shoes = ({ loadShoes, deleteShoe, shoes = [] }) => {
  useEffect(() => {
    if (shoes.length === 0) loadShoesFromDB();
  });

  function loadShoesFromDB() {
    loadShoes().catch((error) => {
      alert("loading shoes failed" + error);
    });
  }

  function handleDelete(id) {
    deleteShoe(id).catch((error) => {
      alert("deleting shoe failed" + error);
    });
  }

  return (
    <div className="col">
      <button className="btn btn-primary float-right mb-2">
        <Link
          to={"/shoe/" + "-1"}
          style={{ textDecoration: "none", color: "white" }}
        >
          Create New
        </Link>
      </button>
      <table className="table ">
        <thead className="thead-dark">
          <tr>
            <th scope="col">ID</th>
            <th scope="col">Welt</th>
            <th scope="col">Size</th>
            <th scope="col">Weight</th>
            <th scope="col">Midsole Price</th>
            <th scope="col">Welt price</th>
            <th scope="col">Eyelet price</th>
            <th scope="col">Midsole</th>
            <th scope="col">Eyelets</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {shoes.map((shoe) => {
            return (
              <tr key={shoe.shoe_id}>
                <th scope="row">{shoe.shoe_id}</th>
                <td>{shoe.welt}</td>
                <td>{shoe.size}</td>
                <td>{shoe.weight}</td>
                <td>{shoe.midsole_price}</td>
                <td>{shoe.welt_price}</td>
                <td>{shoe.eyelet_price}</td>
                <td>{shoe.midsole}</td>
                <td>{shoe.eyelets}</td>
                <td>
                  <button className="btn btn-warning">
                    <Link
                      to={"/shoe/" + shoe.shoe_id}
                      style={{ textDecoration: "none", color: "white" }}
                    >
                      Edit
                    </Link>
                  </button>
                </td>
                <td>
                  <button
                    className="btn btn-danger"
                    onClick={() => handleDelete(shoe.shoe_id)}
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
    shoes: state.shoes.shoes,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadShoes: bindActionCreators(shoeActions.loadShoes, dispatch),
    deleteShoe: bindActionCreators(shoeActions.deleteShoe, dispatch),
  };
};
export default connect(mapStateToProps, mapDispatchToProps)(Shoes);
