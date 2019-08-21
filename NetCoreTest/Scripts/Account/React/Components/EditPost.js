import React, { Component } from 'react';
import { render } from 'react-dom';

export class EditPost extends Component {
    constructor(props) {
        super(props);

        this.state = {
            error: null,
            isLoaded: false,
            items: [],
            condition: false,
            postTitle: '',
            content: ''
        };

        this.handleFormSubmit = this.handleFormSubmit.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleTextAreaChange = this.handleTextAreaChange.bind(this);
    }

    componentDidMount() {
        fetch(`${window.location.origin.toString()}/api/PostsAPI/${this.props.editPostId}`)
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        items: result,
                        postTitle: result.title,
                        content: result.content
                    });
                },
                (error) => {
                    this.setState({
                        isLoaded: true,
                        error
                    });
                }
            )
    }


    handleFormSubmit(event) {

        const data = new FormData(event.target);
        fetch(`${window.location.origin.toString()}/api/PostsAPI/${this.props.editPostId}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                PostId: this.state.items.postId,
                Author: this.state.items.Author,
                PublishTimeStamp: this.state.items.PublishTimeStamp,
                AuthorId: this.state.items.AuthorId,
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

    render() {
        const { error, isLoaded, items} = this.state;
        if (error) {
            return <div>Error: {error.message}</div>;
        } else if (!isLoaded) {
            return <div>Loading...</div>;
        } else {
            return (
                <div className="postcard-container">
                    <div className="PostCard PostCard-myPosts" key={items.postId}>
                        <div className="postCard-row">
                            <div className="postCard-column">
                                <form className="form-container" onSubmit={this.handleFormSubmit}>
                                    <input type='text'
                                        className="form-control title-input"
                                        value={this.state.postTitle}
                                        onChange={this.handleInputChange}
                                        placeholder={items.title}
                                    /> 
                                    <textarea
                                        placeholder={items.content}
                                        value={this.state.content}
                                        onChange={this.handleTextAreaChange}
                                        className="form-control" rows="5" id="comment" />
                                    <button className="submit-button" type="submit">Submit</button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            );
        }
    }
}