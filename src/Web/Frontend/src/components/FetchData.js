import React, { Component } from "react";

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    this.state = { pricePeriods: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderProductPriceTables(productPrices) {
    return (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Marknad</th>
            <th>Pris</th>
            <th>Valuta </th>
            <th>Start och slut</th>
          </tr>
        </thead>
        <tbody>
          {productPrices.map((item, i) => (
            <tr key={i}>
              <td>{item.market}</td>
              <td>{item.price}</td>
              <td>{item.currency}</td>
              <td>{item.start + " - " + item.end}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    const id = this.props.match.params.id;
    const contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      FetchData.renderProductPriceTables(this.state.pricePeriods)
    );

    return (
      <div>
        <h1 id="tabelLabel">Optimized price table for SKU: {id} </h1>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const id = this.props.match.params.id;
    const response = await fetch("api/PriceDetail/" + id);
    const data = await response.json();
    this.setState({ pricePeriods: data, loading: false });
  }
}
