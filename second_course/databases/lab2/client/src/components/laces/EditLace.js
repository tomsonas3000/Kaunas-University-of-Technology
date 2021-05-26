import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import * as laceActions from "../../redux/actions/laceActions";
import { bindActionCreators } from "redux";
import { history } from "../common/history";
import axios from "axios";

const EditLace = ({
  id,
  addLace,
  loadLaces,
  updateLace,
  laces = [],
  ...props
}) => {
  const [inputLace, setLace] = useState({ ...props.lace });
  const [shoesFK, setShoesFK] = useState([]);

  useEffect(() => {
    if (laces.length === 0) {
      loadLaces().catch((error) => {
        alert("loading lace failed" + error);
      });
    } else {
      setLace({ ...props.lace });
    }
  }, [props.lace]);

  useEffect(() => {
    axios.get("http://localhost:5000/api/laces/getFKShoes").then((results) => {
      setShoesFK(results.data);
    });
  }, []);

  function handleChange(event) {
    event.preventDefault();
    console.log(event.target.value);
    const updatedLace = {
      ...inputLace,
      [event.target.name]: event.target.value,
    };
    setLace(updatedLace);
  }

  function handleSave(event) {
    event.preventDefault();
    if (id !== "-1") {
      updateLace(inputLace).catch((error) => {
        console.log(error + "updating lace failed");
      });

      history.push("/laces");
    } else {
      addLace(inputLace).catch((error) => {
        console.log(error + "updating lace failed");
      });
      history.push("/laces");
    }
  }

  return laces.length === 0 ? (
    <h1>Loading...</h1>
  ) : (
    <div className="container">
      <form onSubmit={handleSave}>
        <h2>Lace {inputLace.lace_id} </h2>
        <div>
          <label>
            <span>Color</span>
            <input
              type="text"
              placeholder="color"
              name="color"
              className="form-control"
              defaultValue={inputLace.color}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Material</span>
            <input
              type="text"
              placeholder="material"
              name="material"
              className="form-control"
              defaultValue={inputLace.material}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Length</span>
            <input
              type="text"
              placeholder="length"
              name="length"
              pattern="[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)"
              className="form-control"
              defaultValue={inputLace.length}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Price</span>
            <input
              type="text"
              placeholder="price"
              name="price"
              pattern="[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)"
              className="form-control"
              defaultValue={inputLace.price}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Shoe INFO</span>
            <select
              onChange={handleChange}
              className="form-control"
              name="fk_SHOEshoe_id"
              required
            >
              {id !== "-1" ? null : <option value="">--------------</option>}
              {shoesFK.map((FK) => (
                <option key={FK.shoe_id} value={FK.shoe_id}>
                  {FK.shoe_id} | {FK.welt} | {FK.size}
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

export function getLaceById(laces, id) {
  return laces.find(
    (lace) => lace.lace_id.toString() === id.toString() || null
  );
}

const mapStateToProps = (state, ownProps) => {
  const id = ownProps.match.params.id;
  const lace =
    id && state.laces.laces && state.laces.laces.length > 0
      ? getLaceById(state.laces.laces, id)
      : null;
  return {
    id,
    lace,
    laces: state.laces.laces,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadLaces: bindActionCreators(laceActions.loadLaces, dispatch),
    updateLace: bindActionCreators(laceActions.updateLace, dispatch),
    addLace: bindActionCreators(laceActions.addLace, dispatch),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(EditLace);
