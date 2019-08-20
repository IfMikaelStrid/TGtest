import React, { Component } from 'react';
import { render } from 'react-dom';

var buttonStyle = {
    margin: '10px 10px 10px 0'
};

export class NewPost extends Component {
    render() {
        return (
            <button
                className="btn btn-default"
                style={buttonStyle}
                onClick={this.props.handleClick}>{this.props.label}</button>
            );
        }
}