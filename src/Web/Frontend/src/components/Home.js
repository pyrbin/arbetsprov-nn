import React, { Component } from "react";
import { FormText, FormGroup, Form, FormFeedback, Input } from "reactstrap";

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { query: "27773-02" };
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleSubmit(event) {
        window.location.assign(`sku/${this.state.query}`);
        event.preventDefault();
    }

    render() {
        return (
            <div className="clearfix">
                <h3>Search for product</h3>
                <Form onSubmit={this.handleSubmit}>
                    <FormGroup>
                        <Input bsSize="lg" value={this.state.query} onChange={(e) => this.setState({ query: e.target.value })} />
                        <FormFeedback>You will not be able to see this</FormFeedback>
                        <FormText>Example: 27773-02</FormText>
                    </FormGroup>
                </Form>
            </div>
        );
    }
}
