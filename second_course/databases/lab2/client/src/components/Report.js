import React, { useState } from "react";
import axios from "axios";

const Report = () => {
  const [reportResults, setResults] = useState(null);
  const [orderPrice, setPrice] = useState(0);
  const [buttonDisabled, setButton] = useState(true);

  function handleClick(event) {
    event.preventDefault();
    axios
      .get("http://localhost:5000/api/getReport", {
        params: {
          orderPrice,
        },
      })
      .then((res) => {
        setResults(res.data);
      });
  }
  return reportResults ? (
    <div className="col" style={{ textAlign: "center", fontSize: 22 }}>
      <table className="table">
        <thead className="thead-dark">
          <tr>
            <th scope="col">Factory ID</th>
            <th scope="col">Factory location</th>
            <th scope="col">Factory order count where price > {orderPrice}</th>
            <th scope="col">Factory orders average price</th>
          </tr>
        </thead>
        <tbody>
          {reportResults.map((result) => {
            return (
              <tr key={result.factory_id}>
                <th scope="row">{result.factory_id}</th>
                <td>{result.factory_location}</td>
                <td>{result.order_price_over_number}</td>
                <td>{result.order_average_price}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
      <button
        className="btn btn-secondary p-2"
        onClick={() => {
          setResults(null);
          setButton(true);
        }}
      >
        Another query
      </button>
    </div>
  ) : (
    <div>
      <h4>Count orders where price is over</h4>
      <input
        className="form-control"
        onChange={(e) => {
          setPrice(e.target.value);
          e.target.value === "" ? setButton(true) : setButton(false);
        }}
        type="number"
      ></input>
      <button
        className="btn btn-primary my-2"
        style={{ textAlign: "center", margin: "auto", display: "block" }}
        onClick={handleClick}
        disabled={buttonDisabled}
      >
        Get Report
      </button>
    </div>
  );
};

export default Report;
