import React, { Component } from 'react';
import { render } from 'react-dom';
import { Modal } from './NewPostModal';
import { NewPostForm } from './NewPostForm';

var buttonStyle = {
    position: 'absolute',
    right: '0',
    top: '0',
    margin: '10px 10px 10px 0',
    backgroundColor: '#f44336',
    border: 'none',
    color: '#fff',
    borderRadius: '50%',
};

export class NewPost extends Component {
    constructor(props) {
        super(props);

        this.state = { isOpen: false };
    }

    toggleModal = () => {

        this.setState({
            isOpen: !this.state.isOpen
        });
    }

    render() {
        return (
            <div>
                <button
                    className="btn btn-default"
                    style={buttonStyle}
                    onClick={this.toggleModal}>+</button>

                <Modal show={this.state.isOpen}
                    onClose={this.toggleModal}>
                    <NewPostForm/>
                </Modal>
            </div>
            );
    }

}