import React, { Component } from 'react';
import Input from './Input';  

export class NewPostForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            postTitle: '',
            content: ''
        };


        this.handleFormSubmit = this.handleFormSubmit.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleTextAreaChange = this.handleTextAreaChange.bind(this);
        this.handleClearForm = this.handleClearForm.bind(this);
    }

    /* This life cycle hook gets executed when the component mounts */

    handleFormSubmit(event) {
        const data = new FormData(event.target);
        fetch('/api/PostsAPI/', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                Title: this.state.postTitle,
                Content: this.state.content
            })
        });
    }

    handleInputChange(event) {
        this.setState({ postTitle: event.target.value });
    }
    handleTextAreaChange(event) {
        this.setState({ content: event.target.value });
    }
    handleClearForm() {
        // Logic for resetting the form
    }

    render() {
        return (
            <form className="form-container" onSubmit={this.handleFormSubmit}>
                <input type='text'
                    className="form-control title-input"
                    value={this.state.postTitle}
                    onChange={this.handleInputChange}
                    placeholder='Enter new title'
                /> {/* Name of the user */}
                <textarea
                    placeholder="write your post"
                    value={this.state.content}
                    onChange={this.handleTextAreaChange}
                    className="form-control" rows="5" id="comment" />
                <button className="submit-button" type="submit">Submit</button>
             </form>
        );
    }
}
