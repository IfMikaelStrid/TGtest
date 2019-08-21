import React, { Component } from 'react';
import { render } from 'react-dom';

export class UpVotes extends Component {
    constructor(props) {
        super(props);
        this.state = {
            error: null,
            isLoaded: false,
            items: [],
            numberOfUpvotes: 0,
            condition: false
        };
        this.onUpvote = this.onUpvote.bind(this);
        this.upVoteFlare = this.upVoteFlare.bind(this);
    }

    componentDidMount() {
        fetch(`${window.location.origin.toString()}/api/Upvotes/${this.props.item.postId}`)
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        items: result,
                        numberOfUpvotes: result.length
                    });
                },
                (error) => {
                    this.setState({
                        isLoaded: true,
                        error
                    });
                }
        );

        fetch(`${window.location.origin.toString()}/api/Upvotes/getuserupvotes/${this.props.item.postId}`)
            .then(
                (result) => {
                    this.setState({
                        isLoaded: true,
                        items: result
                    });
                    if (result.ok) {
                        this.upVoteFlare();
                    }
                },
                (error) => {
                    this.setState({
                        isLoaded: true,
                        error
                    });
                }
            );
    }

    upVoteFlare = () => {
        var element = document.getElementById(this.props.item.postId);

        if (element.classList.contains('far')) {
            element.classList.remove('far');
            element.classList.add('fas');
        }
    };

    onUpvote(postId) {
        this.upVoteFlare();
        fetch('/api/Upvotes/', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                PostId: postId
            })
        }).then(
            (result) => {
                if (result.ok) {
                    this.setState({
                        numberOfUpvotes: this.state.numberOfUpvotes + 1
                    });
                }
            },
            (error) => {
                this.setState({
                    isLoaded: true,
                    error
                });
            }
        );
    }


    render() {
        const { error, isLoaded, items, numberOfUpvotes } = this.state;
        if (error) {
            return <div>Error: {error.message}</div>;
        } else if (!isLoaded) {
            return <></>;
        } else {
            return (
                <>
                    <span className="upvote-number">
                        {numberOfUpvotes}
                    </span>
                    <i id={this.props.item.postId} onClick={() => this.onUpvote(this.props.item.postId)} className="far fa-heart" />
                </>
            );
        }
    }
}