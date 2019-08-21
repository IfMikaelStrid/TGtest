import React, { Component } from 'react';
import { render } from 'react-dom';

export class DeletePost extends Component {
    constructor(props) {
        super(props);

        this.handleFormSubmit = this.handleFormSubmit.bind(this);
        this.handleCancel = this.handleCancel.bind(this);
    }

    handleFormSubmit(event) {
        const data = new FormData(event.target);
        fetch(`${window.location.origin.toString()}/api/PostsAPI/${this.props.editPostId}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        });
        window.location.reload();
    }
    handleCancel() {
        window.location.reload();
    }

    render() {
        return (
            <div className="postcard-container">
                <div className="PostCard PostCard-myPosts">
                    <div className="postCard-row rowDelete">
                        <div className="PostCard-title">
                            Are you sure you want to delete - <span>{this.props.postTitle}</span>
                        </div>
                    </div>
                    <div className="postCard-row rowDelete">
                        <button className="submit-button delete" onClick={() => this.handleCancel()} type="submit">Cancel</button>
                        <button className="submit-button delete" onClick={() => this.handleFormSubmit(this.props.editPostId)} type="submit">Delete</button>
                    </div>
                </div>
            </div>
        );
    }
}
