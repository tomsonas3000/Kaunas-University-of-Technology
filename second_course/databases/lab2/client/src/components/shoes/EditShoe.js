import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import * as shoeActions from "../../redux/actions/shoeActions";
import { bindActionCreators } from "redux";
import { history } from "../common/history";
import axios from "axios";

const EditShoe = ({
  id,
  addShoe,
  loadShoes,
  updateShoe,
  shoes = [],
  ...props
}) => {
  const [inputShoe, setShoe] = useState({ ...props.shoe });
  const [shoeTypes, setTypes] = useState([]);
  const [shoeMidsoles, setMidsoles] = useState([]);
  const [shoeEyelets, setEyelets] = useState([]);

  useEffect(() => {
    if (shoes.length === 0) {
      loadShoes().catch((error) => {
        alert("loading shoe failed" + error);
      });
    } else {
      setShoe({ ...props.shoe });
    }
  }, [props.shoe]);

  useEffect(() => {
    axios.get("http://localhost:5000/api/shoes/getTypes").then((results) => {
      setTypes(results.data);
    });
    axios.get("http://localhost:5000/api/shoes/getMidsoles").then((results) => {
      setMidsoles(results.data);
    });
    axios.get("http://localhost:5000/api/shoes/getEyelets").then((results) => {
      setEyelets(results.data);
    });
  }, []);

  function handleChange(event) {
    event.preventDefault();
    console.log(event.target.value);
    const updatedShoe = {
      ...inputShoe,
      [event.target.name]: event.target.value,
    };
    setShoe(updatedShoe);
  }

  function handleSave(event) {
    event.preventDefault();
    if (id !== "-1") {
      updateShoe(inputShoe).catch((error) => {
        console.log(error + "updating shoe failed");
      });

      history.push("/shoes");
    } else {
      addShoe(inputShoe).catch((error) => {
        console.log(error + "updating shoe failed");
      });
      history.push("/shoes");
    }
  }

  return shoes.length === 0 ? (
    <h1>Loading...</h1>
  ) : (
    <div className="container">
      <form onSubmit={handleSave}>
        <h2>Shoe {inputShoe.shoe_id} </h2>
        <div>
          <label>
            <span>Welt</span>
            <input
              type="text"
              placeholder="welt"
              name="welt"
              className="form-control"
              defaultValue={inputShoe.welt}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Size</span>
            <input
              type="text"
              placeholder="size"
              name="size"
              pattern="[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)"
              className="form-control"
              defaultValue={inputShoe.size}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Weight</span>
            <input
              type="text"
              placeholder="weight"
              name="weight"
              pattern="^[0-9]*$"
              className="form-control"
              defaultValue={inputShoe.weight}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Midsole Price</span>
            <input
              type="text"
              placeholder="midsole_price"
              name="midsole_price"
              pattern="[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)"
              className="form-control"
              defaultValue={inputShoe.midsole_price}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Welt price</span>
            <input
              type="text"
              placeholder="welt_price"
              name="welt_price"
              pattern="[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)"
              className="form-control"
              defaultValue={inputShoe.welt_price}
              onChange={handleChange}
              required
            />
          </label>
        </div>

        <div>
          <label>
            <span>Eyelet price</span>
            <input
              type="text"
              placeholder="eyelet_price"
              name="eyelet_price"
              pattern="[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)"
              className="form-control"
              defaultValue={inputShoe.eyelet_price}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Midsoles</span>
            <select
              required
              onChange={handleChange}
              className="form-control"
              name="midsole"
            >
              {id !== "-1" ? null : <option value="">------</option>}
              {shoeMidsoles.map((type) => (
                <option key={type.id_midsoles} value={type.id_midsoles}>
                  {type.name}
                </option>
              ))}
            </select>
          </label>
        </div>
        <div>
          <label>
            <span>Eyelets</span>
            <select
              required
              onChange={handleChange}
              className="form-control"
              name="eyelets"
            >
              {id !== "-1" ? null : <option value="">------</option>}
              {shoeEyelets.map((eyelet) => (
                <option
                  key={eyelet.id_eyelet_types}
                  value={eyelet.id_eyelet_types}
                >
                  {eyelet.name}
                </option>
              ))}
            </select>
          </label>
        </div>
        <div>
          <label>
            <span>Types</span>
            <select
              required
              onChange={handleChange}
              className="form-control"
              name="type"
            >
              {id !== "-1" ? null : <option value="">------</option>}
              {shoeTypes.map((type) => (
                <option key={type.id_shoe_types} value={type.id_shoe_types}>
                  {type.name}
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

export function getShoeById(shoes, id) {
  return shoes.find(
    (shoe) => shoe.shoe_id.toString() === id.toString() || null
  );
}

const mapStateToProps = (state, ownProps) => {
  const id = ownProps.match.params.id;
  const shoe =
    id && state.shoes.shoes && state.shoes.shoes.length > 0
      ? getShoeById(state.shoes.shoes, id)
      : null;
  return {
    id,
    shoe,
    shoes: state.shoes.shoes,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadShoes: bindActionCreators(shoeActions.loadShoes, dispatch),
    updateShoe: bindActionCreators(shoeActions.updateShoe, dispatch),
    addShoe: bindActionCreators(shoeActions.addShoe, dispatch),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(EditShoe);
