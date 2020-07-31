import React, { Component } from "react";
import { FormGroup, Label, Input, Table } from "reactstrap";

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = {
            pricePeriods: [],
            loading: true,
            marketSelect: "sv",
            currencySelect: "SEK"
        };
        this.handleInputChange = this.handleInputChange.bind(this);
    }

    componentDidMount() {
        this.populatePricePeriodsData(this.state.marketSelect, this.state.currencySelect);
    }

    static renderProductPriceTables(productPrices) {
        return (
            <Table bordered hover>
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
            </Table>
        );
    }

    async handleInputChange(e) {
        this.setState({ [e.target.id]: e.target.value }, () => {
            this.populatePricePeriodsData(this.state.marketSelect, this.state.currencySelect);
        });
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
                <h1 id="tabelLabel">{id}</h1>
                <FormGroup>
                    <div>
                        <Label for="marketSelect">Market</Label>
                        <Input bsSize="lg" type="select" name="select" value={this.marketSelect} id="marketSelect" onChange={this.handleInputChange} >
                            <option>sv</option>
                            <option>en</option>
                            <option>de</option>
                            <option>fr</option>
                            <option>ko</option>
                            <option>no</option>
                        </Input>
                    </div>
                    <div>
                        <Label for="currencySelect">Currency</Label>
                        <Input bsSize="lg" type="select" name="select" value={this.currencySelect} id="currencySelect" onChange={this.handleInputChange} >
                            <option>SEK</option>
                            <option>NOK</option>
                            <option>EUR</option>
                            <option>DKK</option>
                        </Input>
                    </div>
                </FormGroup>
                <div>
                    {contents}
                </div>
            </div>
        );
    }
    async populatePricePeriodsData(market, currency) {
        const id = this.props.match.params.id;
        console.log(`api/PriceDetail/${id}?market=${market}&currency=${currency}`);
        const response = await fetch(`api/PriceDetail/${id}?market=${market}&currency=${currency}`);
        const data = await response.json();
        this.setState({ pricePeriods: data, loading: false });
    }
}
