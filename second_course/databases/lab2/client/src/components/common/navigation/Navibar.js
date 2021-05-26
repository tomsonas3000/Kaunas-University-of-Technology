import React from "react";
import NavItem from "./NavItem";

class Navbar extends React.Component {
  constructor(props) {
    super(props);
  }
  render() {
    return (
      <nav
        className="navbar sticky-top navbar-expand-lg navbar-light bg-light mb-5"
        style={{ backgroundColor: "#ffffff" }}
      >
        {" "}
        <span className="navbar-brand mb-0 h1">
          <h1>3 labaratorinis</h1>
        </span>
        <div className="collapse navbar-collapse" id="navbarResponsive">
          <ul className="navbar-nav ml-auto" style={{ fontSize: 30 }}>
            <NavItem path="/" name="Home" />
            <NavItem path="/factories" name="Factories" />
            <NavItem path="/shoes" name="Shoes" />
            <NavItem path="/laces" name="Laces" />
            <NavItem path="/processes" name="Processes" />
            <NavItem path="/workers" name="Workers" />
            <NavItem path="/report" name="Report" />
          </ul>
        </div>
      </nav>
    );
  }
}

export default Navbar;
