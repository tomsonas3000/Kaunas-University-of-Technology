import React, { useEffect, useState } from "react";
import { connect } from "react-redux";
import * as factoryActions from "../../redux/actions/factoryActions";
import { bindActionCreators } from "redux";
import { history } from "../common/history";

const EditFactory = ({
  id,
  addFactory,
  loadFactories,
  updateFactory,
  factories = [],
  ...props
}) => {
  const [inputFactory, setFactory] = useState({ ...props.factory });

  useEffect(() => {
    if (factories.length === 0) {
      loadFactories().catch((error) => {
        alert("loading factory failed" + error);
      });
    } else {
      setFactory({ ...props.factory });
    }
  }, [props.factory]);

  function handleChange(event) {
    event.preventDefault();
    const updatedFactory = {
      ...inputFactory,
      [event.target.name]: event.target.value,
    };
    setFactory(updatedFactory);
  }

  function handleSave(event) {
    event.preventDefault();
    if (id !== "-1") {
      updateFactory(inputFactory).catch((error) => {
        console.log(error + "updating factory failed");
      });

      history.push("/factories");
    } else {
      addFactory(inputFactory).catch((error) => {
        console.log(error + "updating factory failed");
      });
      history.push("/factories");
    }
  }

  return factories.length === 0 ? (
    <h1>Loading...</h1>
  ) : (
    <div className="container">
      <form onSubmit={handleSave}>
        <h2>Factory {inputFactory.factory_id} </h2>
        <div>
          <label>
            <span>City</span>
            <input
              type="text"
              placeholder="city"
              name="city"
              className="form-control"
              defaultValue={inputFactory.city}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Country</span>
            <input
              type="text"
              placeholder="country"
              name="country"
              className="form-control"
              defaultValue={inputFactory.country}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>District</span>
            <input
              type="text"
              placeholder="district"
              name="district"
              className="form-control"
              defaultValue={inputFactory.district}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Street</span>
            <input
              type="text"
              placeholder="street"
              name="street"
              className="form-control"
              defaultValue={inputFactory.street}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Telephone Number</span>
            <input
              type="tel"
              placeholder="telephone_number"
              pattern="^[0-9|+]*$"
              name="telephone_number"
              className="form-control"
              defaultValue={inputFactory.telephone_number}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Address Line</span>
            <input
              type="text"
              placeholder="address_line"
              name="address_line"
              className="form-control"
              defaultValue={inputFactory.address_line}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            <span>Zip Code</span>
            <input
              type="text"
              placeholder="zip_code"
              name="zip_code"
              pattern="^[0-9]*$"
              className="form-control"
              defaultValue={inputFactory.zip_code}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <button className="btn btn-info" type="submit">
          Save
        </button>
      </form>
    </div>
  );
};

export function getFactoryById(factories, id) {
  return factories.find(
    (factory) => factory.factory_id.toString() === id.toString() || null
  );
}

const mapStateToProps = (state, ownProps) => {
  const id = ownProps.match.params.id;
  const factory =
    id && state.factories.factories && state.factories.factories.length > 0
      ? getFactoryById(state.factories.factories, id)
      : null;
  return {
    id,
    factory,
    factories: state.factories.factories,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    loadFactories: bindActionCreators(factoryActions.loadFactories, dispatch),
    updateFactory: bindActionCreators(factoryActions.updateFactory, dispatch),
    addFactory: bindActionCreators(factoryActions.addFactory, dispatch),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(EditFactory);
